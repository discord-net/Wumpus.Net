using Voltaic;
using Wumpus.Entities;
using Xunit;

namespace Wumpus.Rest.Tests
{
    public class MessageTests : BaseTest
    {
        [Fact]
        public void CreateMessageAsync()
        {
            RunTest(c => c.CreateMessageAsync(123, new Requests.CreateMessageParams
            {
                Content = (Utf8String)"test",
                Embed = new Embed { Author = new EmbedAuthor { Name = (Utf8String)"testtest" }, Url = (Utf8String)"http://discordapp.com" },
                IsTTS = true
            }), new Message
            {
                Id = 123,
                ChannelId = 123,
                Content = (Utf8String)"test",
                Embeds = new[] { new Embed { Author = new EmbedAuthor { Name = (Utf8String)"testtest" }, Url = (Utf8String)"http://discordapp.com" } },
                IsTextToSpeech = true
            });
        }
    }
}
