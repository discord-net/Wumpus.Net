using System;
using Voltaic.Logging;

namespace Wumpus.Bot
{
    public class StateOptions
    {
        public bool CacheGuilds { get; set; } = false;
        //public bool CacheGuildChannels { get; set; } = false;
        //public bool CacheGuildMembers { get; set; } = false;
        //public bool CacheGuildRoles { get; set; } = false;
        //public bool CachePrivateChannels { get; set; } = false;
        //public bool CacheChannelMessages { get; set; } = false;
        //public bool CacheUsers { get; set; } = false;
    }

    public class State
    {
        public event Action SessionReady;
        public event Action SessionLost;

        public GuildCache Guilds { get; }

        public State(StateOptions options, LogManager logManager = null)
        {
            Guilds = new GuildCache(options.CacheGuilds, logManager);
        }

        internal void Attach(WumpusGatewayClient client)
        {
            client.Ready += d =>
            {
                if (Guilds.IsEnabled) Guilds.HandleReady(d);
                //if (Channels.IsEnabled) Channels.HandleReady(d);
                //if (Users.IsEnabled) Users.HandleReady(d);
                SessionReady?.Invoke();
            };
            client.SessionLost += () =>
            {
                if (Guilds.IsEnabled) Guilds.HandleSessionLost();
                //if (Channels.IsEnabled) Guilds.HandleSessionLost();
                //if (Users.IsEnabled) Guilds.HandleSessionLost();
                SessionLost?.Invoke();
            };
            if (Guilds.IsEnabled)
            {
                client.GuildCreate += d => Guilds.HandleGuildCreate(d);
                client.GuildUpdate += d => Guilds.HandleGuildUpdate(d);
                client.GuildDelete += d => Guilds.HandleGuildDelete(d);
                client.GuildEmojisUpdate += d => Guilds.HandleGuildEmojisUpdate(d);
                client.GuildIntegrationsUpdate += d => Guilds.HandleGuildIntegrationsUpdate(d);
                client.WebhooksUpdate += d => Guilds.HandleGuildWebhooksUpdate(d);
            }
        }
    }
}
