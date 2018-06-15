using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class DictionaryTests : BaseTest<Dictionary<string, int>>
    {
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
            yield return Fail("{");
            yield return Fail("}");
            yield return Fail("{a}");
            yield return Fail("{a}:");
            yield return Fail("{\"a\"}");
            yield return Fail("{\"a\":}");
            yield return Fail("{1}");
            yield return Fail("{:1}");
            yield return Fail("{,1}");
            yield return Fail("{\"a\":1");
            yield return Fail("\"a\":1}");
            yield return Fail("{\"a\":1,\"b\":2,\"b\":3}"); // Duplicate Key
            yield return Fail("{\"a\":1\"b\":2\"c\":3}");
            yield return Fail("{\"a\":1 \"b\":2 \"c\":3}");
            yield return Fail("{\"a\":1:\"b\":2:\"c\":3}");
            yield return Fail("[\"a\":1]");
            yield return Fail("{\"a\"1}");
            yield return Fail("{\"a\" 1}");
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TestData data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetData))]
        public void TestWhitespace(TestData data) => RunWhitespaceTest(data);
    }
}
