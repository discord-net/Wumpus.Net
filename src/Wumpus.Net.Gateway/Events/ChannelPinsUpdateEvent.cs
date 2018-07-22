using System;
using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> 
    ///     Sent when a <see cref="Message"/> is pinned or unpinned in a text <see cref="Channel"/>. This is not sent when a pinned <see cref="Message"/> is deleted.
    ///     https://discordapp.com/developers/docs/topics/gateway#channel-pins-update
    /// </summary>
    public class ChannelPinsUpdateEvent
    {
        /// <summary> The id of the <see cref="Channel"/>. </summary>
        [ModelProperty("channel_id")]
        public Snowflake ChannelId { get; set; }
        /// <summary> The <see cref="Entities.User"/> for the <see cref="Ban"/>. </summary>
        [ModelProperty("last_pin_timestamp"), StandardFormat('O')]
        public Optional<DateTimeOffset> LastPinTimestamp { get; set; }
    }
}
