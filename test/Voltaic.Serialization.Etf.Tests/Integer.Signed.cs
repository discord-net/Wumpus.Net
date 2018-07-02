using System;
using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Etf.Tests
{
    public class SByteTests : BaseTest<sbyte>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            throw new NotImplementedException();
        }
        public static IEnumerable<object[]> GetDData() => TextToBinary(Utf8.Tests.SByteTests.GetDData());
        public static IEnumerable<object[]> GetNData() => TextToBinary(Utf8.Tests.SByteTests.GetNData());
        public static IEnumerable<object[]> GetXData() => TextToBinary(Utf8.Tests.SByteTests.GetXData());

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(BinaryTestData<sbyte> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(BinaryTestData<sbyte> data) => RunTest(data, new SByteEtfConverter('D'));
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(BinaryTestData<sbyte> data) => RunTest(data, new SByteEtfConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(BinaryTestData<sbyte> data) => RunTest(data, new SByteEtfConverter('X'));
    }

    public class Int16Tests : BaseTest<short>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            throw new NotImplementedException();
        }
        public static IEnumerable<object[]> GetDData() => TextToBinary(Utf8.Tests.Int16Tests.GetDData());
        public static IEnumerable<object[]> GetNData() => TextToBinary(Utf8.Tests.Int16Tests.GetNData());
        public static IEnumerable<object[]> GetXData() => TextToBinary(Utf8.Tests.Int16Tests.GetXData());

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(BinaryTestData<short> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(BinaryTestData<short> data) => RunTest(data, new Int16EtfConverter('D'));
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(BinaryTestData<short> data) => RunTest(data, new Int16EtfConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(BinaryTestData<short> data) => RunTest(data, new Int16EtfConverter('X'));
    }

    public class Int32Tests : BaseTest<int>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            throw new NotImplementedException();
        }
        public static IEnumerable<object[]> GetDData() => TextToBinary(Utf8.Tests.Int32Tests.GetDData());
        public static IEnumerable<object[]> GetNData() => TextToBinary(Utf8.Tests.Int32Tests.GetNData());
        public static IEnumerable<object[]> GetXData() => TextToBinary(Utf8.Tests.Int32Tests.GetXData());

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(BinaryTestData<int> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(BinaryTestData<int> data) => RunTest(data, new Int32EtfConverter('D'));
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(BinaryTestData<int> data) => RunTest(data, new Int32EtfConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(BinaryTestData<int> data) => RunTest(data, new Int32EtfConverter('X'));
    }

    public class Int64Tests : BaseTest<long>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            throw new NotImplementedException();
        }
        public static IEnumerable<object[]> GetDData() => TextToBinary(Utf8.Tests.Int64Tests.GetDData());
        public static IEnumerable<object[]> GetNData() => TextToBinary(Utf8.Tests.Int64Tests.GetNData());
        public static IEnumerable<object[]> GetXData() => TextToBinary(Utf8.Tests.Int64Tests.GetXData());

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(BinaryTestData<long> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(BinaryTestData<long> data) => RunTest(data, new Int64EtfConverter('D'));
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(BinaryTestData<long> data) => RunTest(data, new Int64EtfConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(BinaryTestData<long> data) => RunTest(data, new Int64EtfConverter('X'));
    }
}
