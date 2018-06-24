using System.Net.Http;
using RestEase;
using Voltaic.Serialization.Json;

namespace Wumpus.Net
{
    // TODO: RestEase converts to UTF16 but we support reading the UTF8 byte stream
    internal class WumpusResponseDeserializer : ResponseDeserializer
    {
        private readonly JsonSerializer _serializer;

        public WumpusResponseDeserializer(JsonSerializer serializer)
        {
            _serializer = serializer;
        }

        public override T Deserialize<T>(string content, HttpResponseMessage response, ResponseDeserializerInfo info)
            => _serializer.Read<T>(content); // TODO: Why is this not bytes?
    }
}
