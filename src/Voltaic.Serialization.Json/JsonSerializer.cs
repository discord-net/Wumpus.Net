using System;
using System.Buffers;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Voltaic.Serialization.Json
{
    public class JsonSerializer : Serializer
    {
        public JsonSerializer(ConverterCollection converters = null, ArrayPool<byte> bytePool = null)
          : base(converters, bytePool)
        {
            // Integers
            _converters.SetDefault<sbyte, SByteJsonConverter>();
            _converters.SetDefault<byte, ByteJsonConverter>();
            _converters.SetDefault<short, Int16JsonConverter>();
            _converters.SetDefault<ushort, UInt16JsonConverter>();
            _converters.SetDefault<int, Int32JsonConverter>();
            _converters.SetDefault<uint, UInt32JsonConverter>();
            _converters.AddConditional<long, Int53JsonConverter>((t, p) => p?.GetCustomAttribute<Int53Attribute>() != null);
            _converters.AddConditional<ulong, UInt53JsonConverter>((t, p) => p?.GetCustomAttribute<Int53Attribute>() != null);
            _converters.SetDefault<long, Int64JsonConverter>();
            _converters.SetDefault<ulong, UInt64JsonConverter>();

            // Floats
            _converters.SetDefault<float, SingleJsonConverter>();
            _converters.SetDefault<double, DoubleJsonConverter>();
            _converters.SetDefault<decimal, DecimalJsonConverter>();

            // Dates/TimeSpans
            _converters.SetDefault<DateTime, DateTimeJsonConverter>();
            _converters.SetDefault<DateTimeOffset, DateTimeOffsetJsonConverter>();
            _converters.SetDefault<TimeSpan, TimeSpanJsonConverter>();

            // Others
            _converters.SetDefault<char, CharJsonConverter>();
            _converters.SetDefault<string, StringJsonConverter>();
            _converters.SetDefault<bool, BooleanJsonConverter>();
            _converters.SetDefault<Guid, GuidJsonConverter>();
        }

        public T Read<T>(ReadOnlyMemory<byte> utf8, ValueConverter<T> converter = null)
            => base.Read<T>(utf8.Span, converter);
        public new T Read<T>(ReadOnlySpan<byte> utf8, ValueConverter<T> converter = null)
            => base.Read<T>(utf8, converter);
        public T Read<T>(ReadOnlyMemory<char> utf16, ValueConverter<T> converter = null)
            => Read<T>(utf16.Span, converter);
        public T Read<T>(ReadOnlySpan<char> utf16, ValueConverter<T> converter = null)
        {
            var utf16Bytes = MemoryMarshal.AsBytes(utf16);
            if (Encodings.Utf16.ToUtf8Length(utf16Bytes, out int bytes) != OperationStatus.Done)
                throw new SerializationException("Failed to convert to UTF8");
            var utf8 = _pool.Rent(bytes);
            try
            {
                if (Encodings.Utf16.ToUtf8(utf16Bytes, MemoryMarshal.AsBytes(utf8.AsSpan()), out _, out _) != OperationStatus.Done)
                    throw new SerializationException("Failed to convert to UTF8");
                return base.Read<T>(utf8.AsSpan(0, bytes), converter);
            }
            finally
            {
                _pool.Return(utf8);
            }
        }
        public T Read<T>(string utf16, ValueConverter<T> converter = null)
            => Read<T>(utf16.AsSpan(), converter);

        public ReadOnlyMemory<byte> WriteUtf8<T>(T value, ValueConverter<T> converter = null)
            => base.Write<T>(value, converter).AsMemory();
        public ReadOnlyMemory<char> WriteUtf16<T>(T value, ValueConverter<T> converter = null)
        {
            var data = base.Write<T>(value, converter);
            try
            {
                if (Encodings.Utf8.ToUtf16Length(data, out int bytes) != OperationStatus.Done)
                    throw new SerializationException("Failed to convert to UTF16");
                var utf16 = new char[bytes / 2];
                if (Encodings.Utf8.ToUtf16(data, MemoryMarshal.AsBytes(utf16.AsSpan()), out _, out _) != OperationStatus.Done)
                    throw new SerializationException("Failed to convert to UTF16");
                return utf16.AsMemory();
            }
            finally
            {
                _pool.Return(data.Array);
            }
        }
        public string WriteString<T>(T value, ValueConverter<T> converter = null)
        {
            var data = base.Write<T>(value, converter);
            try
            {
                if (Encodings.Utf8.ToUtf16Length(data, out int bytes) != OperationStatus.Done)
                    throw new SerializationException("Failed to convert to UTF16");

                var result = new String(' ', bytes / 2);
                unsafe
                {
                    fixed (char* pResult = result)
                    {
                        var resultBytes = new Span<byte>((void*)pResult, bytes);
                        if (Encodings.Utf8.ToUtf16(data.AsSpan(), resultBytes, out _, out _) != OperationStatus.Done)
                            throw new SerializationException("Failed to convert to UTF16");
                    }
                }
                return result;
            }
            finally
            {
                _pool.Return(data.Array);
            }
        }
    }
}
