using System.Buffers;
using Voltaic.Serialization.Json;

namespace Wumpus.Server
{
    public class SerializerOptions
    {
        public JsonSerializer Serializer { get; }
        public ArrayPool<byte> Pool { get; }

        public SerializerOptions(JsonSerializer serializer, ArrayPool<byte> pool = null)
        {
            Serializer = serializer;
            Pool = pool ?? ArrayPool<byte>.Shared;
        }
    }
}
