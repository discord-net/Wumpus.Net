using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class VoiceServerUpdateEvent
    {
        /// <summary> xxx </summary>
		[ModelProperty("guild_id")]
		public Snowflake GuildId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("endpoint")]
		public Utf8String Endpoint { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("token")]
		public Utf8String Token { get; set; }
    }
}
