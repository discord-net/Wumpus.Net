using Voltaic.Serialization;
using System;
using Voltaic;

namespace Wumpus.Entities
{
    public class Channel
    {
        //Shared
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("type")]
        public ChannelType Type { get; set; }

        //GuildChannel
        /// <summary> xxx </summary>
        [ModelProperty("guild_id")]
        public Optional<Snowflake> GuildId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("position")]
        public Optional<int> Position { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("permission_overwrites")]
        public Optional<Overwrite[]> PermissionOverwrites { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Optional<Utf8String> Name { get; set; }

        //TextChannel
        /// <summary> xxx </summary>
        [ModelProperty("topic")]
        public Optional<Utf8String> Topic { get; set; }

        //VoiceChannel
        /// <summary> xxx </summary>
        [ModelProperty("bitrate")]
        public Optional<int> Bitrate { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("user_limit")]
        public Optional<int> UserLimit { get; set; }

        //PrivateChannel
        /// <summary> xxx </summary>
        [ModelProperty("recipients")]
        public Optional<User[]> Recipients { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("owner_id")]
        public Optional<Snowflake> OwnerId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("application_id")]
        public Optional<Snowflake> ApplicationId { get; set; }

        //GroupChannel
        /// <summary> xxx </summary>
        [ModelProperty("icon")]
        public Optional<Utf8String> Icon { get; set; }

        //MessageChannel
        /// <summary> xxx </summary>
        [ModelProperty("last_message_id")]
        public Optional<Snowflake?> LastMessageId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("last_pin_timestamp")]
        public Optional<DateTimeOffset?> LastPinTimestamp { get; set; }
    }
}
