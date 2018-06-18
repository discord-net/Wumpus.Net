using Voltaic.Serialization;
using System;
using Voltaic;

namespace Wumpus.Entities
{
    public class Channel
    {
        //Shared
        [ModelProperty("id")]
        public ulong Id { get; set; }
        [ModelProperty("type")]
        public ChannelType Type { get; set; }

        //GuildChannel
        [ModelProperty("guild_id")]
        public Optional<ulong> GuildId { get; set; }
        [ModelProperty("position")]
        public Optional<int> Position { get; set; }
        [ModelProperty("permission_overwrites")]
        public Optional<Overwrite[]> PermissionOverwrites { get; set; }
        [ModelProperty("name")]
        public Optional<string> Name { get; set; }

        //TextChannel
        [ModelProperty("topic")]
        public Optional<string> Topic { get; set; }

        //VoiceChannel
        [ModelProperty("bitrate")]
        public Optional<int> Bitrate { get; set; }
        [ModelProperty("user_limit")]
        public Optional<int> UserLimit { get; set; }
        
        //PrivateChannel
        [ModelProperty("recipients")]
        public Optional<User[]> Recipients { get; set; }
        [ModelProperty("owner_id")]
        public Optional<ulong> OwnerId { get; set; }
        [ModelProperty("application_id")]
        public Optional<ulong> ApplicationId { get; set; }

        //GroupChannel
        [ModelProperty("icon")]
        public Optional<string> Icon { get; set; }

        //MessageChannel
        [ModelProperty("last_message_id")]
        public Optional<ulong?> LastMessageId { get; set; }
        [ModelProperty("last_pin_timestamp")]
        public Optional<DateTimeOffset?> LastPinTimestamp { get; set; }
    }
}
