using System;
using System.Buffers;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Voltaic.Serialization.Json
{
    public class JsonSerializer : Serializer
    {
        public static StandardFormat BooleanFormat { get; } = new StandardFormat('l'); // true/false
        public static StandardFormat FloatFormat { get; } = new StandardFormat('g'); // 1.245000e+1
        public static StandardFormat IntFormat { get; } = new StandardFormat('d'); // 32767

        public JsonSerializer(ConverterCollection converters = null, ArrayPool<byte> bytePool = null)
          : base(converters, bytePool)
        {
            // Integers
            _converters.SetDefault<sbyte, SByteJsonConverter>(
                (s, t, p) => new SByteJsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<byte, ByteJsonConverter>(
                (s, t, p) => new ByteJsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<short, Int16JsonConverter>(
                (s, t, p) => new Int16JsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<ushort, UInt16JsonConverter>(
                (s, t, p) => new UInt16JsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<int, Int32JsonConverter>(
                (s, t, p) => new Int32JsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<uint, UInt32JsonConverter>(
                (s, t, p) => new UInt32JsonConverter(GetStandardFormat(p)));
            _converters.AddConditional<long, Int53JsonConverter>(
                (t, p) => p?.GetCustomAttribute<Int53Attribute>() != null,
                (s, t, p) => new Int53JsonConverter(GetStandardFormat(p)));
            _converters.AddConditional<ulong, UInt53JsonConverter>(
                (t, p) => p?.GetCustomAttribute<Int53Attribute>() != null,
                (s, t, p) => new UInt53JsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<long, Int64JsonConverter>(
                (s, t, p) => new Int64JsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<ulong, UInt64JsonConverter>(
                (s, t, p) => new UInt64JsonConverter(GetStandardFormat(p)));

            // Floats
            _converters.SetDefault<float, SingleJsonConverter>(
                (s, t, p) => new SingleJsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<double, DoubleJsonConverter>(
                (s, t, p) => new DoubleJsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<decimal, DecimalJsonConverter>(
                (s, t, p) => new DecimalJsonConverter(GetStandardFormat(p)));

            // Dates/TimeSpans
            _converters.SetDefault<DateTime, DateTimeJsonConverter>(
                (s, t, p) => new DateTimeJsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<DateTimeOffset, DateTimeOffsetJsonConverter>(
                (s, t, p) => new DateTimeOffsetJsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<TimeSpan, TimeSpanJsonConverter>(
                (s, t, p) => new TimeSpanJsonConverter(GetStandardFormat(p)));

            // Collections
            _converters.AddGlobalConditional(typeof(ArrayJsonConverter<>), 
                (t, p) => t.IsArray, 
                (t) => t.GetElementType());
            _converters.SetGenericDefault(typeof(List<>), typeof(ListJsonConverter<>), 
                (t) => t.GenericTypeArguments[0]);
            _converters.AddGenericConditional(typeof(Dictionary<,>), typeof(DictionaryJsonConverter<>), 
                (t, p) => t.GenericTypeArguments[0] == typeof(string), 
                (t) => t.GenericTypeArguments[1]);

            // Others
            _converters.SetDefault<char, CharJsonConverter>();
            _converters.SetDefault<string, StringJsonConverter>();
            _converters.SetDefault<bool, BooleanJsonConverter>(
                (s, t, p) => new BooleanJsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<Guid, GuidJsonConverter>(
                (s, t, p) => new GuidJsonConverter(GetStandardFormat(p)));
            _converters.SetGenericDefault(typeof(Nullable<>), typeof(NullableJsonConverter<>), 
                (t) => t.GenericTypeArguments[0]);
            _converters.SetGenericDefault(typeof(Optional<>), typeof(OptionalJsonConverter<>),
                (t) => t.GenericTypeArguments[0]);
            _converters.AddGlobalConditional(typeof(ObjectJsonConverter<>),
                (t, p) => t.IsClass,
                (t) => t.AsType());
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
                var span = data.AsSpan();
                if (Encodings.Utf8.ToUtf16Length(span, out int bytes) != OperationStatus.Done)
                    throw new SerializationException("Failed to convert to UTF16");
                var utf16 = new char[bytes / 2];
                if (Encodings.Utf8.ToUtf16(span, MemoryMarshal.AsBytes(utf16.AsSpan()), out _, out _) != OperationStatus.Done)
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
                var span = data.AsSpan();
                if (Encodings.Utf8.ToUtf16Length(span, out int bytes) != OperationStatus.Done)
                    throw new SerializationException("Failed to convert to UTF16");

                var result = new String(' ', bytes / 2);
                unsafe
                {
                    fixed (char* pResult = result)
                    {
                        var resultBytes = new Span<byte>((void*)pResult, bytes);
                        if (Encodings.Utf8.ToUtf16(span, resultBytes, out _, out _) != OperationStatus.Done)
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

        private StandardFormat GetStandardFormat(PropertyInfo propInfo)
        {
            var attr = propInfo?.GetCustomAttribute<StandardFormatAttribute>();
            if (attr == null)
                return default;
            return attr.Format;
        }
    }
}
