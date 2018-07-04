using Voltaic;
using Voltaic.Serialization;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/topics/oauth2#get-current-application-information-response-structure </summary>
    public class Application
    {
        /// <summary> The description of the <see cref="Application"/>. </summary>
        [ModelProperty("description")]
        public Optional<Utf8String> Description { get; set; }
        /// <summary> An array of RPC origin url strings, if RPC is enabled. </summary>
        [ModelProperty("rpc_origins")]
        public Utf8String[] RPCOrigins { get; set; }
        /// <summary> The name of the <see cref="Application"/>. </summary>
        [ModelProperty("name")]
        public Utf8String Name { get; set; }
        /// <summary> The Id of the <see cref="Application"/>. </summary>
        [ModelProperty("id")]
        public Snowflake Id { get; set; }
        /// <summary> The icon hash of the <see cref="Application"/>. </summary>
        [ModelProperty("icon")]
        public Optional<Utf8String> Icon { get; set; }
        /// <summary> When false only <see cref="Application"/> owner can join the <see cref="Application"/>'s bot to guilds. </summary>
        [ModelProperty("bot_public")]
        public bool IsPublic { get; set; }
        /// <summary> When true, the <see cref="Application"/>'s bot will only join upon completion of the full OAuth2 code grant flow. </summary>
        [ModelProperty("bot_require_code_grant")]
        public bool RequiresCodeGrant { get; set; }
        /// <summary> Partial <see cref="User"/> object containing info on the owner of the <see cref="Application"/>. </summary>
        [ModelProperty("owner")]
        public Optional<User> Owner { get; set; }
    }
}
