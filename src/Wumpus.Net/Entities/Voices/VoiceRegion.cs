using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> xxx </summary>
    public class VoiceRegion
    {
        /// <summary> xxx </summary>
        [ModelProperty("id")]
        public string Id { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("name")]
        public string Name { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("vip")]
        public bool IsVip { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("optimal")]
        public bool IsOptimal { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("sample_hostname")]
        public string SampleHostname { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("sample_port")]
        public int SamplePort { get; set; }
    }
}
