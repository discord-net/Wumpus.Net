using System;
using System.Buffers;
using System.Runtime.InteropServices;

namespace Voltaic.Serialization.Utf8
{
    // TODO: Adjust writer buffers based on StandardFormat's max length

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

            // Strings
            _converters.SetDefault<char, CharUtf8Converter>();
            _converters.SetDefault<string, StringUtf8Converter>();
            _converters.SetDefault<Utf8String, Utf8StringUtf8Converter>();

            // Others
            _converters.SetDefault<bool, BooleanUtf8Converter>();
            _converters.SetDefault<Guid, GuidUtf8Converter>();
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

        public T ReadUtf16<T>(ResizableMemory<char> data, ValueConverter<T> converter = null)
            => ReadUtf16(data.AsReadOnlySpan(), converter);
        public T ReadUtf16<T>(ReadOnlyMemory<char> data, ValueConverter<T> converter = null)
            => ReadUtf16(data.Span, converter);
        public T ReadUtf16<T>(ReadOnlySpan<char> data, ValueConverter<T> converter = null)
        {
            var utf16Bytes = MemoryMarshal.AsBytes(data);
            if (Encodings.Utf16.ToUtf8Length(utf16Bytes, out int bytes) != OperationStatus.Done)
                throw new SerializationException("Failed to convert to UTF8");
            var utf8 = _pool.Rent(bytes);
            try
            {
                if (Encodings.Utf16.ToUtf8(utf16Bytes, MemoryMarshal.AsBytes(utf8.AsSpan()), out _, out _) != OperationStatus.Done)
                    throw new SerializationException("Failed to convert to UTF8");
                return Read(utf8.AsSpan(0, bytes), converter);
            }
            finally
            {
                _pool.Return(utf8);
            }
        }
        public T ReadUtf16<T>(string data, ValueConverter<T> converter = null)
            => ReadUtf16(data.AsSpan(), converter);

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
    }
}
