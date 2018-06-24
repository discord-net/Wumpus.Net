using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public class ModifyGuildMemberParams
    {
        /// <summary> xxx </summary>
        [ModelProperty("mute")]
        public Optional<bool> Mute { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("deaf")]
        public Optional<bool> Deaf { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("nick")]
        public Optional<string> Nickname { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("roles")]
        public Optional<Snowflake[]> RoleIds { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("channel_id")]
        public Optional<Snowflake> ChannelId { get; set; }

        public void Validate()
        {
            Preconditions.NotNullOrWhitespace(Nickname, nameof(Nickname));
        }
    }
}
