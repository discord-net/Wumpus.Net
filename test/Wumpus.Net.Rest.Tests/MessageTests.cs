using System.Collections.Generic;
using Voltaic;
using Wumpus.Entities;
using Xunit;

namespace Wumpus.Net.Tests
{
    public class MessageTests : BaseTest<Message>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return Test(c => c.CreateMessageAsync(123, new Requests.CreateMessageParams
            {
                Content = (Utf8String)"test",
                Embed = new Embed { Author = new EmbedAuthor { Name = (Utf8String)"testtest" }, Url = (Utf8String)"http://discordapp.com" },
                IsTTS = true
            }), new Message
            {
                Content = (Utf8String)"test",
                Embeds = new[] { new Embed { Author = new EmbedAuthor { Name = (Utf8String)"testtest" }, Url = (Utf8String)"http://discordapp.com" } },
                IsTextToSpeech = true
            });
        }

        public MessageTests() : base(RecursiveComparer<Message>.Instance) { }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Request(TestData<Message> data) => RunTest(data);
    }
}
