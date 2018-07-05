using System.Collections.Generic;
using Voltaic.Serialization.Utf8.Tests;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class DictionaryTests : BaseTest<Dictionary<string, int>>
    {
        private class Comparer : IEqualityComparer<Dictionary<string, int>>
        {
            public bool Equals(Dictionary<string, int> x, Dictionary<string, int> y)
            {
                if (x == null && y == null)
                    return true;
                if (x == null || y == null)
                    return false;
                foreach (var pair in x)
                {
                    if (!y.ContainsKey(pair.Key) || y[pair.Key] != pair.Value)
                        return false;
                }
                foreach (var pair in y)
                {
                    if (!x.ContainsKey(pair.Key) || x[pair.Key] != pair.Value)
                        return false;
                }
                return true;
            }
            public int GetHashCode(Dictionary<string, int> obj) => 0; // Ignore
        }

        public static IEnumerable<object[]> GetData()
        {
            yield return ReadWrite("null", null);

            yield return ReadWrite("{}", new Dictionary<string, int>());
            yield return ReadWrite("{\"a\":1}", new Dictionary<string, int> { ["a"] = 1 });
            yield return Read("{ \"a\":1}", new Dictionary<string, int> { ["a"] = 1 });
            yield return Read("{\"a\":1 }", new Dictionary<string, int> { ["a"] = 1 });
            yield return Read("{\"a\" :1}", new Dictionary<string, int> { ["a"] = 1 });
            yield return Read("{\"a\": 1}", new Dictionary<string, int> { ["a"] = 1 });
            yield return Read("{  \"a\"  :  1  }", new Dictionary<string, int> { ["a"] = 1 });
            yield return ReadWrite("{\"a\":1,\"b\":2,\"c\":3}", new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 });
            yield return Read("{\"a\":1 ,\"b\":2 ,\"c\":3}", new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 });
            yield return Read("{\"a\":1, \"b\":2, \"c\":3}", new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 });
            yield return Read("{\"a\":1 , \"b\":2 , \"c\":3}", new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 });
            yield return Read("{\"a\":1  ,  \"b\":2  ,  \"c\":3}", new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 });
            yield return FailRead("{");
            yield return FailRead("}");
            yield return FailRead("{a}");
            yield return FailRead("{a}:");
            yield return FailRead("{\"a\"}");
            yield return FailRead("{\"a\":}");
            yield return FailRead("{1}");
            yield return FailRead("{:1}");
            yield return FailRead("{,1}");
            yield return FailRead("{\"a\":1");
            yield return FailRead("\"a\":1}");
            yield return FailRead("{\"a\":1,\"b\":2,\"b\":3}"); // Duplicate Key
            yield return FailRead("{\"a\":1\"b\":2\"c\":3}");
            yield return FailRead("{\"a\":1 \"b\":2 \"c\":3}");
            yield return FailRead("{\"a\":1:\"b\":2:\"c\":3}");
            yield return FailRead("[\"a\":1]");
            yield return FailRead("{\"a\"1}");
            yield return FailRead("{\"a\" 1}");
        }

        public DictionaryTests() : base(new Comparer()) { }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Object(TextTestData<Dictionary<string, int>> data) => RunTest(data);
    }
}
