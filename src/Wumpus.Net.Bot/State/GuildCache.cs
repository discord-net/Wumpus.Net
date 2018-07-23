using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Voltaic.Logging;
using Wumpus.Entities;
using Wumpus.Events;

namespace Wumpus.Bot
{
    public class GuildCache : IReadOnlyCollection<CachedGuild>
    {
        public event Action<CachedGuild> Available;
        public event Action<CachedGuild> Unavailable;
        public event Action<CachedGuild> Created;
        public event Action<CachedGuild> Updated;
        public event Action<CachedGuild> Deleted;

        private readonly ConcurrentDictionary<ulong, CachedGuild> _guilds;
        private readonly ILogger _logger;

        public bool IsEnabled { get; }
        public int Count => IsEnabled ? _guilds.Count : 0;

        internal GuildCache(bool isEnabled, LogManager logManager)
        {
            IsEnabled = isEnabled;
            _logger = logManager?.CreateLogger("Guilds") ?? new NullLogger("Guilds");
            if (IsEnabled)
                _guilds = new ConcurrentDictionary<ulong, CachedGuild>();
        }

        internal void HandleReady(ReadyEvent data)
        {
            foreach (var guildData in data.Guilds)
            {
                var guild = new CachedGuild { Id = guildData.Id };
                guild.Unavailable = true;
                _guilds[guildData.Id.RawValue] = guild;
            }
        }
        internal void HandleSessionLost()
        {
            foreach (var guild in this)
            {
                if (guild.Unavailable != true)
                {
                    guild.Unavailable = true;
                    Unavailable?.Invoke(guild);
                }
            }
            _guilds.Clear();
        }
        internal void HandleGuildCreate(GatewayGuild data)
        {
            if (!_guilds.TryGetValue(data.Id.RawValue, out var guild))
            {
                guild = new CachedGuild { Id = data.Id };
                guild.Update(data);
                _guilds[data.Id.RawValue] = guild;
                Created?.Invoke(guild);
                if (guild.Unavailable != true)
                    Available?.Invoke(guild);
            }
            else
            {
                if (data.Unavailable == false && guild.Unavailable != false)
                {
                    guild.Unavailable = false;
                    Available?.Invoke(guild);
                }
                else if (data.Unavailable == true && guild.Unavailable != true)
                {
                    guild.Unavailable = true;
                    Unavailable?.Invoke(guild);
                }
                guild.Update(data);
            }
        }
        internal void HandleGuildUpdate(Guild data)
        {
            if (!_guilds.TryGetValue(data.Id.RawValue, out var guild))
            {
                _logger.Warning($"Failed to process GuildUpdate, unknown guild {data.Id}");
                return;
            }

            guild.Update(data);
            Updated?.Invoke(guild);
        }
        internal void HandleGuildDelete(UnavailableGuild data)
        {
            if (!_guilds.TryGetValue(data.Id.RawValue, out var guild))
            {
                _logger.Warning($"Failed to process GuildDelete, unknown guild {data.Id}");
                return;
            }

            if (data.Unavailable == true)
            {
                Unavailable?.Invoke(guild);
            }
            else
            {
                if (guild.Unavailable != true)
                {
                    guild.Unavailable = true;
                    Unavailable?.Invoke(guild);
                }
                Deleted?.Invoke(guild);
            }
        }
        internal void HandleGuildEmojisUpdate(GuildEmojiUpdateEvent data)
        {
            if (!_guilds.TryGetValue(data.GuildId.RawValue, out var guild))
            {
                _logger.Warning($"Failed to process GuildEmojisUpdate, unknown guild {data.GuildId}");
                return;
            }

            // TODO: How do we handle this?
            //guild.Update(data);
            //Updated?.Invoke(guild);
        }
        internal void HandleGuildIntegrationsUpdate(GuildIntegrationsUpdateEvent data)
        {
            if (!_guilds.TryGetValue(data.GuildId.RawValue, out var guild))
            {
                _logger.Warning($"Failed to process GuildIntegrationsUpdate, unknown guild {data.GuildId}");
                return;
            }

            // TODO: How do we handle this?
            //guild.Update(data);
            //Updated?.Invoke(guild);
        }
        internal void HandleGuildWebhooksUpdate(WebhooksUpdateEvent data)
        {
            if (!_guilds.TryGetValue(data.GuildId.RawValue, out var guild))
            {
                _logger.Warning($"Failed to process GuildWebhooksUpdate, unknown guild {data.GuildId}");
                return;
            }

            // TODO: How do we handle this?
            //guild.Update(data);
            //Updated?.Invoke(guild);
        }

        public IEnumerator<CachedGuild> GetEnumerator()
            => _guilds.Select(x => x.Value).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => _guilds.Select(x => x.Value).GetEnumerator();
    }
}
