using Voltaic.Serialization;
using System;
using Voltaic;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class Integration
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("type")]
        public Utf8String Type { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("enabled")]
        public bool Enabled { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("syncing")]
        public bool Syncing { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("role_id")]
        public Snowflake RoleId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("expire_behavior")]
        public int ExpireBehavior { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("expire_grace_period")]
        public int ExpireGracePeriod { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("user")]
        public User User { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("account")]
        public IntegrationAccount Account { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("synced_at")]
        public DateTimeOffset SyncedAt { get; set; }
    }
}
