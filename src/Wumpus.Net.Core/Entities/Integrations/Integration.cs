using System;
using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#integration-object </summary>
    public class Integration
    {
        /// <summary> <see cref="Integration"/> id. </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> <see cref="Integration"/> name. </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> <see cref="Integration"/> type. </summary>
        /// <remarks> Twitch, YouTube, etc. </remarks>
        [ModelProperty("type")]
        public Utf8String Type { get; set; }
        /// <summary> Is this <see cref="Integration"/> enabled? </summary>
        [ModelProperty("enabled")]
        public bool Enabled { get; set; }
        /// <summary> Is this <see cref="Integration"/> syncing? </summary>
        [ModelProperty("syncing")]
        public bool Syncing { get; set; }
        /// <summary> Id that this integration  </summary>
        [ModelProperty("role_id")]
        public Snowflake RoleId { get; set; }
        /// <summary> The behavior of expiring subscribers. </summary>
        [ModelProperty("expire_behavior")]
        public int ExpireBehavior { get; set; }
        /// <summary> The grace period before expiring subscribers. </summary>
        [ModelProperty("expire_grace_period")]
        public int ExpireGracePeriod { get; set; }
        /// <summary> <see cref="Entities.User"/> for this <see cref="Integration"/>. </summary>
        [ModelProperty("user")]
        public User User { get; set; }
        /// <summary> <see cref="IntegrationAccount"/> information. </summary>
        [ModelProperty("account")]
        public IntegrationAccount Account { get; set; }
        /// <summary> When this <see cref="Integration"/> was last synced.  </summary>
        [ModelProperty("synced_at"), StandardFormat('O')]
        public DateTimeOffset SyncedAt { get; set; }
    }
}
