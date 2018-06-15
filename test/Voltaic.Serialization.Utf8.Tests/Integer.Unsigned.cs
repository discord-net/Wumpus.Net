using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Utf8.Tests
{
    public class ByteTests : BaseTest<byte>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return Fail("-1"); // Min -1
            yield return ReadWrite("0", 0);
            yield return ReadWrite("255", 255); // Max
            yield return Fail("256"); // Max + 1
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TestData data) => RunTest(data);
    }

    public class UInt16Tests : BaseTest<ushort>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return Fail("-1"); // Min -1
            yield return ReadWrite("0", 0);
            yield return ReadWrite("65535", 65535); // Max
            yield return Fail("65536"); // Max + 1
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TestData data) => RunTest(data);
    }

    public class UInt32Tests : BaseTest<uint>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return Fail("-1"); // Min -1
            yield return ReadWrite("0", 0);
            yield return ReadWrite("4294967295", 4294967295); // Max
            yield return Fail("4294967296"); // Max + 1
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TestData data) => RunTest(data);
    }

    public class UInt64Tests : BaseTest<ulong>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return Fail("-1"); // Min -1
            yield return ReadWrite("0", 0);
            yield return ReadWrite("18446744073709551615", 18446744073709551615); // Max
            yield return Fail("18446744073709551616"); // Max + 1
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TestData data) => RunTest(data);
    }
}