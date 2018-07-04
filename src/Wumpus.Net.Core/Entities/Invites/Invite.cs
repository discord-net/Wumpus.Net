using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/invite#invite-resource </summary>
    public class Invite
    {
        /// <summary> The <see cref="Invite"/> code. </summary>
        /// <remarks> Unique id. </remarks>
        [ModelProperty("code")]
        public Utf8String Code { get; set; }
        /// <summary> The (partial) <see cref="InviteGuild"/> this <see cref="Invite"/> is for. </summary>
        [ModelProperty("guild")]
        public InviteGuild Guild { get; set; }
        /// <summary> The (partial) <see cref="InviteChannel"/> this <see cref="Invite"/> is for. </summary>
        [ModelProperty("channel")]
        public InviteChannel Channel { get; set; }
        /// <summary> Approxmiate count of online <see cref="GuildMember"/>s. </summary>
        [ModelProperty("approximate_presence_count")]
        public Optional<int> ApproximatePresenceCount { get; set; }
        /// <summary> Approxmiate count of total <see cref="GuildMember"/>s. </summary>
        [ModelProperty("approximate_member_count")]
        public Optional<int> ApproximateMemberCount { get; set; }
    }
}
