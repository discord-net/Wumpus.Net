
using System.Reflection;

namespace Wumpus
{
    public static class DiscordConstants
    {
        public const int APIVersion = 6;
        public static string Version { get; } =
            typeof(DiscordConstants).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ??
            typeof(DiscordConstants).GetTypeInfo().Assembly.GetName().Version.ToString(3) ??
            "Unknown";

        public static string UserAgent { get; } = $"DiscordBot (https://github.com/RogueException/Wumpus.Net, v{Version})";
        public static readonly string APIUrl = $"https://discordapp.com/api/v{APIVersion}/";
        public const string CDNUrl = "https://cdn.discordapp.com/";
        public const string InviteUrl = "https://discord.gg/";
        
        public const int MaxMessageSize = 2000;
        public const int MaxMessagesPerBatch = 100;
        public const int MaxUsersPerBatch = 1000;
        public const int MaxGuildsPerBatch = 100;
    }
}
