using System.Collections.Generic;
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
                Content = "test",
                Embed = new Embed { Author = new EmbedAuthor { Name = "testtest" }, Url = "http://discordapp.com" },
                IsTTS = true
            }), new Message
            {
                Content = "test",
                Embeds = new[] { new Embed { Author = new EmbedAuthor { Name = "testtest" }, Url = "http://discordapp.com" } },
                IsTextToSpeech = true
            });
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Request(TestData<Message> data) => RunTest(data);
    }
}
