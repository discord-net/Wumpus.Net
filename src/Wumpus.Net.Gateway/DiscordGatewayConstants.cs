using System.Reflection;

namespace Wumpus
{
    // TODO: Do we use these?
    public static class DiscordGatewayConstants
    {
        public const int APIVersion = 6;
        public static string Version { get; } =
            typeof(DiscordGatewayConstants).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ??
            typeof(DiscordGatewayConstants).GetTypeInfo().Assembly.GetName().Version.ToString(3) ??
            "Unknown";
    }
}
