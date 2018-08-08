using System.Buffers;
using Voltaic.Serialization;
using Voltaic.Serialization.Json;
using Wumpus.Entities;

namespace Wumpus.Serialization
{
    public class WumpusJsonSerializer : JsonSerializer
    {
        public WumpusJsonSerializer(ConverterCollection converters = null, ArrayPool<byte> bytePool = null)
          : base(converters, bytePool)
        {
            _converters.SetGenericDefault(typeof(EntityOrId<>), typeof(EntityOrIdConverter<>),
                (t) => t.GenericTypeArguments[0]);
            _converters.SetDefault<Color, ColorConverter>();
            _converters.SetDefault<Image, ImageConverter>();
            _converters.SetDefault<Snowflake, SnowflakeConverter>();
        }
    }
}
