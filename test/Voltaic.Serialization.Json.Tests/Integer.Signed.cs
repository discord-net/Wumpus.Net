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
        public void Number(TestData<sbyte> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TestData<sbyte> data) => RunQuoteTest(data, onlyReads: true);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TestData<sbyte> data) => RunQuoteTest(data, new SByteJsonConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TestData<sbyte> data) => RunQuoteTest(data, new SByteJsonConverter('X'));
    }

    public class Int16Tests : BaseTest<short>
    {
        public static IEnumerable<object[]> GetDData() => Utf8.Tests.Int16Tests.GetDData();
        public static IEnumerable<object[]> GetNData() => Utf8.Tests.Int16Tests.GetNData();
        public static IEnumerable<object[]> GetXData() => Utf8.Tests.Int16Tests.GetXData();

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Number(TestData<short> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TestData<short> data) => RunQuoteTest(data, onlyReads: true);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TestData<short> data) => RunQuoteTest(data, new Int16JsonConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TestData<short> data) => RunQuoteTest(data, new Int16JsonConverter('X'));
    }

    public class Int32Tests : BaseTest<int>
    {
        public static IEnumerable<object[]> GetDData() => Utf8.Tests.Int32Tests.GetDData();
        public static IEnumerable<object[]> GetNData() => Utf8.Tests.Int32Tests.GetNData();
        public static IEnumerable<object[]> GetXData() => Utf8.Tests.Int32Tests.GetXData();

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Number(TestData<int> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TestData<int> data) => RunQuoteTest(data, onlyReads: true);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TestData<int> data) => RunQuoteTest(data, new Int32JsonConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TestData<int> data) => RunQuoteTest(data, new Int32JsonConverter('X'));
    }

    public class Int53Tests : BaseTest<long>
    {
        public static IEnumerable<object[]> GetDData() => Utf8.Tests.Int64Tests.GetDData();
        public static IEnumerable<object[]> GetNData() => Utf8.Tests.Int64Tests.GetNData();
        public static IEnumerable<object[]> GetXData() => Utf8.Tests.Int64Tests.GetXData();

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Number(TestData<long> data) => RunTest(data, new Int53JsonConverter());
        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TestData<long> data) => RunQuoteTest(data, new Int53JsonConverter(), onlyReads: true);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TestData<long> data) => RunQuoteTest(data, new Int53JsonConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TestData<long> data) => RunQuoteTest(data, new Int53JsonConverter('X'));
    }

    public class Int64Tests : BaseTest<long>
    {
        public static IEnumerable<object[]> GetDData() => Utf8.Tests.Int64Tests.GetDData();
        public static IEnumerable<object[]> GetNData() => Utf8.Tests.Int64Tests.GetNData();
        public static IEnumerable<object[]> GetXData() => Utf8.Tests.Int64Tests.GetXData();

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TestData<long> data) => RunQuoteTest(data);
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TestData<long> data) => RunQuoteTest(data, new Int64JsonConverter('N'));
        [Theory]
        [MemberData(nameof(GetXData))]
        public void Format_X(TestData<long> data) => RunQuoteTest(data, new Int64JsonConverter('X'));
    }
}
