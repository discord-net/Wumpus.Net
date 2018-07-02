using System.Collections.Generic;
using Voltaic.Serialization.Utf8.Tests;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class ByteTests : BaseTest<byte>
    {
        public static IEnumerable<object[]> GetDData() => Utf8.Tests.ByteTests.GetDData();
        public static IEnumerable<object[]> GetNData() => Utf8.Tests.ByteTests.GetNData();
        public static IEnumerable<object[]> GetXData() => Utf8.Tests.ByteTests.GetXData();

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Number(TextTestData<byte> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<byte> data) => RunQuoteTest(data, onlyReads: true);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<byte> data) => RunQuoteTest(data, new ByteJsonConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TextTestData<byte> data) => RunQuoteTest(data, new ByteJsonConverter('X'));
    }

    public class UInt16Tests : BaseTest<ushort>
    {
        public static IEnumerable<object[]> GetDData() => Utf8.Tests.UInt16Tests.GetDData();
        public static IEnumerable<object[]> GetNData() => Utf8.Tests.UInt16Tests.GetNData();
        public static IEnumerable<object[]> GetXData() => Utf8.Tests.UInt16Tests.GetXData();

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Number(TextTestData<ushort> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<ushort> data) => RunQuoteTest(data, onlyReads: true);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<ushort> data) => RunQuoteTest(data, new UInt16JsonConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TextTestData<ushort> data) => RunQuoteTest(data, new UInt16JsonConverter('X'));
    }

    public class UInt32Tests : BaseTest<uint>
    {
        public static IEnumerable<object[]> GetDData() => Utf8.Tests.UInt32Tests.GetDData();
        public static IEnumerable<object[]> GetNData() => Utf8.Tests.UInt32Tests.GetNData();
        public static IEnumerable<object[]> GetXData() => Utf8.Tests.UInt32Tests.GetXData();

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Number(TextTestData<uint> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<uint> data) => RunQuoteTest(data, onlyReads: true);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<uint> data) => RunQuoteTest(data, new UInt32JsonConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TextTestData<uint> data) => RunQuoteTest(data, new UInt32JsonConverter('X'));
    }

    public class UInt53Tests : BaseTest<ulong>
    {
        public static IEnumerable<object[]> GetDData() => Utf8.Tests.UInt64Tests.GetDData();
        public static IEnumerable<object[]> GetNData() => Utf8.Tests.UInt64Tests.GetNData();
        public static IEnumerable<object[]> GetXData() => Utf8.Tests.UInt64Tests.GetXData();

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Number(TextTestData<ulong> data) => RunTest(data, new UInt53JsonConverter());
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<ulong> data) => RunQuoteTest(data, new UInt53JsonConverter(), onlyReads: true);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<ulong> data) => RunQuoteTest(data, new UInt53JsonConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TextTestData<ulong> data) => RunQuoteTest(data, new UInt53JsonConverter('X'));
    }

    public class UInt64Tests : BaseTest<ulong>
    {
        public static IEnumerable<object[]> GetDData() => Utf8.Tests.UInt64Tests.GetDData();
        public static IEnumerable<object[]> GetNData() => Utf8.Tests.UInt64Tests.GetNData();
        public static IEnumerable<object[]> GetXData() => Utf8.Tests.UInt64Tests.GetXData();

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<ulong> data) => RunQuoteTest(data, onlyReads: true);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<ulong> data) => RunQuoteTest(data, new UInt64JsonConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TextTestData<ulong> data) => RunQuoteTest(data, new UInt64JsonConverter('X'));
    }
}
