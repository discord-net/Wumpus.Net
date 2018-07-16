using System;
using Wumpus.Entities;
using Wumpus.Requests;
using Xunit;

namespace Wumpus.Rest.Tests
{
    public class AuditLogTests : BaseTest
    {
        [Fact]
        public void GetGuildAuditLogAsync()
        {
            RunTest(c => c.GetGuildAuditLogAsync(123, new GetGuildAuditLogParams
            {
                ActionType = AuditLogEvent.ChannelCreate,
                Before = new Snowflake(DateTime.UtcNow),
                Limit = 10,
                UserId = new Snowflake(123)
            }), x =>
            {
                Assert.Equal(AuditLogEvent.ChannelCreate, x.Entries[0].ActionType);
                Assert.True(x.Entries[0].Id.ToDateTime() <= DateTime.UtcNow);
                Assert.True(x.Entries.Length < 10);
                Assert.Equal(123UL, x.Entries[0].UserId.RawValue);
            });
        }
    }
}
