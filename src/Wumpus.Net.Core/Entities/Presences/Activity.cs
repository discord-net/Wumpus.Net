using System;
using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/topics/gateway#activity-object </summary>
    [IgnoreErrors]
    public class Activity
    {
        /// <summary> the activity's name </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> activity type </summary>
        [ModelProperty("type")]
        public ActivityType Type { get; set; }
        /// <summary> stream url, is validated when type is 1 </summary>
        [ModelProperty("url")]
        public Optional<Utf8String> Url { get; set; }
        /// <summary> unix timestamps for start and/or end of the game </summary>
        [ModelProperty("timestamps")]
        public Optional<ActivityTimestamps> Timestamps { get; set; }
        /// <summary> what the player is currently doing </summary>
        [ModelProperty("details")]
        public Optional<Utf8String> Details { get; set; }
        /// <summary> the user's current party status </summary>
        [ModelProperty("state")]
        public Optional<Utf8String> State { get; set; }
        /// <summary> information for the current party of the player </summary>
        [ModelProperty("party")]
        public Optional<ActivityParty> Party { get; set; }
        /// <summary> images for the presence and their hover texts </summary>
        [ModelProperty("assets")]
        public Optional<ActivityAssets> Assets { get; set; }
    }

    /// <summary> https://discordapp.com/developers/docs/topics/gateway#activity-object-activity-timestamps </summary>
    [IgnoreErrors]
    public class ActivityTimestamps
    {
        /// <summary> unix time (in milliseconds) of when the activity started </summary>
        [ModelProperty("start"), Epoch(EpochType.UnixMillis)]
        public Optional<DateTimeOffset> Start { get; set; }
        /// <summary> unix time (in milliseconds) of when the activity ends </summary>
        [ModelProperty("end"), Epoch(EpochType.UnixMillis)]
        public Optional<DateTimeOffset> End { get; set; }
    }

    /// <summary> https://discordapp.com/developers/docs/topics/gateway#activity-object-activity-party </summary>
    [IgnoreErrors]
    public class ActivityParty
    {
        /// <summary> the id of the party </summary>
        [ModelProperty("id")]
        public Optional<Utf8String> Id { get; set; }
        /// <summary> used to show the party's current and maximum size </summary>
        [ModelProperty("size")]
        public Optional<long[]> Size { get; set; }
    }

    /// <summary> https://discordapp.com/developers/docs/topics/gateway#activity-object-activity-assets </summary>
    [IgnoreErrors]
    public class ActivityAssets
    {
        /// <summary> the id for a large asset of the activity, usually a snowflake </summary>
        [ModelProperty("large_image")]
        public Optional<Utf8String> LargeImage { get; set; }
        /// <summary> text displayed when hovering over the large image of the activity </summary>
        [ModelProperty("large_text")]
        public Optional<Utf8String> LargeText { get; set; }
        /// <summary> the id for a small asset of the activity, usually a snowflake </summary>
        [ModelProperty("small_image")]
        public Optional<Utf8String> SmallImage { get; set; }
        /// <summary> text displayed when hovering over the small image of the activity </summary>
        [ModelProperty("small_text")]
        public Optional<Utf8String> SmallText { get; set; }
    }
}
