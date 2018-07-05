using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Utf8.Tests
{
    public class ByteTests : BaseTest<byte>
    {
        public static IEnumerable<object[]> GetDData()
        {
            yield return FailRead("-1"); // Min -1
            yield return ReadWrite("0", 0);
            yield return ReadWrite("255", 255); // Max
            yield return FailRead("256"); // Max + 1
        }
        public static IEnumerable<object[]> GetNData()
        {
            yield return FailRead("-1.00"); // Min -1
            yield return ReadWrite("0.00", 0);
            yield return ReadWrite("255.00", 255); // Max
            yield return FailRead("256.00"); // Max + 1
        }
        public static IEnumerable<object[]> GetXData()
        {
            yield return ReadWrite("0", 0);
            yield return ReadWrite("FF", 255); // Max
            yield return FailRead("100"); // Max + 1
        }

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<byte> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<byte> data) => RunTest(data, new ByteUtf8Converter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TextTestData<byte> data) => RunTest(data, new ByteUtf8Converter('X'));
    }

    public class UInt16Tests : BaseTest<ushort>
    {
        public static IEnumerable<object[]> GetDData()
        {
            yield return FailRead("-1"); // Min -1
            yield return ReadWrite("0", 0);
            yield return ReadWrite("65535", 65535); // Max
            yield return FailRead("65536"); // Max + 1
        }
        public static IEnumerable<object[]> GetNData()
        {
            yield return FailRead("-1.00"); // Min -1
            yield return ReadWrite("0.00", 0);
            yield return ReadWrite("65,535.00", 65535); // Max
            yield return FailRead("65,536.00"); // Max + 1
        }
        public static IEnumerable<object[]> GetXData()
        {
            yield return ReadWrite("0", 0);
            yield return ReadWrite("FFFF", 65535); // Max
            yield return FailRead("10000"); // Max + 1
        }

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<ushort> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<ushort> data) => RunTest(data, new UInt16Utf8Converter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TextTestData<ushort> data) => RunTest(data, new UInt16Utf8Converter('X'));
    }

    public class UInt32Tests : BaseTest<uint>
    {
        public static IEnumerable<object[]> GetDData()
        {
            yield return FailRead("-1"); // Min -1
            yield return ReadWrite("0", 0);
            yield return ReadWrite("4294967295", 4294967295); // Max
            yield return FailRead("4294967296"); // Max + 1
        }
        public static IEnumerable<object[]> GetNData()
        {
            yield return FailRead("-1.00"); // Min -1
            yield return ReadWrite("0.00", 0);
            yield return ReadWrite("4,294,967,295.00", 4294967295); // Max
            yield return FailRead("4,294,967,296.00"); // Max + 1
        }
        public static IEnumerable<object[]> GetXData()
        {
            yield return ReadWrite("0", 0);
            yield return ReadWrite("FFFFFFFF", 4294967295); // Max
            yield return FailRead("100000000"); // Max + 1
        }

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<uint> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<uint> data) => RunTest(data, new UInt32Utf8Converter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TextTestData<uint> data) => RunTest(data, new UInt32Utf8Converter('X'));
    }

    public class UInt64Tests : BaseTest<ulong>
    {
        public static IEnumerable<object[]> GetDData()
        {
            yield return FailRead("-1"); // Min -1
            yield return ReadWrite("0", 0);
            yield return ReadWrite("18446744073709551615", 18446744073709551615); // Max
            yield return FailRead("18446744073709551616"); // Max + 1
        }
        public static IEnumerable<object[]> GetNData()
        {
            yield return FailRead("-1.00"); // Min -1
            yield return ReadWrite("0.00", 0);
            yield return ReadWrite("18,446,744,073,709,551,615.00", 18446744073709551615); // Max
            yield return FailRead("18,446,744,073,709,551,616.00"); // Max + 1
        }
        public static IEnumerable<object[]> GetXData()
        {
            yield return ReadWrite("0", 0);
            yield return ReadWrite("FFFFFFFFFFFFFFFF", 18446744073709551615); // Max
            yield return FailRead("10000000000000000"); // Max + 1
        }

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<ulong> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<ulong> data) => RunTest(data, new UInt64Utf8Converter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TextTestData<ulong> data) => RunTest(data, new UInt64Utf8Converter('X'));
    }
}
