using Voltaic;
using Wumpus.Entities;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class CreateTextChannelParams : CreateGuildChannelParams
    {
        public CreateTextChannelParams(Utf8String name)
            : base(name, ChannelType.Text)
        {
        }
    }
}
