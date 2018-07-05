using System.Collections.Generic;
using Voltaic.Serialization.Utf8.Tests;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class CharTests : BaseTest<char>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return FailRead("");
            yield return ReadWrite("a", 'a');
            yield return FailRead("aa");
            yield return ReadWrite("\\u0000", '\0');
            yield return ReadWrite("\\u2611", '☑');
            yield return FailRead("👌");
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void String(TextTestData<char> data) => RunQuoteTest(data);
    }

    public class StringTests : BaseTest<string>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return ReadWrite("", ""); // Blank
            yield return ReadWrite("aaaaaa", "aaaaaa"); // Short
            yield return ReadWrite(new string('a', 65536), new string('a', 65536)); // Long

            yield return ReadWrite("\\u2611", "☑"); // Unicode
            yield return ReadWrite("a\\u2611b", "a☑b");
            yield return ReadWrite("\\uD83D\\uDC4C", "👌");
            yield return ReadWrite("a\\uD83D\\uDC4Cb", "a👌b");

            yield return ReadWrite("\\\\", "\\"); // \\
            yield return ReadWrite("a\\\\b", "a\\b");
            yield return Read("\\/", "/"); // \/
            yield return ReadWrite("/", "/"); // \/
            yield return Read("a\\/b", "a/b");
            yield return ReadWrite("a/b", "a/b");
            yield return ReadWrite("\\\"", "\""); // \"
            yield return ReadWrite("a\\\"b", "a\"b");
            yield return ReadWrite("\\r", "\r"); // \r
            yield return ReadWrite("a\\rb", "a\rb");
            yield return ReadWrite("\\n", "\n"); // \n
            yield return ReadWrite("a\\nb", "a\nb");
            yield return ReadWrite("\\t", "\t"); // \t
            yield return ReadWrite("a\\tb", "a\tb");
            yield return ReadWrite("\\f", "\f"); // \f
            yield return ReadWrite("a\\fb", "a\fb");
            yield return ReadWrite("\\b", "\b"); // \b
            yield return ReadWrite("a\\bb", "a\bb");
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void String(TextTestData<string> data) => RunQuoteTest(data);
    }
}
