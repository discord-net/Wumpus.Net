using System;
using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Etf.Tests
{
    public class ByteTests : BaseTest<byte>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            throw new NotImplementedException();
        }
        public static IEnumerable<object[]> GetDData() => TextToBinary(Utf8.Tests.ByteTests.GetDData());
        public static IEnumerable<object[]> GetNData() => TextToBinary(Utf8.Tests.ByteTests.GetNData());
        public static IEnumerable<object[]> GetXData() => TextToBinary(Utf8.Tests.ByteTests.GetXData());

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(BinaryTestData<byte> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(BinaryTestData<byte> data) => RunTest(data, new ByteEtfConverter('D'));
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(BinaryTestData<byte> data) => RunTest(data, new ByteEtfConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(BinaryTestData<byte> data) => RunTest(data, new ByteEtfConverter('X'));
    }

    public class UInt16Tests : BaseTest<ushort>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            throw new NotImplementedException();
        }
        public static IEnumerable<object[]> GetDData() => TextToBinary(Utf8.Tests.UInt16Tests.GetDData());
        public static IEnumerable<object[]> GetNData() => TextToBinary(Utf8.Tests.UInt16Tests.GetNData());
        public static IEnumerable<object[]> GetXData() => TextToBinary(Utf8.Tests.UInt16Tests.GetXData());

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(BinaryTestData<ushort> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(BinaryTestData<ushort> data) => RunTest(data, new UInt16EtfConverter('D'));
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(BinaryTestData<ushort> data) => RunTest(data, new UInt16EtfConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(BinaryTestData<ushort> data) => RunTest(data, new UInt16EtfConverter('X'));
    }

    public class UInt32Tests : BaseTest<uint>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            throw new NotImplementedException();
        }
        public static IEnumerable<object[]> GetDData() => TextToBinary(Utf8.Tests.UInt32Tests.GetDData());
        public static IEnumerable<object[]> GetNData() => TextToBinary(Utf8.Tests.UInt32Tests.GetNData());
        public static IEnumerable<object[]> GetXData() => TextToBinary(Utf8.Tests.UInt32Tests.GetXData());

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(BinaryTestData<uint> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(BinaryTestData<uint> data) => RunTest(data, new UInt32EtfConverter('D'));
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(BinaryTestData<uint> data) => RunTest(data, new UInt32EtfConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(BinaryTestData<uint> data) => RunTest(data, new UInt32EtfConverter('X'));
    }

    public class UInt64Tests : BaseTest<ulong>
    {
        public static IEnumerable<object[]> GetNumberData()
        {
            throw new NotImplementedException();
        }
        public static IEnumerable<object[]> GetDData() => TextToBinary(Utf8.Tests.UInt64Tests.GetDData());
        public static IEnumerable<object[]> GetNData() => TextToBinary(Utf8.Tests.UInt64Tests.GetNData());
        public static IEnumerable<object[]> GetXData() => TextToBinary(Utf8.Tests.UInt64Tests.GetXData());

        [Theory]
        [MemberData(nameof(GetNumberData))]
        public void Number(BinaryTestData<ulong> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(BinaryTestData<ulong> data) => RunTest(data, new UInt64EtfConverter('D'));
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(BinaryTestData<ulong> data) => RunTest(data, new UInt64EtfConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(BinaryTestData<ulong> data) => RunTest(data, new UInt64EtfConverter('X'));
    }
}
