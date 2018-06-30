using System.Buffers;
using Voltaic.Serialization;
using Voltaic.Serialization.Etf;
using Wumpus.Entities;

namespace Wumpus.Serialization
{
    public class WumpusEtfSerializer : EtfSerializer
    {
        public WumpusEtfSerializer(ConverterCollection converters = null, ArrayPool<byte> bytePool = null)
          : base(converters, bytePool)
        {
            _converters.SetGenericDefault(typeof(EntityOrId<>), typeof(EntityOrIdEtfConverter<>),
                (t) => t.GenericTypeArguments[0]);
            _converters.SetDefault<Color, ColorEtfConverter>();
            _converters.SetDefault<Image, ImageEtfConverter>();
            _converters.SetDefault<Snowflake, SnowflakeEtfConverter>();
        }
    }
}
