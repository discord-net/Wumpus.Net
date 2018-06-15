using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Utf8.Tests
{
    public class SingleTests : BaseTest<float>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return Fail("-3.402824e38"); // Min - 1e38
            yield return Read("-3.402823e38", -3.402823e38f); // Min
            yield return ReadWrite("-3.402823E+38", -3.402823e38f); // Min
            yield return Fail("-1e");
            yield return Fail("-0.01e");
            yield return ReadWrite("0", 0);
            yield return Fail("0.01e");
            yield return Fail("1e");
            yield return ReadWrite("3.402823E+38", 3.402823e38f); // Max
            yield return Read("3.402823e38", 3.402823e38f); // Max
            yield return Fail("3.402824e38"); // Max + 1e38

            yield return ReadWrite("Infinity", float.PositiveInfinity);
            yield return ReadWrite("-Infinity", float.NegativeInfinity);
            yield return ReadWrite("NaN", float.NaN);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TestData data) => RunTest(data);
    }

    public class DoubleTests : BaseTest<double>
    {
        public static IEnumerable<object[]> GetData()
        {
            // yield return Fail("-3.402824e38"); // Min - 1e38
            // yield return Read("-3.402823e38",-3.402823e38f); // Min
            // yield return ReadWrite("-3.402823E+38",-3.402823e38f); // Min
            yield return Fail("-1e");
            yield return Fail("-0.01e");
            yield return ReadWrite("0", 0);
            yield return Fail("0.01e");
            yield return Fail("1e");
            // yield return ReadWrite("3.402823E+38", 3.402823e38f); // Max
            // yield return Read("3.402823e38", 3.402823e38f); // Max
            // yield return Fail("3.402824e38"); // Max + 1e38

            yield return ReadWrite("Infinity", float.PositiveInfinity);
            yield return ReadWrite("-Infinity", float.NegativeInfinity);
            yield return ReadWrite("NaN", float.NaN);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TestData data) => RunTest(data);
    }

    public class DecimalTests : BaseTest<decimal>
    {
        public static IEnumerable<object[]> GetData()
        {
            // yield return Fail("-3.402824e38"); // Min - 1e38
            // yield return Read("-3.402823e38",-3.402823e38f); // Min
            // yield return ReadWrite("-3.402823E+38",-3.402823e38f); // Min
            yield return Fail("-1e");
            yield return Fail("-0.01e");
            yield return ReadWrite("0", 0);
            yield return Fail("0.01e");
            yield return Fail("1e");
            // yield return ReadWrite("3.402823E+38", 3.402823e38f); // Max
            // yield return Read("3.402823e38", 3.402823e38f); // Max
            // yield return Fail("3.402824e38"); // Max + 1e38
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TestData data) => RunTest(data);
    }
}
