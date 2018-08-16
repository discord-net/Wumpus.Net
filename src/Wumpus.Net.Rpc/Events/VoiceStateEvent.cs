using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class VoiceStateEvent
    {
        /// <summary> xxx </summary>
        [ModelProperty("voice_state")]
        public VoiceState VoiceState { get; set; }
    }
}