using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Utf8.Tests
{
    public class CharTests : BaseTest<char>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return FailRead("");
            yield return ReadWrite("a", 'a');
            yield return FailRead("aa");
            yield return ReadWrite("\0", '\0');
            yield return ReadWrite("☑", '☑');
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TextTestData<char> data) => RunTest(data);
    }

    public class StringTests : BaseTest<string>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return ReadWrite("", ""); // Blank
            yield return ReadWrite("aaaaaa", "aaaaaa"); // Short
            yield return ReadWrite(new string('a', 65536), new string('a', 65536)); // Long

            yield return ReadWrite("\0", "\0"); // Null
            yield return ReadWrite("a\0b", "a\0b");
            yield return ReadWrite("'", "'"); // Single Quotes
            yield return ReadWrite("''", "''");
            yield return ReadWrite("a'b", "a'b");
            yield return ReadWrite("a''b", "a''b");
            yield return ReadWrite("'ab'", "'ab'");
            yield return ReadWrite("\"", "\""); // Double Quotes
            yield return ReadWrite("\"\"", "\"\"");
            yield return ReadWrite("a\"b", "a\"b");
            yield return ReadWrite("a\"\"b", "a\"\"b");
            yield return ReadWrite("\"ab\"", "\"ab\"");
            yield return ReadWrite("☑", "☑"); // Unicode
            yield return ReadWrite("a☑b", "a☑b");
            yield return ReadWrite("👌", "👌");
            yield return ReadWrite("a👌b", "a👌b");
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TextTestData<string> data) => RunTest(data);
    }
}
