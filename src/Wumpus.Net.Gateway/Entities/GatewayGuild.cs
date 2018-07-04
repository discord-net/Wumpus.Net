using Voltaic.Serialization;
using System;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class GatewayGuild : Guild
    {
        /// <summary> xxx </summary>
        [ModelProperty("unavailable")]
        public bool? Unavailable { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("member_count")]
        public int MemberCount { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("large")]
        public bool Large { get; set; }

        /// <summary> xxx </summary>
        [ModelProperty("presences")]
        public Presence[] Presences { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("members")]
        public GuildMember[] Members { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("channels")]
        public Channel[] Channels { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("joined_at"), StandardFormat('O')]
        public DateTimeOffset JoinedAt { get; set; }
    }
}
