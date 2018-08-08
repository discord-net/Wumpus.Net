using System.Reflection;

namespace Wumpus
{
    public static class DiscordRpcConstants
    {
        public const int APIVersion = 1;
        public static string Version { get; } =
            typeof(DiscordRpcConstants).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ??
            typeof(DiscordRpcConstants).GetTypeInfo().Assembly.GetName().Version.ToString(3) ??
            "Unknown";
    }
}
