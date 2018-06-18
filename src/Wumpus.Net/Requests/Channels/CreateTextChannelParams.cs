using Wumpus.Entities;

namespace Wumpus.Requests
{
    public class CreateTextChannelParams : CreateGuildChannelParams
    {
        public CreateTextChannelParams(string name)
            : base(name, ChannelType.Text)
        {
        }
    }
}
