using Voltaic.Serialization;

namespace Wumpus.Entities
{
    public class InviteGuild
    {
        [ModelProperty("id")]
        public ulong Id { get; set; }
        [ModelProperty("name")]
        public string Name { get; set; }
        [ModelProperty("splash_hash")]
        public string SplashHash { get; set; }
    }
}
