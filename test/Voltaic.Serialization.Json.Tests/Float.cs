using System.Collections.Generic;
using Voltaic.Serialization.Utf8.Tests;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class SingleTests : BaseTest<float>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            foreach (var data in Utf8.Tests.SingleTests.GetLittleGData())
            {
                var value = (data[0] as TextTestData<float>).Value;
                if (float.IsFinite(value))
                    yield return data;
            }

            yield return Write("\"Infinity\"", float.PositiveInfinity);
            yield return Write("\"-Infinity\"", float.NegativeInfinity);
            yield return Write("\"NaN\"", float.NaN);
        }
        public static IEnumerable<object[]> GetGData() => Utf8.Tests.SingleTests.GetGData();
        public static IEnumerable<object[]> GetLittleGData() => Utf8.Tests.SingleTests.GetLittleGData();
        public static IEnumerable<object[]> GetFData() => Utf8.Tests.SingleTests.GetFData();
        public static IEnumerable<object[]> GetEData() => Utf8.Tests.SingleTests.GetEData();
        public static IEnumerable<object[]> GetLittleEData() => Utf8.Tests.SingleTests.GetLittleEData();

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(TextTestData<float> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetLittleGData))]
        public void Format_LittleG(TextTestData<float> data) => RunQuoteTest(data, onlyReads: true);
        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_G(TextTestData<float> data) => RunQuoteTest(data, new SingleJsonConverter('G'));
        [Theory]
        [MemberData(nameof(GetFData))]
        public void Format_F(TextTestData<float> data) => RunQuoteTest(data, new SingleJsonConverter('F'));
        [Theory]
        [MemberData(nameof(GetEData))]
        public void Format_E(TextTestData<float> data) => RunQuoteTest(data, new SingleJsonConverter('E'));
        [Theory]
        [MemberData(nameof(GetLittleEData))]
        public void Format_LittleE(TextTestData<float> data) => RunQuoteTest(data, new SingleJsonConverter('e'));
    }

    public class DoubleTests : BaseTest<double>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            foreach (var data in Utf8.Tests.DoubleTests.GetLittleGData())
            {
                var value = (data[0] as TextTestData<double>).Value;
                if (double.IsFinite(value))
                    yield return data;
            }

            yield return Write("\"Infinity\"", double.PositiveInfinity);
            yield return Write("\"-Infinity\"", double.NegativeInfinity);
            yield return Write("\"NaN\"", double.NaN);
        }
        public static IEnumerable<object[]> GetGData() => Utf8.Tests.DoubleTests.GetGData();
        public static IEnumerable<object[]> GetLittleGData() => Utf8.Tests.DoubleTests.GetLittleGData();
        public static IEnumerable<object[]> GetFData() => Utf8.Tests.DoubleTests.GetFData();
        public static IEnumerable<object[]> GetEData() => Utf8.Tests.DoubleTests.GetEData();
        public static IEnumerable<object[]> GetLittleEData() => Utf8.Tests.DoubleTests.GetLittleEData();

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(TextTestData<double> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetLittleGData))]
        public void Format_LittleG(TextTestData<double> data) => RunQuoteTest(data, onlyReads: true);
        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_G(TextTestData<double> data) => RunQuoteTest(data, new DoubleJsonConverter('G'));
        [Theory]
        [MemberData(nameof(GetFData))]
        public void Format_F(TextTestData<double> data) => RunQuoteTest(data, new DoubleJsonConverter('F'));
        [Theory]
        [MemberData(nameof(GetEData))]
        public void Format_E(TextTestData<double> data) => RunQuoteTest(data, new DoubleJsonConverter('E'));
        [Theory]
        [MemberData(nameof(GetLittleEData))]
        public void Format_LittleE(TextTestData<double> data) => RunQuoteTest(data, new DoubleJsonConverter('e'));
    }

    public class DecimalTests : BaseTest<decimal>
    {
        public static IEnumerable<object[]> GetGData() => Utf8.Tests.DecimalTests.GetGData();
        public static IEnumerable<object[]> GetFData() => Utf8.Tests.DecimalTests.GetFData();
        public static IEnumerable<object[]> GetEData() => Utf8.Tests.DecimalTests.GetEData();
        public static IEnumerable<object[]> GetLittleEData() => Utf8.Tests.DecimalTests.GetLittleEData();

        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_G(TextTestData<decimal> data) => RunQuoteTest(data);
        [Theory]
        [MemberData(nameof(GetFData))]
        public void Format_F(TextTestData<decimal> data) => RunQuoteTest(data, new DecimalJsonConverter('F'));
        [Theory]
        [MemberData(nameof(GetEData))]
        public void Format_E(TextTestData<decimal> data) => RunQuoteTest(data, new DecimalJsonConverter('E'));
        [Theory]
        [MemberData(nameof(GetLittleEData))]
        public void Format_LittleE(TextTestData<decimal> data) => RunQuoteTest(data, new DecimalJsonConverter('e'));
    }
}
