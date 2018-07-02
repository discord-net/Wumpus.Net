using System.Collections.Generic;
using Voltaic.Serialization.Utf8.Tests;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class SByteTests : BaseTest<sbyte>
    {
        public static IEnumerable<object[]> GetDData() => Utf8.Tests.SByteTests.GetDData();
        public static IEnumerable<object[]> GetNData() => Utf8.Tests.SByteTests.GetNData();
        public static IEnumerable<object[]> GetXData() => Utf8.Tests.SByteTests.GetXData();

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Number(TextTestData<sbyte> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<sbyte> data) => RunQuoteTest(data, onlyReads: true);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<sbyte> data) => RunQuoteTest(data, new SByteJsonConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TextTestData<sbyte> data) => RunQuoteTest(data, new SByteJsonConverter('X'));
    }

    public class Int16Tests : BaseTest<short>
    {
        public static IEnumerable<object[]> GetDData() => Utf8.Tests.Int16Tests.GetDData();
        public static IEnumerable<object[]> GetNData() => Utf8.Tests.Int16Tests.GetNData();
        public static IEnumerable<object[]> GetXData() => Utf8.Tests.Int16Tests.GetXData();

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Number(TextTestData<short> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<short> data) => RunQuoteTest(data, onlyReads: true);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<short> data) => RunQuoteTest(data, new Int16JsonConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TextTestData<short> data) => RunQuoteTest(data, new Int16JsonConverter('X'));
    }

    public class Int32Tests : BaseTest<int>
    {
        public static IEnumerable<object[]> GetDData() => Utf8.Tests.Int32Tests.GetDData();
        public static IEnumerable<object[]> GetNData() => Utf8.Tests.Int32Tests.GetNData();
        public static IEnumerable<object[]> GetXData() => Utf8.Tests.Int32Tests.GetXData();

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Number(TextTestData<int> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<int> data) => RunQuoteTest(data, onlyReads: true);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<int> data) => RunQuoteTest(data, new Int32JsonConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TextTestData<int> data) => RunQuoteTest(data, new Int32JsonConverter('X'));
    }

    public class Int53Tests : BaseTest<long>
    {
        public static IEnumerable<object[]> GetDData() => Utf8.Tests.Int64Tests.GetDData();
        public static IEnumerable<object[]> GetNData() => Utf8.Tests.Int64Tests.GetNData();
        public static IEnumerable<object[]> GetXData() => Utf8.Tests.Int64Tests.GetXData();

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Number(TextTestData<long> data) => RunTest(data, new Int53JsonConverter());
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<long> data) => RunQuoteTest(data, new Int53JsonConverter(), onlyReads: true);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<long> data) => RunQuoteTest(data, new Int53JsonConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TextTestData<long> data) => RunQuoteTest(data, new Int53JsonConverter('X'));
    }

    public class Int64Tests : BaseTest<long>
    {
        public static IEnumerable<object[]> GetDData() => Utf8.Tests.Int64Tests.GetDData();
        public static IEnumerable<object[]> GetNData() => Utf8.Tests.Int64Tests.GetNData();
        public static IEnumerable<object[]> GetXData() => Utf8.Tests.Int64Tests.GetXData();

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<long> data) => RunQuoteTest(data);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<long> data) => RunQuoteTest(data, new Int64JsonConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TextTestData<long> data) => RunQuoteTest(data, new Int64JsonConverter('X'));
    }
}
