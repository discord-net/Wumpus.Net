using System;
using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Utf8.Tests
{
    public class GuidTests : BaseTest<Guid>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return Fail("");
            yield return Fail("A-B-C-D-E");
            yield return Fail("CB0AFB61-6F04-401A-BBEA-G0FC0B6E4E51"); // G
            yield return Read("CB0AFB61-6F04-401A-BBEA-C0FC0B6E4E51", Guid.Parse("CB0AFB61-6F04-401A-BBEA-C0FC0B6E4E51"));
            yield return Read("FC1911F9-9EED-4CA8-AC8B-CEEE1EBE2C72", Guid.Parse("FC1911F9-9EED-4CA8-AC8B-CEEE1EBE2C72"));
            yield return ReadWrite("cb0afb61-6f04-401a-bbea-c0fc0b6e4e51", Guid.Parse("cb0afb61-6f04-401a-bbea-c0fc0b6e4e51"));
            yield return ReadWrite("fc1911f9-9eed-4ca8-ac8b-ceee1ebe2c72", Guid.Parse("fc1911f9-9eed-4ca8-ac8b-ceee1ebe2c72"));
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void TestGuid(TestData data) => Test(data);
    }
}
