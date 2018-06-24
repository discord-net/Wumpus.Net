using Voltaic.Serialization;
using System;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class Integration
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public ulong Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public string Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("type")]
        public string Type { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("enabled")]
        public bool Enabled { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("syncing")]
        public bool Syncing { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("role_id")]
        public ulong RoleId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("expire_behavior")]
        public ulong ExpireBehavior { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("expire_grace_period")]
        public ulong ExpireGracePeriod { get; set; }
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
