using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Requests
{
    /// <summary> https://discordapp.com/developers/docs/resources/guild#guild-embed-object-guild-embed-structure </summary>
    public class ModifyGuildEmbedParams
    {
        /// <summary> If the <see cref="Entities.GuildEmbed"/> is enabled. </summary>
        [ModelProperty("enabled")]
        public Optional<bool> Enabled { get; set; }
        /// <summary> The <see cref="Entities.GuildEmbed"/> <see cref="Entities.Channel"/> id. </summary>
        [ModelProperty("channel")]
        public Optional<Snowflake?> ChannelId { get; set; }

        public void Validate() { }
    }
}
