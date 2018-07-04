using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/resources/channel#message-object-message-application-structure </summary>
    public class MessageApplication
    {
        /// <summary> Id of the <see cref="MessageApplication"/>. </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> Id of the embed's image asset. </summary>
        [ModelProperty("cover_image")]
        public Utf8String CoverImage { get; set; }
        /// <summary> <see cref="MessageApplication"/>'s description. </summary>
        [ModelProperty("description")]
        public Utf8String Description { get; set; }
        /// <summary> Id of the <see cref="MessageApplication"/>'s icon. </summary>
        [ModelProperty("icon")]
        public Snowflake Icon { get; set; }
        /// <summary> Name of the <see cref="MessageApplication"/>. </summary>
        [ModelProperty("name")]
        public Snowflake Name { get; set; }
    }
}