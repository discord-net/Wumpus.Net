using Voltaic.Serialization;

namespace Wumpus.Events
{
    /// <summary> xxx </summary>
    public class VoiceServerUpdateEvent
    {
        /// <summary> xxx </summary>
		[ModelProperty("guild_id")]
		public ulong GuildId { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("endpoint")]
		public string Endpoint { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("token")]
		public string Token { get; set; }
    }
}
