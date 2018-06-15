using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class CharTests : BaseTest<char>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return Fail("");
            yield return ReadWrite("a", 'a');
            yield return Fail("aa");
            yield return ReadWrite("\0", '\0');
            yield return ReadWrite("☑", '☑');
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TestData data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetData))]
        public void TestWhitespace(TestData data) => RunWhitespaceTest(data);
    }

    public class StringTests : BaseTest<string>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return ReadWrite("\"\"", ""); // Blank
            yield return ReadWrite("\"aaaaaa\"", "aaaaaa"); // Short
            yield return ReadWrite('\"' + new string('a', 65536) + '\"', new string('a', 65536)); // Long

            yield return ReadWrite("\"☑\"", "☑"); // Unicode
            yield return ReadWrite("\"a☑b\"", "a☑b");
            yield return ReadWrite("\"👌\"", "👌");
            yield return ReadWrite("\"a👌b\"", "a👌b");

            yield return ReadWrite("\"\\\\\"", "\\"); // \\
            yield return ReadWrite("\"a\\\\/b\"", "a\\b");
            yield return ReadWrite("\"\\/\"", "/"); // \/
            yield return ReadWrite("\"\\'\"", "'"); // \'
            yield return ReadWrite("\"a\\'/b\"", "a'b");
            yield return ReadWrite("\"\\\"\"", "\""); // \"
            yield return ReadWrite("\"a\\\"b\"", "a\"b");
            yield return ReadWrite("\"a\\/b\"", "a/b");
            yield return ReadWrite("\"\\\r\"", "\r"); // \r
            yield return ReadWrite("\"a\\\rb\"", "a\rb");
            yield return ReadWrite("\"\\\n\"", "\n"); // \n
            yield return ReadWrite("\"a\\\nb\"", "a\nb");
            yield return ReadWrite("\"\\\t\"", "\t"); // \t
            yield return ReadWrite("\"a\\\tb\"", "a\tb");
            yield return ReadWrite("\"\\\f\"", "\f"); // \f
            yield return ReadWrite("\"a\\\fb\"", "a\fb");
            yield return ReadWrite("\"\\\b\"", "\b"); // \b
            yield return ReadWrite("\"a\\\bb\"", "a\bb");
            yield return ReadWrite("\"\\\u0123\"", "\u0123"); // \u0123
            yield return ReadWrite("\"a\\\u0123b\"", "a\u0123b");
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TestData data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetData))]
        public void TestWhitespace(TestData data) => RunWhitespaceTest(data);
    }
}
