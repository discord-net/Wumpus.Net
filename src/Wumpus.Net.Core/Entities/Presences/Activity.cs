using System;
using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/topics/gateway#activity-object </summary>
    [IgnoreErrors]
    public class Activity
    {
        /// <summary> The <see cref="Activity"/>'s name </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> <see cref="Activity"/> type. </summary>
        [ModelProperty("type")]
        public ActivityType Type { get; set; }
        /// <summary> Stream url, is validated when type is 1. </summary>
        [ModelProperty("url")]
        public Optional<Utf8String> Url { get; set; }
        /// <summary> Unix timestamps for start and/or end of the game. </summary>
        [ModelProperty("timestamps")]
        public Optional<ActivityTimestamps> Timestamps { get; set; }
        /// <summary> What the player is currently doing. </summary>
        [ModelProperty("details")]
        public Optional<Utf8String> Details { get; set; }
        /// <summary> The user's current party status. </summary>
        [ModelProperty("state")]
        public Optional<Utf8String> State { get; set; }
        /// <summary> Information for the current party of the player. </summary>
        [ModelProperty("party")]
        public Optional<ActivityParty> Party { get; set; }
        /// <summary> Images for the presence and their hover texts. </summary>
        [ModelProperty("assets")]
        public Optional<ActivityAssets> Assets { get; set; }
    }

    /// <summary> https://discordapp.com/developers/docs/topics/gateway#activity-object-activity-timestamps </summary>
    [IgnoreErrors]
    public class ActivityTimestamps
    {
        /// <summary> Unix time (in milliseconds) of when the <see cref="Activity"/> started. </summary>
        [ModelProperty("start"), Epoch(EpochType.UnixMillis)]
        public Optional<int> Start { get; set; }
        /// <summary> Unix time (in milliseconds) of when the <see cref="Activity"/> ends. </summary>
        [ModelProperty("end"), Epoch(EpochType.UnixMillis)]
        public Optional<int> End { get; set; }
    }

    /// <summary> https://discordapp.com/developers/docs/topics/gateway#activity-object-activity-party </summary>
    [IgnoreErrors]
    public class ActivityParty
    {
        /// <summary> The id of the party. </summary>
        [ModelProperty("id")]
        public Optional<Utf8String> Id { get; set; }
        /// <summary> Used to show the party's current and maximum size. </summary>
        [ModelProperty("size")]
        public Optional<long[]> Size { get; set; }
    }

    /// <summary> https://discordapp.com/developers/docs/topics/gateway#activity-object-activity-assets </summary>
    [IgnoreErrors]
    public class ActivityAssets
    {
        /// <summary> The id for a large asset of the <see cref="Activity"/>, usually a <see cref="Snowflake"/>. </summary>
        [ModelProperty("large_image")]
        public Optional<Utf8String> LargeImage { get; set; }
        /// <summary> Text displayed when hovering over the large image of the <see cref="Activity"/>. </summary>
        [ModelProperty("large_text")]
        public Optional<Utf8String> LargeText { get; set; }
        /// <summary> The id for a small asset of the <see cref="Activity"/>, usually a <see cref="Snowflake"/>. </summary>
        [ModelProperty("small_image")]
        public Optional<Utf8String> SmallImage { get; set; }
        /// <summary> Text displayed when hovering over the small image of the <see cref="Activity"/>. </summary>
        [ModelProperty("small_text")]
        public Optional<Utf8String> SmallText { get; set; }
    }
}
