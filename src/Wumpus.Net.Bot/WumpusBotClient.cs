using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Voltaic.Logging;
using Wumpus.Events;
using Wumpus.Net;
using Wumpus.Requests;
using Wumpus.Responses;
using Wumpus.Serialization;

namespace Wumpus.Bot
{
    public partial class WumpusBotClient
    {
        public static string Version { get; } =
            typeof(WumpusBotClient).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ??
            typeof(WumpusBotClient).GetTypeInfo().Assembly.GetName().Version.ToString(3) ??
            "Unknown";

        public WumpusRestClient Rest { get; }
        public WumpusGatewayClient Gateway { get; }
        public State State { get; }

        private readonly LogManager _logManager;
        private readonly ILogger _logger;
        private readonly SemaphoreSlim _runLock;
        private bool _wroteInitialLog;

        public AuthenticationHeaderValue Authorization
        {
            get => Rest.Authorization;
            set
            {
                Rest.Authorization = value;
                Gateway.Authorization = value;
            }
        }

        public WumpusBotClient(
            WumpusJsonSerializer jsonSerializer = null, WumpusEtfSerializer etfSerializer = null,
            IRateLimiter restRateLimiter = null, State state = null,
            LogManager logManager = null, bool logLibraryInfo = true)
        {
            _runLock = new SemaphoreSlim(1, 1);
            State = state ?? new State(new StateOptions());

            Rest = new WumpusRestClient(jsonSerializer, restRateLimiter);
            Gateway = new WumpusGatewayClient(etfSerializer);

            if (logManager != null)
            {
                _logManager = logManager;
                _logger = _logManager?.CreateLogger("Wumpus.Net") ?? new NullLogger();
                _wroteInitialLog = !logLibraryInfo;

                if (logManager.MinSeverity >= LogSeverity.Warning)
                {
                    Gateway.SessionLost += () => _logger.Warning("Lost previous session");
                }
                if (logManager.MinSeverity >= LogSeverity.Info)
                {
                    Gateway.Connected += () => _logger.Info("Connected to gateway");
                    Gateway.Disconnected += ex => _logger.Info("Disconnected from gateway", ex);
                    Gateway.Resumed += () => _logger.Info("Resumed previous session");
                    Gateway.SessionCreated += () => _logger.Info("Created new session");
                }
                if (logManager.MinSeverity >= LogSeverity.Verbose)
                {
                    State.Guilds.Available += g => _logger.Verbose($"Connected to guild {g.Name}");
                    State.Guilds.Unavailable += g => _logger.Verbose($"Disconnected from guild {g.Name}");
                    State.Guilds.Created += g => _logger.Verbose($"Joined guild {g.Name}");
                    State.Guilds.Deleted += g => _logger.Verbose($"Left guild {g.Name}");
                }
                if (logManager.MinSeverity >= LogSeverity.Debug)
                {
                    Rest.JsonSerializer.UnknownProperty += path => _logger.Debug($"Unknown JSON property \"{path}\"");
                    Rest.JsonSerializer.FailedProperty += path => _logger.Debug($"Failed to deserialize JSON \"{path}\"");
                    Gateway.EtfSerializer.UnknownProperty += path => _logger.Debug($"Unknown ETF property \"{path}\"");
                    Gateway.EtfSerializer.FailedProperty += path => _logger.Debug($"Failed to deserialize ETF \"{path}\"");

                    Gateway.ReceivedPayload += (payload, bytes) =>
                    {
                        if (payload.Operation == GatewayOperation.Dispatch)
                            _logger.Debug($"<~ {payload.DispatchType.Value} ({bytes.Length} bytes)");
                        else
                            _logger.Debug($"<~ {payload.Operation} ({bytes.Length} bytes)");
                    };
                    Gateway.SentPayload += (payload, bytes) =>
                    {
                        if (payload.Operation == GatewayOperation.Dispatch)
                            _logger.Debug($"~> {payload.DispatchType.Value} ({bytes.Length} bytes)");
                        else
                            _logger.Debug($"~> {payload.Operation} ({bytes.Length} bytes)");
                    };

                    State.Guilds.Updated += g => _logger.Verbose($"Updated guild {g.Name}");
                }
            }
            else
            {
                _logger = new NullLogger();
                _wroteInitialLog = true;
            }

            State.Attach(Gateway);
        }

        public void Run(string url = null, int? shardId = null, int? totalShards = null, UpdateStatusParams initialPresence = null)
            => RunAsync(url, shardId, totalShards, initialPresence).GetAwaiter().GetResult();
        public async Task RunAsync(string url = null, int? shardId = null, int? totalShards = null, UpdateStatusParams initialPresence = null)
        {
            await _runLock.WaitAsync().ConfigureAwait(false);
            try
            {
                if (!_wroteInitialLog)
                {
                    _logger.Info($"Wumpus.Net.Bot v{Version}");
                    _logger.Info($"Wumpus.Net.Rest v{WumpusRestClient.Version} (API v{WumpusRestClient.ApiVersion})");
                    _logger.Info($"Wumpus.Net.Gateway v{WumpusGatewayClient.Version} (API v{WumpusGatewayClient.ApiVersion})");
                    _wroteInitialLog = true;
                }
                while (true)
                {
                    GetGatewayResponse gatewayInfo = null;
                    if (url == null)
                    {
                        try
                        {
                            gatewayInfo = await Rest.GetGatewayAsync();
                        }
                        catch (HttpRequestException ex)
                        {
                            _logger.Warning("Failed to get gateway URL", ex);
                            await Task.Delay(5000);
                        }
                    }
                    try
                    {
                        await Gateway.RunAsync(url ?? gatewayInfo.Url.ToString(), shardId, totalShards, initialPresence);
                    }
                    catch (WebSocketException ex) when (ex.InnerException is HttpRequestException)
                    {
                        _logger.Warning("Failed to connect to gateway URL", ex);
                        await Task.Delay(5000);
                    }
                }
            }
            finally
            {
                _runLock.Release();
            }
        }

        public void Stop()
            => Gateway.Stop();
        public async Task StopAsync()
        {
            await Gateway.StopAsync();
        }
    }
}
