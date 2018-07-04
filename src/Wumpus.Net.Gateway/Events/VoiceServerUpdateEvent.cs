using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> 
    ///     Sent when a <see cref="Entities.Guild"/>'s voice server is updated. This is sent when initially connecting to voice, and when the current voice instance fails over to a new server.
    ///     https://discordapp.com/developers/docs/topics/gateway#voice-server-update
    /// </summary>
    public class VoiceServerUpdateEvent
    {
        /// <summary> The <see cref="Entities.Guild"/> this voice server update is for. </summary>
		[ModelProperty("guild_id")]
		public Snowflake GuildId { get; set; }
        /// <summary> The voice server host. </summary>
        [ModelProperty("endpoint")]
		public Utf8String Endpoint { get; set; }
        /// <summary> Voice connection token. </summary>
        [ModelProperty("token")]
		public Utf8String Token { get; set; }
    }
}
