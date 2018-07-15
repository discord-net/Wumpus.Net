using Voltaic.Serialization;
using System;
using Voltaic;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#guild-object </summary>
    public class GatewayGuild : Guild
    {
        /// <summary> Without the <see cref="GatewayGuild"/> Id. </summary>
        [ModelProperty("voice_states")]
        public Optional<VoiceState[]> VoiceStates { get; set; }
        /// <summary> Is this <see cref="GatewayGuild"/> unavailable? </summary>
        [ModelProperty("unavailable")]
        public Optional<bool> Unavailable { get; set; }
        /// <summary> Total number of <see cref="GuildMember"/> entities in this <see cref="GatewayGuild"/>. </summary>
        [ModelProperty("member_count")]
        public Optional<int> MemberCount { get; set; }
        /// <summary> Whether this is considered a large <see cref="GatewayGuild"/>. </summary>
        [ModelProperty("large")]
        public Optional<bool> Large { get; set; }
        /// <summary> <see cref="Presence"/> entities of the <see cref="GuildMember"/> entites in the <see cref="GatewayGuild"/>. </summary>
        [ModelProperty("presences")]
        public Optional<Presence[]> Presences { get; set; }
        /// <summary> <see cref="GuildMember"/> entities in the <see cref="GatewayGuild"/>. </summary>
        [ModelProperty("members")]
        public Optional<GuildMember[]> Members { get; set; }
        /// <summary> <see cref="Channel"/> entities in the <see cref="GatewayGuild"/>. </summary>
        [ModelProperty("channels")]
        public Optional<Channel[]> Channels { get; set; }
    }
}
