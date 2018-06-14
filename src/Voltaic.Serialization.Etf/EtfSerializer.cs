using System;
using System.Buffers;

namespace Voltaic.Serialization.Json
{
    public class EtfSerializer : Serializer
    {
        public EtfSerializer(ConverterCollection converters = null, ArrayPool<byte> bytePool = null)
          : base(converters, bytePool)
        {
        }

        public T Read<T>(ReadOnlyMemory<byte> data, ValueConverter<T> converter = null)
            => base.Read<T>(data.Span, converter);
        public new T Read<T>(ReadOnlySpan<byte> data, ValueConverter<T> converter = null)
            => base.Read<T>(data, converter);
        public new ReadOnlyMemory<byte> Write<T>(T value, ValueConverter<T> converter = null)
            => base.Write<T>(value, converter).AsMemory();
    }
}
