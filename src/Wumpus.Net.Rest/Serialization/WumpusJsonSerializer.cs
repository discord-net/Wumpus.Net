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
            _converters.SetGenericDefault(typeof(EntityOrId<>), typeof(EntityOrIdJsonConverter<>),
                (t) => t.GenericTypeArguments[0]);
            _converters.SetDefault<Color, ColorJsonConverter>();
            _converters.SetDefault<Image, ImageJsonConverter>();
            _converters.SetDefault<Snowflake, SnowflakeJsonConverter>();
        }
    }
}
