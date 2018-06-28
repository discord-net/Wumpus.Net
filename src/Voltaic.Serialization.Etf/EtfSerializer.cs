using System;
using System.Buffers;

namespace Voltaic.Serialization.Etf
{
    public class EtfSerializer : Serializer
    {
        public EtfSerializer(ConverterCollection converters = null, ArrayPool<byte> bytePool = null)
          : base(converters, bytePool)
        {
        }

        public override T Read<T>(ReadOnlySpan<byte> data, ValueConverter<T> converter = null)
        {
            // Strip header
            if (data.Length > 0 && data[0] == 131)
                data = data.Slice(1); 

            return base.Read(data, converter);
        }
    }
}
