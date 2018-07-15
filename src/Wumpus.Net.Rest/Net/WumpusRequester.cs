using RestEase;
using RestEase.Implementation;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Voltaic;
using Wumpus.Entities;
using Wumpus.Serialization;

namespace Wumpus.Net
{
    internal class WumpusRequester : Requester
    {
        private readonly WumpusJsonSerializer _serializer;
        private readonly IRateLimiter _rateLimiter;

        public WumpusRequester(HttpClient httpClient, WumpusJsonSerializer serializer, IRateLimiter rateLimiter)
            : base(httpClient)
        {
            _serializer = serializer;
            _rateLimiter = rateLimiter;

            ResponseDeserializer = new WumpusResponseDeserializer(_serializer);
            RequestBodySerializer = new WumpusBodySerializer(_serializer);
            RequestQueryParamSerializer = new WumpusQueryParamSerializer(_serializer);
        }

        protected override async Task<HttpResponseMessage> SendRequestAsync(IRequestInfo request, bool readBody)
        {
            var bucketId = GenerateBucketId(request);
            while (true)
            {
                await _rateLimiter.EnterLockAsync(bucketId, request.CancellationToken).ConfigureAwait(false);

                bool allowAnyStatus = request.AllowAnyStatusCode;
                ((RequestInfo)request).AllowAnyStatusCode = true;
                var response = await base.SendRequestAsync(request, readBody).ConfigureAwait(false);

                var info = new RateLimitInfo(response.Headers);
                if (response.IsSuccessStatusCode)
                {
                    _rateLimiter.UpdateLimit(bucketId, info);
                    return response;
                }

                switch (response.StatusCode)
                {
                    case (HttpStatusCode)429:
                        _rateLimiter.UpdateLimit(bucketId, info);
                        continue;
                    case HttpStatusCode.BadGateway: //502
                        await Task.Delay(250, request.CancellationToken).ConfigureAwait(false);
                        continue;
                    default:
                        if (allowAnyStatus)
                            return response;
                        // TODO: Does this allocate?
                        var bytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

                        RestError error = null;
                        try { error = _serializer.Read<RestError>(bytes.AsSpan()); } catch { }
                        if (error != null)
                            throw new DiscordRestException(response.StatusCode, error.Code, error.Message);

                        Utf8String msg = null;
                        try { msg = new Utf8String(bytes); } catch { }
                        if (msg != (Utf8String)null)
                            throw new DiscordRestException(response.StatusCode, null, msg);

                        throw new DiscordRestException(response.StatusCode);
                }
            }
        }

        private string GenerateBucketId(IRequestInfo request)
        {
            if (request.Path == null || (!request.PathParams.Any() && !request.PathProperties.Any()))
                return request.Path;

            var sb = new StringBuilder(request.Path);
            foreach (var pathParam in request.PathParams.Concat(request.PathProperties))
            {
                var serialized = pathParam.SerializeToString(FormatProvider);
                if (serialized.Key != "channelId" && serialized.Key != "guildId")
                    continue;

                // Space needs to be treated separately
                string value = pathParam.UrlEncode ? WebUtility.UrlEncode(serialized.Value ?? string.Empty).Replace("+", "%20") : serialized.Value;
                sb.Replace("{" + (serialized.Key ?? string.Empty) + "}", value);
            }
            return sb.ToString();
        }
    }
}
