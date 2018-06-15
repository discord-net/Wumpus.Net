using System.Collections.Generic;
using Voltaic.Serialization.Utf8;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class SByteTests : BaseTest<sbyte>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return Fail("-129"); // Min - 1
            yield return ReadWrite("-128", -128); // Min
            yield return ReadWrite("0", 0);
            yield return ReadWrite("127", 127); // Max
            yield return Fail("128"); // Max + 1
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TestData data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetData))]
        public void TestQuotes(TestData data) => RunQuoteTest(data);
        [Theory]
        [MemberData(nameof(GetData))]
        public void TestWhitespace(TestData data) => RunWhitespaceTest(data);
    }

    public class Int16Tests : BaseTest<short>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return Fail("-32769"); // Min - 1
            yield return ReadWrite("-32768", -32768); // Min
            yield return ReadWrite("0", 0);
            yield return ReadWrite("32767", 32767); // Max
            yield return Fail("32768"); // Max + 1
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TestData data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetData))]
        public void TestQuotes(TestData data) => RunQuoteTest(data);
        [Theory]
        [MemberData(nameof(GetData))]
        public void TestWhitespace(TestData data) => RunWhitespaceTest(data);
    }

    public class Int32Tests : BaseTest<int>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return Fail("-2147483649"); // Min - 1
            yield return ReadWrite("-2147483648", -2147483648); // Min
            yield return ReadWrite("0", 0);
            yield return ReadWrite("2147483647", 2147483647); // Max
            yield return Fail("2147483648"); // Max + 1
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TestData data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetData))]
        public void TestQuotes(TestData data) => RunQuoteTest(data);
        [Theory]
        [MemberData(nameof(GetData))]
        public void TestWhitespace(TestData data) => RunWhitespaceTest(data);
    }

    public class Int53Tests : BaseTest<long>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return Fail("-9223372036854775809"); // Min - 1
            yield return Read("-9223372036854775808", -9223372036854775808); // Min
            //yield return Fail("-9007199254740992"); // Min - 1
            yield return ReadWrite("-9007199254740991", -9007199254740991); // Min
            yield return ReadWrite("0", 0);
            yield return ReadWrite("9007199254740991", 9007199254740991);  // Max
            //yield return Fail("9007199254740992"); // Max + 1
            yield return Read("9223372036854775807", 9223372036854775807); // Max
            yield return Fail("9223372036854775808"); // Max + 1
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TestData data) => RunTest(data, new Int53JsonConverter());
        [Theory]
        [MemberData(nameof(GetData))]
        public void TestQuotes(TestData data) => RunQuoteTest(data, new Int53JsonConverter());
        [Theory]
        [MemberData(nameof(GetData))]
        public void TestWhitespace(TestData data) => RunWhitespaceTest(data, new Int53JsonConverter());
    }

    public class Int64Tests : BaseTest<long>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return Fail("-9223372036854775809"); // Min - 1
            yield return Read("-9223372036854775808", -9223372036854775808); // Min
            yield return Read("0", 0);
            yield return Read("9223372036854775807", 9223372036854775807); // Max
            yield return Fail("9223372036854775808"); // Max + 1

            yield return Fail("\"-9223372036854775809\""); // Min - 1
            yield return ReadWrite("\"-9223372036854775808\"", -9223372036854775808); // Min
            yield return ReadWrite("\"0\"", 0);
            yield return ReadWrite("\"9223372036854775807\"", 9223372036854775807); // Max
            yield return Fail("\"9223372036854775808\""); // Max + 1

            yield return Fail("\"0"); // Unclosed quote
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TestData data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetData))]
        public void TestWhitespace(TestData data) => RunWhitespaceTest(data);
    }
}
