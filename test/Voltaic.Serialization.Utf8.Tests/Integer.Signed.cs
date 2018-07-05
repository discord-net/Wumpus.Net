using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Utf8.Tests
{
    public class SByteTests : BaseTest<sbyte>
    {
        public static IEnumerable<object[]> GetDData()
        {
            yield return FailRead("-129"); // Min - 1
            yield return ReadWrite("-128", -128); // Min
            yield return ReadWrite("0", 0);
            yield return ReadWrite("127", 127); // Max
            yield return FailRead("128"); // Max + 1
        }
        public static IEnumerable<object[]> GetNData()
        {
            yield return FailRead("-129.00"); // Min - 1
            yield return ReadWrite("-128.00", -128); // Min
            yield return ReadWrite("0.00", 0);
            yield return ReadWrite("127.00", 127); // Max
            yield return FailRead("128.00"); // Max + 1
        }
        public static IEnumerable<object[]> GetXData()
        {
            yield return ReadWrite("0", 0);
            yield return ReadWrite("80", -128); // Max
            yield return ReadWrite("7F", 127); // Max
            yield return FailRead("100"); // Max + 1
        }

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<sbyte> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<sbyte> data) => RunTest(data, new SByteUtf8Converter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TextTestData<sbyte> data) => RunTest(data, new SByteUtf8Converter('X'));
    }

    public class Int16Tests : BaseTest<short>
    {
        public static IEnumerable<object[]> GetDData()
        {
            yield return FailRead("-32769"); // Min - 1
            yield return ReadWrite("-32768", -32768); // Min
            yield return ReadWrite("0", 0);
            yield return ReadWrite("32767", 32767); // Max
            yield return FailRead("32768"); // Max + 1
        }
        public static IEnumerable<object[]> GetNData()
        {
            yield return FailRead("-32,769.00"); // Min - 1
            yield return ReadWrite("-32,768.00", -32768); // Min
            yield return ReadWrite("0.00", 0);
            yield return ReadWrite("32,767.00", 32767); // Max
            yield return FailRead("32,768.00"); // Max + 1
        }
        public static IEnumerable<object[]> GetXData()
        {
            yield return ReadWrite("0", 0);
            yield return ReadWrite("8000", -32768); // Max
            yield return ReadWrite("7FFF", 32767); // Max
            yield return FailRead("10000"); // Max + 1
        }

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<short> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<short> data) => RunTest(data, new Int16Utf8Converter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TextTestData<short> data) => RunTest(data, new Int16Utf8Converter('X'));
    }

    public class Int32Tests : BaseTest<int>
    {
        public static IEnumerable<object[]> GetDData()
        {
            yield return FailRead("-2147483649"); // Min - 1
            yield return ReadWrite("-2147483648", -2147483648); // Min
            yield return ReadWrite("0", 0);
            yield return ReadWrite("2147483647", 2147483647); // Max
            yield return FailRead("2147483648"); // Max + 1
        }
        public static IEnumerable<object[]> GetNData()
        {
            yield return FailRead("-2,147,483,649.00"); // Min - 1
            yield return ReadWrite("-2,147,483,648.00", -2147483648); // Min
            yield return ReadWrite("0.00", 0);
            yield return ReadWrite("2,147,483,647.00", 2147483647); // Max
            yield return FailRead("2,147,483,648.00"); // Max + 1
        }
        public static IEnumerable<object[]> GetXData()
        {
            yield return ReadWrite("0", 0);
            yield return ReadWrite("80000000", -2147483648); // Max
            yield return ReadWrite("7FFFFFFF", 2147483647); // Max
            yield return FailRead("100000000"); // Max + 1
        }

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<int> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<int> data) => RunTest(data, new Int32Utf8Converter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TextTestData<int> data) => RunTest(data, new Int32Utf8Converter('X'));
    }

    public class Int64Tests : BaseTest<long>
    {
        public static IEnumerable<object[]> GetDData()
        {
            yield return FailRead("-9223372036854775809"); // Min - 1
            yield return ReadWrite("-9223372036854775808", -9223372036854775808); // Min
            yield return ReadWrite("0", 0);
            yield return ReadWrite("9223372036854775807", 9223372036854775807); // Max
            yield return FailRead("9223372036854775808"); // Max + 1
        }
        public static IEnumerable<object[]> GetNData()
        {
            yield return FailRead("-9,223,372,036,854,775,809.00"); // Min - 1
            yield return ReadWrite("-9,223,372,036,854,775,808.00", -9223372036854775808); // Min
            yield return ReadWrite("0.00", 0);
            yield return ReadWrite("9,223,372,036,854,775,807.00", 9223372036854775807); // Max
            yield return FailRead("9,223,372,036,854,775,808.00"); // Max + 1
        }
        public static IEnumerable<object[]> GetXData()
        {
            yield return ReadWrite("0", 0);
            yield return ReadWrite("8000000000000000", -9223372036854775808); // Max
            yield return ReadWrite("7FFFFFFFFFFFFFFF", 9223372036854775807); // Max
            yield return FailRead("10000000000000000"); // Max + 1
        }

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<long> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<long> data) => RunTest(data, new Int64Utf8Converter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TextTestData<long> data) => RunTest(data, new Int64Utf8Converter('X'));
    }
}
