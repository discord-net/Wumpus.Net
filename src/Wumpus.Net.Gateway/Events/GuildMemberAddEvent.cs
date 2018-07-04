using Wumpus.Entities;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> 
    ///     Sent when a new <see cref="User"/> joins a <see cref="Guild"/>. The inner payload is a <see cref="GuildMember"/> object, with an extra guild_id key. 
    ///     https://discordapp.com/developers/docs/topics/gateway#guild-member-add 
    /// </summary>
    public class GuildMemberAddEvent : GuildMember
    {
        /// <summary> Id of the <see cref="Guild"/>. </summary>
        [ModelProperty("guild_id")]
        public Snowflake GuildId { get; set; }
    }
}
