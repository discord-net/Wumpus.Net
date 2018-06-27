using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class VoiceRegion
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public Utf8String Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("vip")]
        public bool IsVip { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("optimal")]
        public bool IsOptimal { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("sample_hostname")]
        public Utf8String SampleHostname { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("sample_port")]
        public int SamplePort { get; set; }
    }
}
