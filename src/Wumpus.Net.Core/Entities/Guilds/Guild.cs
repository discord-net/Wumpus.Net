using System;
using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#guild-object </summary>
    public class Guild
    {
        public const int MinNameLength = 2;
        public const int MaxNameLength = 100;

        public const int MinGetMembersLimit = 1;
        public const int MaxGetMembersLimit = 1000;

        public const int MinGetGuildsLimit = 1;
        public const int MaxGetGuildsLimit = 100;
        
        /// <summary> <see cref="Guild"/> id. </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> <see cref="Guild"/> name. </summary>
        /// <remarks> 2-100 characters. </remarks>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> Icon hash. </summary>
        [ModelProperty("icon")]
        public Utf8String Icon { get; set; }
        /// <summary> Splash hash. </summary>
        [ModelProperty("splash")]
        public Utf8String Splash { get; set; }
        /// <summary> Id of owner. </summary>
        [ModelProperty("owner_id")]
        public Snowflake OwnerId { get; set; }
        /// <summary> Total permissions for the <see cref="User"/> in the <see cref="Guild"/> (does not include <see cref="Overwrite"/>). </summary>
        [ModelProperty("permissions")]
        public Optional<GuildPermissions> Permissions { get; set; }
        /// <summary> <see cref="VoiceRegion"/> Id for the <see cref="Guild"/>. </summary>
        [ModelProperty("region")]
        public Utf8String Region { get; set; }
        /// <summary> Id of AFK <see cref="Channel"/>. </summary>
        [ModelProperty("afk_channel_id")]
        public Snowflake? AFKChannelId { get; set; }
        /// <summary> AFK timeout in seconds. </summary>
        [ModelProperty("afk_timeout")]
        public int AFKTimeout { get; set; }
        /// <summary> Is this <see cref="Guild"/> embeddable? </summary>
        /// <remarks> e.g. widget. </remarks>
        [ModelProperty("embed_enabled")]
        public Optional<bool> EmbedEnabled { get; set; }
        /// <summary> Id of embedded <see cref="Channel"/>. </summary>
        [ModelProperty("embed_channel_id")]
        public Optional<Snowflake?> EmbedChannelId { get; set; }
        /// <summary> <see cref="Entities.VerificationLevel"/> required for the <see cref="Guild"/>. </summary>
        [ModelProperty("verification_level")]
        public VerificationLevel VerificationLevel { get; set; }
        /// <summary> Default <see cref="Message"/> notifications level. </summary>
        [ModelProperty("default_message_notifications")]
        public DefaultMessageNotifications DefaultMessageNotifications { get; set; }
        /// <summary> Explicit content filter level. </summary>
        [ModelProperty("explicit_content_filter")]
        public ExplicitContentFilter ExplicitContentFilter { get; set; }
        /// <summary> <see cref="Role"/> entities in the <see cref="Guild"/>. </summary>
        [ModelProperty("roles")]
        public Role[] Roles { get; set; }
        /// <summary> Custom <see cref="Guild"/> <see cref="Emoji"/> entities. </summary>
        [ModelProperty("emojis")]
        public Emoji[] Emojis { get; set; }
        /// <summary> Enabled <see cref="Guild"/> features. </summary>
        [ModelProperty("features")]
        public Utf8String[] Features { get; set; }
        /// <summary> Required MFA level for the <see cref="Guild"/>. </summary>
        [ModelProperty("mfa_level")]
        public MfaLevel MfaLevel { get; set; }
        /// <summary> <see cref="Application"/> Id of the <see cref="Guild"/> creator if it is bot-created. </summary>
        [ModelProperty("application_id")]
        public Snowflake? ApplicationId { get; set; }
        /// <summary> Whether or not the <see cref="Guild"/> widget is enabled. </summary>
        [ModelProperty("widget_enabled")]
        public Optional<bool> IsWidgetEnabled { get; set; }
        /// <summary> The <see cref="Channel"/> Id for the <see cref="Guild"/> widget. </summary>
        [ModelProperty("widget_channel_id")]
        public Optional<Snowflake> WidgetChannelId { get; set; }
        /// <summary> The id of the <see cref="Channel"/> to which system <see cref="Message"/> entities are sent. </summary>
        [ModelProperty("system_channel_id")]
        public Snowflake? SystemChannelId { get; set; }
    }
}
