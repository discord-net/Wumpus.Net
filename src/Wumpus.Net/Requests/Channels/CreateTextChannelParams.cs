using Wumpus.Entities;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class CreateTextChannelParams : CreateGuildChannelParams
    {
        public CreateTextChannelParams(string name)
            : base(name, ChannelType.Text)
        {
        }
    }
}
