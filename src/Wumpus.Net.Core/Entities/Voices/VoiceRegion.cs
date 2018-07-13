using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/voice#voice-region-object </summary>
    public class VoiceRegion
    {
        /// <summary> Unique id for the <see cref="VoiceRegion"/>. </summary>
        [ModelProperty("id")]
        public Utf8String Id { get; set; }
        /// <summary> Name of the <see cref="VoiceRegion"/>. </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> True if this is a VIP-only server. </summary>
        [ModelProperty("vip")]
        public bool IsVip { get; set; }
        /// <summary> True for a single server that is closest to the current user's client. </summary>
        [ModelProperty("optimal")]
        public bool IsOptimal { get; set; }
        /// <summary> Whether this is a deprecated <see cref="VoiceRegion"/>. </summary>
        /// <remarks> Avoid switching ot these. </remarks>
        [ModelProperty("deprecated")]
        public bool Deprecated { get; set; }
        /// <summary> Whether this is a custom <see cref="VoiceRegion"/>. </summary>
        /// <remarks> Used for events/etc. </remarks>
        [ModelProperty("custom")]
        public bool Custom { get; set; }
    }
}
