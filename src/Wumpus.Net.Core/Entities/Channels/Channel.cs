using Voltaic.Serialization;
using System;
using Voltaic;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel </summary>
    public class Channel
    {
        public const int MinChannelNameLength = 2;
        public const int MaxChannelNameLength = 100;

        public const int MinChannelTopicLength = 0;
        public const int MaxChannelTopicLength = 1024;

        public const int MinUserLimit = 0;
        public const int MaxUserLimit = 99;

        public const int MinBitrate = 8000;
        public const int MaxBitrate = 128000;

        public const int MinAfkTimeoutDuration = 60;
        public const int MaxAFkTimeoutDuration = 3600;

        public const int MinBulkMessageDeleteAmount = 2;
        public const int MaxBulkMessageDeleteAmount = 100;

        //Shared

        /// <summary> The id of this <see cref="Channel"/>. </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> The <see cref="ChannelType"/> of <see cref="Channel"/>. </summary>
        [ModelProperty("type")]
        public ChannelType Type { get; set; }

        //GuildChannel

        /// <summary> The id of the <see cref="Guild"/>. </summary>
        [ModelProperty("guild_id")]
        public Optional<Snowflake> GuildId { get; set; }
        /// <summary> Sorting position of the <see cref="Channel"/>. </summary>
        [ModelProperty("position")]
        public Optional<int> Position { get; set; }
        /// <summary> Id of the parent category for a <see cref="Channel"/>. </summary>
        [ModelProperty("parent_id")]
        public Optional<Snowflake?> ParentId { get; set; }
        /// <summary> Explicit <see cref="Overwrite"/>s for <see cref="GuildMember"/>s and <see cref="Role"/>s. </summary>
        [ModelProperty("permission_overwrites")]
        public Optional<Overwrite[]> PermissionOverwrites { get; set; }
        /// <summary> The name of the <see cref="Channel"/>. </summary>
        /// <remarks> 2-100 characters. </remarks>
        [ModelProperty("name")]
        public Optional<Utf8String> Name { get; set; }

        //TextChannel

        /// <summary> The <see cref="Channel"/> topic. </summary>
        /// <remarks> 0-1024 characters. </remarks>
        [ModelProperty("topic")]
        public Optional<Utf8String> Topic { get; set; }
        /// <summary> If the <see cref="Channel"/> is nsfw </summary>
        [ModelProperty("nsfw")]
        public Optional<bool> IsNsfw { get; set; }

        //MessageChannel

        /// <summary> The id of the last <see cref="Message"/> sent in this <see cref="Channel"/>. </summary>
        /// <remarks> May not point to an existing or valid message. </remarks>
        [ModelProperty("last_message_id")]
        public Optional<Snowflake?> LastMessageId { get; set; }
        /// <summary> When the last pinned <see cref="Message"/> was pinned. </summary>
        [ModelProperty("last_pin_timestamp"), StandardFormat('O')]
        public Optional<DateTimeOffset?> LastPinTimestamp { get; set; }

        //VoiceChannel

        /// <summary> The bitrate (in bits) of the voice <see cref="Channel"/>. </summary>
        [ModelProperty("bitrate")]
        public Optional<int> Bitrate { get; set; }
        /// <summary> The <see cref="GuildMember"/> limit of the voice <see cref="Channel"/>. </summary>
        [ModelProperty("user_limit")]
        public Optional<int> UserLimit { get; set; }

        //PrivateChannel

        /// <summary> The recipients of the DM. </summary>
        [ModelProperty("recipients")]
        public Optional<User[]> Recipients { get; set; }
        /// <summary> Id of the DM creator. </summary>
        [ModelProperty("owner_id")]
        public Optional<Snowflake> OwnerId { get; set; }
        /// <summary> <see cref="Application"/> Id of the group DM creator if it is bot-created. </summary>
        [ModelProperty("application_id")]
        public Optional<Snowflake> ApplicationId { get; set; }

        //GroupChannel

        /// <summary> Icon hash. </summary>
        [ModelProperty("icon")]
        public Optional<Utf8String> Icon { get; set; }
    }
}
