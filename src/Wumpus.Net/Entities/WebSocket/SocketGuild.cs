using Voltaic.Serialization;
using System;

namespace Wumpus.Entities
{
    public class SocketGuild : Guild
    {
        [ModelProperty("unavailable")]
        public bool? Unavailable { get; set; }
        [ModelProperty("member_count")]
        public int MemberCount { get; set; }
        [ModelProperty("large")]
        public bool Large { get; set; }

        [ModelProperty("presences")]
        public Presence[] Presences { get; set; }
        [ModelProperty("members")]
        public GuildMember[] Members { get; set; }
        [ModelProperty("channels")]
        public Channel[] Channels { get; set; }
        [ModelProperty("joined_at")]
        public DateTimeOffset JoinedAt { get; set; }
    }
}
