using System;
using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;

namespace Voltaic.Serialization.Utf8
{
    public class Utf8Serializer : Serializer
    {
        public Utf8Serializer(ConverterCollection converters = null, ArrayPool<byte> bytePool = null)
          : base(converters, bytePool)
        {
            // Integers
            _converters.SetDefault<sbyte, SByteUtf8Converter>();
            _converters.SetDefault<byte, ByteUtf8Converter>();
            _converters.SetDefault<short, Int16Utf8Converter>();
            _converters.SetDefault<ushort, UInt16Utf8Converter>();
            _converters.SetDefault<int, Int32Utf8Converter>();
            _converters.SetDefault<uint, UInt32Utf8Converter>();
            _converters.SetDefault<long, Int64Utf8Converter>();
            _converters.SetDefault<ulong, UInt64Utf8Converter>();

            // Floats
            _converters.SetDefault<float, SingleUtf8Converter>();
            _converters.SetDefault<double, DoubleUtf8Converter>();
            _converters.SetDefault<decimal, DecimalUtf8Converter>();

            // Dates/TimeSpans
            _converters.SetDefault<DateTime, DateTimeUtf8Converter>();
            _converters.SetDefault<DateTimeOffset, DateTimeOffsetUtf8Converter>();
            _converters.SetDefault<TimeSpan, TimeSpanUtf8Converter>();

            // Others
            _converters.SetDefault<char, CharUtf8Converter>();
            _converters.SetDefault<string, StringUtf8Converter>();
            _converters.SetDefault<bool, BooleanUtf8Converter>();
            _converters.SetDefault<Guid, GuidUtf8Converter>();
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
