using Voltaic.Serialization;

namespace Wumpus.Events
{
    public class VoiceServerUpdateEvent
	{
		[ModelProperty("guild_id")]
		public ulong GuildId { get; set; }
        [ModelProperty("endpoint")]
		public string Endpoint { get; set; }
        [ModelProperty("token")]
		public string Token { get; set; }
    }
}
