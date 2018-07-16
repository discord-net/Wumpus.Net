using Wumpus.Requests;
using Xunit;

namespace Wumpus.Rest.Tests
{
    public class EmojiTests : BaseTest
    {
        [Fact]
        public void GetGuildEmojisAsync()
        {
            RunTest(c => c.GetGuildEmojisAsync(123));
        }
        [Fact]
        public void GetGuildEmojiAsync()
        {
            RunTest(c => c.GetGuildEmojiAsync(123, 456), x =>
            {
                Assert.Equal(456UL, x.Id.Value.RawValue);
            });
        }
        [Fact]
        public void CreateGuildEmojiAsync()
        {
            RunTest(c => c.CreateGuildEmojiAsync(123, new CreateGuildEmojiParams
            {
                // TODO: Impl
            }), x =>
            {
                // TODO: Impl
            });
        }
        [Fact]
        public void ModifyGuildEmojiAsync()
        {
            RunTest(c => c.ModifyGuildEmojiAsync(123, 456, new ModifyGuildEmojiParams
            {
                // TODO: Impl
            }), x =>
            {
                // TODO: Impl
            });
        }
        [Fact]
        public void DeleteGuildEmojiAsync()
        {
            RunTest(c => c.DeleteGuildEmojiAsync(123, 456));
        }
    }
}
