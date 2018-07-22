using System;
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

namespace Wumpus
{
    public class WumpusBotClient
    {
        public static string Version { get; } =
            typeof(WumpusBotClient).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ??
            typeof(WumpusBotClient).GetTypeInfo().Assembly.GetName().Version.ToString(3) ??
            "Unknown";

        public WumpusRestClient Rest { get; }
        public WumpusGatewayClient Gateway { get; }

        private readonly LogManager _logManager;
        private readonly ILogger _logger;
        private bool _wroteInitialLog;
        private SemaphoreSlim _runLock;

        public AuthenticationHeaderValue Authorization
        {
            get => Rest.Authorization;
            set
            {
                Rest.Authorization = value;
                Gateway.Authorization = value;
            }
        }

        public WumpusBotClient(WumpusJsonSerializer jsonSerializer = null, WumpusEtfSerializer etfSerializer = null, IRateLimiter restRateLimiter = null, LogManager logManager = null)
        {
            Rest = new WumpusRestClient(jsonSerializer, restRateLimiter);
            Gateway = new WumpusGatewayClient(etfSerializer);
            _runLock = new SemaphoreSlim(1, 1);

            if (logManager != null)
            {
                _logManager = logManager;
                _logger = _logManager?.CreateLogger("Wumpus.Net") ?? new NullLogger();
                if (logManager.MinSeverity >= LogSeverity.Info)
                {
                    Gateway.Connected += () => _logger.Info("Connected to gateway");
                    Gateway.Disconnected += ex => _logger.Info("Disconnected to gateway", ex);
                }
                if (logManager.MinSeverity >= LogSeverity.Verbose)
                {
                    Rest.JsonSerializer.UnknownProperty += path => _logger.Verbose($"Unknown JSON property \"{path}\"");
                    Rest.JsonSerializer.FailedProperty += path => _logger.Verbose($"Failed to deserialize JSON \"{path}\"");
                    Gateway.EtfSerializer.UnknownProperty += path => _logger.Verbose($"Unknown ETF property \"{path}\"");
                    Gateway.EtfSerializer.FailedProperty += path => _logger.Verbose($"Failed to deserialize ETF \"{path}\"");
                }
                if (logManager.MinSeverity >= LogSeverity.Debug)
                {
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
                }
            }
            else
                _logger = new NullLogger();
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
