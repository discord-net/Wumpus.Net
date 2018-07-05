using System;
using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Utf8.Tests
{
    public class GuidTests : BaseTest<Guid>
    {
        public static IEnumerable<object[]> GetDData()
        {
            yield return FailRead("");
            yield return FailRead("A-B-C-D-E");
            yield return FailRead("CB0AFB61-6F04-401A-BBEA-G0FC0B6E4E51"); // G
            yield return Read("CB0AFB61-6F04-401A-BBEA-C0FC0B6E4E51", Guid.Parse("CB0AFB61-6F04-401A-BBEA-C0FC0B6E4E51"));
            yield return Read("FC1911F9-9EED-4CA8-AC8B-CEEE1EBE2C72", Guid.Parse("FC1911F9-9EED-4CA8-AC8B-CEEE1EBE2C72"));
            yield return ReadWrite("cb0afb61-6f04-401a-bbea-c0fc0b6e4e51", Guid.Parse("cb0afb61-6f04-401a-bbea-c0fc0b6e4e51"));
            yield return ReadWrite("fc1911f9-9eed-4ca8-ac8b-ceee1ebe2c72", Guid.Parse("fc1911f9-9eed-4ca8-ac8b-ceee1ebe2c72"));
        }
        public static IEnumerable<object[]> GetBData()
        {
            yield return FailRead("");
            yield return FailRead("{}");
            yield return FailRead("{A-B-C-D-E}");
            yield return FailRead("{CB0AFB61-6F04-401A-BBEA-G0FC0B6E4E51}"); // G
            yield return Read("{CB0AFB61-6F04-401A-BBEA-C0FC0B6E4E51}", Guid.Parse("CB0AFB61-6F04-401A-BBEA-C0FC0B6E4E51"));
            yield return Read("{FC1911F9-9EED-4CA8-AC8B-CEEE1EBE2C72}", Guid.Parse("FC1911F9-9EED-4CA8-AC8B-CEEE1EBE2C72"));
            yield return ReadWrite("{cb0afb61-6f04-401a-bbea-c0fc0b6e4e51}", Guid.Parse("cb0afb61-6f04-401a-bbea-c0fc0b6e4e51"));
            yield return ReadWrite("{fc1911f9-9eed-4ca8-ac8b-ceee1ebe2c72}", Guid.Parse("fc1911f9-9eed-4ca8-ac8b-ceee1ebe2c72"));
        }
        public static IEnumerable<object[]> GetPData()
        {
            yield return FailRead("");
            yield return FailRead("()");
            yield return FailRead("(A-B-C-D-E)");
            yield return FailRead("(CB0AFB61-6F04-401A-BBEA-G0FC0B6E4E51)"); // G
            yield return Read("(CB0AFB61-6F04-401A-BBEA-C0FC0B6E4E51)", Guid.Parse("CB0AFB61-6F04-401A-BBEA-C0FC0B6E4E51"));
            yield return Read("(FC1911F9-9EED-4CA8-AC8B-CEEE1EBE2C72)", Guid.Parse("FC1911F9-9EED-4CA8-AC8B-CEEE1EBE2C72"));
            yield return ReadWrite("(cb0afb61-6f04-401a-bbea-c0fc0b6e4e51)", Guid.Parse("cb0afb61-6f04-401a-bbea-c0fc0b6e4e51"));
            yield return ReadWrite("(fc1911f9-9eed-4ca8-ac8b-ceee1ebe2c72)", Guid.Parse("fc1911f9-9eed-4ca8-ac8b-ceee1ebe2c72"));
        }
        public static IEnumerable<object[]> GetNData()
        {
            yield return FailRead("");
            yield return FailRead("ABCDE");
            yield return FailRead("CB0AFB616F04401ABBEAG0FC0B6E4E51"); // G
            yield return Read("CB0AFB616F04401ABBEAC0FC0B6E4E51", Guid.Parse("CB0AFB616F04401ABBEAC0FC0B6E4E51"));
            yield return Read("FC1911F99EED4CA8AC8BCEEE1EBE2C72", Guid.Parse("FC1911F99EED4CA8AC8BCEEE1EBE2C72"));
            yield return ReadWrite("cb0afb616f04401abbeac0fc0b6e4e51", Guid.Parse("cb0afb616f04401abbeac0fc0b6e4e51"));
            yield return ReadWrite("fc1911f99eed4ca8ac8bceee1ebe2c72", Guid.Parse("fc1911f99eed4ca8ac8bceee1ebe2c72"));
        }

        [Theory]
        [MemberData(nameof(GetDData))]
        public void Format_D(TextTestData<Guid> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetBData))]
        public void Format_B(TextTestData<Guid> data) => RunTest(data, new GuidUtf8Converter('B'));
        [Theory]
        [MemberData(nameof(GetPData))]
        public void Format_P(TextTestData<Guid> data) => RunTest(data, new GuidUtf8Converter('P'));
        [Theory]
        [MemberData(nameof(GetNData))]
        public void Format_N(TextTestData<Guid> data) => RunTest(data, new GuidUtf8Converter('N'));
    }
}
