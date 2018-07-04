using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/invite#invite-object-example-invite-object </summary>
    public class InviteGuild
    {
        /// <summary> The id of the <see cref="InviteGuild"/>. </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> The name of the <see cref="InviteGuild"/>. </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> The splash hash of the <see cref="InviteGuild"/> </summary>
        [ModelProperty("splash")]
        public Utf8String Splash { get; set; }
        /// <summary> The icon hash of the <see cref="InviteGuild"/> </summary>
        [ModelProperty("icon")]
        public Utf8String Icon { get; set; }
    }
}
