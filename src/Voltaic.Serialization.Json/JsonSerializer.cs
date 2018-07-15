using System;
using System.Buffers;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Voltaic.Serialization.Json
{
    public class JsonSerializer : Serializer
    {
        internal static StandardFormat BooleanFormat { get; } = new StandardFormat('l'); // true/false
        internal static StandardFormat FloatFormat { get; } = new StandardFormat('g'); // 1.245000e+1
        internal static StandardFormat IntFormat { get; } = new StandardFormat('d'); // 32767

        public JsonSerializer(ConverterCollection converters = null, ArrayPool<byte> bytePool = null)
          : base(converters, bytePool)
        {
            // Integers
            _converters.SetDefault<sbyte, SByteJsonConverter>(
                (t, p) => new SByteJsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<byte, ByteJsonConverter>(
                (t, p) => new ByteJsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<short, Int16JsonConverter>(
                (t, p) => new Int16JsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<ushort, UInt16JsonConverter>(
                (t, p) => new UInt16JsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<int, Int32JsonConverter>(
                (t, p) => new Int32JsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<uint, UInt32JsonConverter>(
                (t, p) => new UInt32JsonConverter(GetStandardFormat(p)));
            _converters.AddConditional<long, Int53JsonConverter>(
                (t, p) => p?.GetCustomAttribute<Int53Attribute>() != null,
                (t, p) => new Int53JsonConverter(GetStandardFormat(p)));
            _converters.AddConditional<ulong, UInt53JsonConverter>(
                (t, p) => p?.GetCustomAttribute<Int53Attribute>() != null,
                (t, p) => new UInt53JsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<long, Int64JsonConverter>(
                (t, p) => new Int64JsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<ulong, UInt64JsonConverter>(
                (t, p) => new UInt64JsonConverter(GetStandardFormat(p)));

            // Floats
            _converters.SetDefault<float, SingleJsonConverter>(
                (t, p) => new SingleJsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<double, DoubleJsonConverter>(
                (t, p) => new DoubleJsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<decimal, DecimalJsonConverter>(
                (t, p) => new DecimalJsonConverter(GetStandardFormat(p)));

            // Dates/TimeSpans
            _converters.SetDefault<DateTime, DateTimeJsonConverter>(
                (t, p) => new DateTimeJsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<DateTimeOffset, DateTimeOffsetJsonConverter>(
                (t, p) => new DateTimeOffsetJsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<TimeSpan, TimeSpanJsonConverter>(
                (t, p) => new TimeSpanJsonConverter(GetStandardFormat(p)));
            _converters.AddConditional<DateTime, DateTimeEpochConverter>(
                (t, p) => p?.GetCustomAttribute<EpochAttribute>() != null,
                (t, p) => new DateTimeEpochConverter(this, p.GetCustomAttribute<EpochAttribute>().Type));
            _converters.AddConditional<DateTimeOffset, DateTimeOffsetEpochConverter>(
                (t, p) => p?.GetCustomAttribute<EpochAttribute>() != null,
                (t, p) => new DateTimeOffsetEpochConverter(this, p.GetCustomAttribute<EpochAttribute>().Type));
            _converters.AddConditional<TimeSpan, TimeSpanEpochConverter>(
                (t, p) => p?.GetCustomAttribute<EpochAttribute>() != null,
                (t, p) => new TimeSpanEpochConverter(this, p.GetCustomAttribute<EpochAttribute>().Type));

            // Collections
            _converters.AddGlobalConditional(typeof(ArrayJsonConverter<>),
                (t, p) => t.IsArray,
                (t) => t.GetElementType());
            _converters.SetGenericDefault(typeof(List<>), typeof(ListJsonConverter<>),
                (t) => t.GenericTypeArguments[0]);
            _converters.AddGenericConditional(typeof(Dictionary<,>), typeof(DictionaryJsonConverter<>),
                (t, p) => t.GenericTypeArguments[0] == typeof(string),
                (t) => t.GenericTypeArguments[1]);

            // Strings
            _converters.SetDefault<char, CharJsonConverter>();
            _converters.SetDefault<string, StringJsonConverter>();
            _converters.SetDefault<Utf8String, Utf8StringJsonConverter>();

            // Enums
            _converters.AddGlobalConditional(typeof(Int64EnumJsonConverter<>),
                (t, p) => t.IsEnum && (
                    Enum.GetUnderlyingType(t.AsType()) == typeof(sbyte) ||
                    Enum.GetUnderlyingType(t.AsType()) == typeof(short) ||
                    Enum.GetUnderlyingType(t.AsType()) == typeof(int) ||
                    Enum.GetUnderlyingType(t.AsType()) == typeof(long)),
                (t) => t.AsType());
            _converters.AddGlobalConditional(typeof(UInt64EnumJsonConverter<>),
                (t, p) => t.IsEnum && (
                    Enum.GetUnderlyingType(t.AsType()) == typeof(byte) ||
                    Enum.GetUnderlyingType(t.AsType()) == typeof(ushort) ||
                    Enum.GetUnderlyingType(t.AsType()) == typeof(uint) ||
                    Enum.GetUnderlyingType(t.AsType()) == typeof(ulong)),
                (t) => t.AsType());

            // Others
            _converters.SetDefault<bool, BooleanJsonConverter>(
                (t, p) => new BooleanJsonConverter(GetStandardFormat(p)));
            _converters.SetDefault<Guid, GuidJsonConverter>(
                (t, p) => new GuidJsonConverter(GetStandardFormat(p)));
            _converters.SetGenericDefault(typeof(Nullable<>), typeof(NullableJsonConverter<>),
                (t) => t.GenericTypeArguments[0]);
            _converters.SetGenericDefault(typeof(Optional<>), typeof(OptionalJsonConverter<>),
                (t) => t.GenericTypeArguments[0]);
            _converters.AddGlobalConditional(typeof(ObjectJsonConverter<>),
                (t, p) => t.IsClass,
                (t) => t.AsType());
        }

        public T ReadUtf8<T>(ResizableMemory<byte> data, ValueConverter<T> converter = null)
            => Read(data.AsReadOnlySpan(), converter);
        public T ReadUtf8<T>(ReadOnlyMemory<byte> data, ValueConverter<T> converter = null)
            => Read(data.Span, converter);
        public T ReadUtf8<T>(ReadOnlySpan<byte> data, ValueConverter<T> converter = null)
            => Read(data, converter);
        public T ReadUtf8<T>(Utf8String data, ValueConverter<T> converter = null)
            => Read(data.Bytes, converter);
        public T ReadUtf8<T>(Utf8Span data, ValueConverter<T> converter = null)
            => Read(data.Bytes, converter);

        public object ReadUtf8(Type type, ResizableMemory<byte> data, ValueConverter converter = null)
            => Read(type, data.AsReadOnlySpan(), converter);
        public object ReadUtf8(Type type, ReadOnlyMemory<byte> data, ValueConverter converter = null)
            => Read(type, data.Span, converter);
        public object ReadUtf8(Type type, ReadOnlySpan<byte> data, ValueConverter converter = null)
            => Read(type, data, converter);
        public object ReadUtf8(Type type, Utf8String data, ValueConverter converter = null)
            => Read(type, data.Bytes, converter);
        public object ReadUtf8(Type type, Utf8Span data, ValueConverter converter = null)
            => Read(type, data.Bytes, converter);

        public T ReadUtf16<T>(ResizableMemory<char> data, ValueConverter<T> converter = null)
            => ReadUtf16(MemoryMarshal.AsBytes(data.AsReadOnlySpan()), converter);
        public T ReadUtf16<T>(ReadOnlyMemory<char> data, ValueConverter<T> converter = null)
            => ReadUtf16(MemoryMarshal.AsBytes(data.Span), converter);
        public T ReadUtf16<T>(ResizableMemory<byte> data, ValueConverter<T> converter = null)
            => ReadUtf16(data.AsReadOnlySpan(), converter);
        public T ReadUtf16<T>(ReadOnlyMemory<byte> data, ValueConverter<T> converter = null)
            => ReadUtf16(data.Span, converter);
        public T ReadUtf16<T>(ReadOnlySpan<byte> data, ValueConverter<T> converter = null)
        {
            if (Encodings.Utf16.ToUtf8Length(data, out int bytes) != OperationStatus.Done)
                throw new SerializationException("Failed to convert to UTF8");
            var utf8 = _pool.Rent(bytes);
            try
            {
                if (Encodings.Utf16.ToUtf8(data, MemoryMarshal.AsBytes(utf8.AsSpan()), out _, out _) != OperationStatus.Done)
                    throw new SerializationException("Failed to convert to UTF8");
                return Read(utf8.AsSpan(0, bytes), converter);
            }
            finally
            {
                _pool.Return(utf8);
            }
        }
        public T ReadUtf16<T>(string data, ValueConverter<T> converter = null)
            => ReadUtf16(MemoryMarshal.AsBytes(data.AsSpan()), converter);

        public object ReadUtf16(Type type, ResizableMemory<char> data, ValueConverter converter = null)
            => ReadUtf16(type, MemoryMarshal.AsBytes(data.AsReadOnlySpan()), converter);
        public object ReadUtf16(Type type, ReadOnlyMemory<char> data, ValueConverter converter = null)
            => ReadUtf16(type, MemoryMarshal.AsBytes(data.Span), converter);
        public object ReadUtf16(Type type, ResizableMemory<byte> data, ValueConverter converter = null)
            => ReadUtf16(type, data.AsReadOnlySpan(), converter);
        public object ReadUtf16(Type type, ReadOnlyMemory<byte> data, ValueConverter converter = null)
            => ReadUtf16(type, data.Span, converter);
        public object ReadUtf16(Type type, ReadOnlySpan<byte> data, ValueConverter converter = null)
        {
            if (Encodings.Utf16.ToUtf8Length(data, out int bytes) != OperationStatus.Done)
                throw new SerializationException("Failed to convert to UTF8");
            var utf8 = _pool.Rent(bytes);
            try
            {
                if (Encodings.Utf16.ToUtf8(data, MemoryMarshal.AsBytes(utf8.AsSpan()), out _, out _) != OperationStatus.Done)
                    throw new SerializationException("Failed to convert to UTF8");
                return Read(type, utf8.AsSpan(0, bytes), converter);
            }
            finally
            {
                _pool.Return(utf8);
            }
        }
        public object ReadUtf16(Type type, string data, ValueConverter converter = null)
            => ReadUtf16(type, MemoryMarshal.AsBytes(data.AsSpan()), converter);

        public ReadOnlyMemory<byte> WriteUtf8<T>(T value, ValueConverter<T> converter = null)
            => Write(value, converter).AsReadOnlyMemory();
        public ReadOnlyMemory<byte> WriteUtf8(object value, ValueConverter converter = null)
            => Write(value, converter).AsReadOnlyMemory();
        public Utf8String WriteUtf8String<T>(T value, ValueConverter<T> converter = null)
            => new Utf8String(Write(value, converter).AsReadOnlySpan());
        public Utf8String WriteUtf8String(object value, ValueConverter converter = null)
            => new Utf8String(Write(value, converter).AsReadOnlySpan());

        public ReadOnlyMemory<char> WriteUtf16<T>(T value, ValueConverter<T> converter = null)
            => FinishWriteUtf16(Write(value, converter));
        public ReadOnlyMemory<char> WriteUtf16(object value, ValueConverter converter = null)
            => FinishWriteUtf16(Write(value, converter));
        private ReadOnlyMemory<char> FinishWriteUtf16(ResizableMemory<byte> data)
        {
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
        public ReadOnlyMemory<byte> WriteUtf16Bytes<T>(T value, ValueConverter<T> converter = null)
            => FinishWriteUtf16Bytes(Write(value, converter));
        public ReadOnlyMemory<byte> WriteUtf16Bytes(object value, ValueConverter converter = null)
            => FinishWriteUtf16Bytes(Write(value, converter));
        private ReadOnlyMemory<byte> FinishWriteUtf16Bytes(ResizableMemory<byte> data)
        {
            try
            {
                var span = data.AsSpan();
                if (Encodings.Utf8.ToUtf16Length(span, out int bytes) != OperationStatus.Done)
                    throw new SerializationException("Failed to convert to UTF16");
                var utf16 = new byte[bytes];
                if (Encodings.Utf8.ToUtf16(span, MemoryMarshal.AsBytes(utf16.AsSpan()), out _, out _) != OperationStatus.Done)
                    throw new SerializationException("Failed to convert to UTF16");
                return utf16.AsMemory();
            }
            finally
            {
                _pool.Return(data.Array);
            }
        }
        public string WriteUtf16String<T>(T value, ValueConverter<T> converter = null)
            => FinishWriteUtf16String(Write(value, converter));
        public string WriteUtf16String(object value, ValueConverter converter = null)
            => FinishWriteUtf16String(Write(value, converter));
        private string FinishWriteUtf16String(ResizableMemory<byte> data)
        {
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

        internal new void RaiseUnknownProperty(ModelMap model, Utf8String propName)
            => base.RaiseUnknownProperty(model, propName);
        internal new void RaiseFailedProperty(ModelMap model, PropertyMap prop)
            => base.RaiseFailedProperty(model, prop);
        internal new void RaiseFailedProperty(PropertyMap prop, int i)
            => base.RaiseFailedProperty(prop, i);
    }
}
