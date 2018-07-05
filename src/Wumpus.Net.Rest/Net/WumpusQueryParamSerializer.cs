using RestEase;
using System.Collections.Generic;
using Wumpus.Serialization;

namespace Wumpus.Net
{
    //TODO: Benchmark our serializer vs Json.Net for short strings like these
    public class WumpusQueryParamSerializer : RequestQueryParamSerializer
    {
        private readonly WumpusJsonSerializer _serializer;

        public WumpusQueryParamSerializer(WumpusJsonSerializer serializer)
        {
            _serializer = serializer;
        }

        public override IEnumerable<KeyValuePair<string, string>> SerializeQueryParam<T>(string name, T value, RequestQueryParamSerializerInfo info)
        {
            if (value == null)
                yield break;

            yield return new KeyValuePair<string, string>(name, _serializer.WriteUtf16String(value));
        }

        public override IEnumerable<KeyValuePair<string, string>> SerializeQueryCollectionParam<T>(string name, IEnumerable<T> values, RequestQueryParamSerializerInfo info)
        {
            if (values == null)
                yield break;

            foreach (var value in values)
            {
                if (value != null)
                    yield return new KeyValuePair<string, string>(name, _serializer.WriteUtf16String(value));
            }
        }
    }
}
