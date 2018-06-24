using RestEase;
using RestEase.Implementation;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Wumpus.Entities;
using Wumpus.Serialization;

namespace Wumpus.Net
{
    internal class WumpusRequester : Requester
    {
        private readonly WumpusJsonSerializer _serializer;
        private readonly ConcurrentDictionary<string, RequestBucket> _buckets;

        private DateTimeOffset _globalWaitUntil;

        public WumpusRequester(HttpClient httpClient, WumpusJsonSerializer serializer)
            : base(httpClient)
        {
            _serializer = serializer;
            _buckets = new ConcurrentDictionary<string, RequestBucket>();

            ResponseDeserializer = new WumpusResponseDeserializer(_serializer);
            RequestBodySerializer = new WumpusBodySerializer(_serializer);
            RequestQueryParamSerializer = new WumpusQueryParamSerializer(_serializer);
        }

        protected override async Task<HttpResponseMessage> SendRequestAsync(IRequestInfo request, bool readBody)
        {
            var bucket = GetOrCreateBucket(request);

            while (true)
            {
                await EnterGlobalLockAsync().ConfigureAwait(false);
                await bucket.EnterAsync(request).ConfigureAwait(false);

                bool allowAnyStatus = request.AllowAnyStatusCode;
                ((RequestInfo)request).AllowAnyStatusCode = true;
                var response = await base.SendRequestAsync(request, readBody).ConfigureAwait(false);

                var info = new RateLimitInfo(response.Headers);                
                if (response.IsSuccessStatusCode)
                {
                    bucket.UpdateRateLimit(info, false);
                    return response;
                }
                
                switch (response.StatusCode)
                {
                    case (HttpStatusCode)429:
                        {
                            if (info.IsGlobal)
                                UpdateGlobalRateLimit(info);
                            else
                                bucket.UpdateRateLimit(info, true);
                        }
                        continue;
                    case HttpStatusCode.BadGateway: //502
                        await Task.Delay(250, request.CancellationToken).ConfigureAwait(false);
                        continue;
                    default:
                        {
                            if (allowAnyStatus)
                                return response;
                            RestError error = null;
                            try
                            {
                                // TODO: Does this allocate?
                                var bytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                                error = _serializer.Read<RestError>(bytes.AsSpan());
                            }
                            catch { }
                            throw new DiscordRestException(response.StatusCode, error?.Code, error?.Message);
                        }
                }
            }
        }

        internal RequestBucket GetOrCreateBucket(IRequestInfo request)
        {
            string bucketId = GenerateBucketId(request);
            return _buckets.GetOrAdd(bucketId, x => new RequestBucket(this));
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

        internal async Task EnterGlobalLockAsync()
        {
            int millis = (int)Math.Ceiling((_globalWaitUntil - DateTimeOffset.UtcNow).TotalMilliseconds);
            if (millis > 0)
                await Task.Delay(millis).ConfigureAwait(false);
        }
        internal void UpdateGlobalRateLimit(RateLimitInfo info)
        {
            _globalWaitUntil = DateTimeOffset.UtcNow.AddMilliseconds(info.RetryAfter.Value + (info.Lag?.TotalMilliseconds ?? 0.0));
        }
    }
}
