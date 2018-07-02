using System;
using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Etf.Tests
{
    public class SingleTests : BaseTest<float>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            throw new NotImplementedException();
        }
        public static IEnumerable<object[]> GetGData() => TextToBinary(Utf8.Tests.SingleTests.GetGData());
        public static IEnumerable<object[]> GetLittleGData() => TextToBinary(Utf8.Tests.SingleTests.GetLittleGData());
        public static IEnumerable<object[]> GetFData() => TextToBinary(Utf8.Tests.SingleTests.GetFData());
        public static IEnumerable<object[]> GetEData() => TextToBinary(Utf8.Tests.SingleTests.GetEData());
        public static IEnumerable<object[]> GetLittleEData() => TextToBinary(Utf8.Tests.SingleTests.GetLittleEData());

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(BinaryTestData<float> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetLittleGData))]
        public void Format_LittleG(BinaryTestData<float> data) => RunTest(data, new SingleEtfConverter('g'));
        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_G(BinaryTestData<float> data) => RunTest(data, new SingleEtfConverter('G'));
        [Theory]
        [MemberData(nameof(GetFData))]
        public void Format_F(BinaryTestData<float> data) => RunTest(data, new SingleEtfConverter('F'));
        [Theory]
        [MemberData(nameof(GetEData))]
        public void Format_E(BinaryTestData<float> data) => RunTest(data, new SingleEtfConverter('E'));
        [Theory]
        [MemberData(nameof(GetLittleEData))]
        public void Format_LittleE(BinaryTestData<float> data) => RunTest(data, new SingleEtfConverter('e'));
    }

    public class DoubleTests : BaseTest<double>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            throw new NotImplementedException();
        }
        public static IEnumerable<object[]> GetGData() => TextToBinary(Utf8.Tests.DoubleTests.GetGData());
        public static IEnumerable<object[]> GetLittleGData() => TextToBinary(Utf8.Tests.DoubleTests.GetLittleGData());
        public static IEnumerable<object[]> GetFData() => TextToBinary(Utf8.Tests.DoubleTests.GetFData());
        public static IEnumerable<object[]> GetEData() => TextToBinary(Utf8.Tests.DoubleTests.GetEData());
        public static IEnumerable<object[]> GetLittleEData() => TextToBinary(Utf8.Tests.DoubleTests.GetLittleEData());

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(BinaryTestData<double> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetLittleGData))]
        public void Format_LittleG(BinaryTestData<double> data) => RunTest(data, new DoubleEtfConverter('g'));
        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_G(BinaryTestData<double> data) => RunTest(data, new DoubleEtfConverter('G'));
        [Theory]
        [MemberData(nameof(GetFData))]
        public void Format_F(BinaryTestData<double> data) => RunTest(data, new DoubleEtfConverter('F'));
        [Theory]
        [MemberData(nameof(GetEData))]
        public void Format_E(BinaryTestData<double> data) => RunTest(data, new DoubleEtfConverter('E'));
        [Theory]
        [MemberData(nameof(GetLittleEData))]
        public void Format_LittleE(BinaryTestData<double> data) => RunTest(data, new DoubleEtfConverter('e'));
    }

    public class DecimalTests : BaseTest<decimal>
    {
        public static IEnumerable<object[]> GetGData() => TextToBinary(Utf8.Tests.DecimalTests.GetGData());
        public static IEnumerable<object[]> GetFData() => TextToBinary(Utf8.Tests.DecimalTests.GetFData());
        public static IEnumerable<object[]> GetEData() => TextToBinary(Utf8.Tests.DecimalTests.GetEData());
        public static IEnumerable<object[]> GetLittleEData() => TextToBinary(Utf8.Tests.DecimalTests.GetLittleEData());

        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_G(BinaryTestData<decimal> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetFData))]
        public void Format_F(BinaryTestData<decimal> data) => RunTest(data, new DecimalEtfConverter('F'));
        [Theory]
        [MemberData(nameof(GetEData))]
        public void Format_E(BinaryTestData<decimal> data) => RunTest(data, new DecimalEtfConverter('E'));
        [Theory]
        [MemberData(nameof(GetLittleEData))]
        public void Format_LittleE(BinaryTestData<decimal> data) => RunTest(data, new DecimalEtfConverter('e'));
    }
}
