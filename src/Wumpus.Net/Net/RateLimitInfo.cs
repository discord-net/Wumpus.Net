using System;
using System.Linq;
using System.Net.Http.Headers;

namespace Wumpus.Net
{
    internal struct RateLimitInfo
    {
        public bool IsGlobal { get; }
        public int? Limit { get; }
        public int? Remaining { get; }
        public int? RetryAfter { get; }
        public DateTimeOffset? Reset { get; }
        public TimeSpan? Lag { get; }

        internal RateLimitInfo(HttpResponseHeaders headers)
        {
            IsGlobal = headers.TryGetValues("X-RateLimit-Global", out var values) &&
                bool.TryParse(values.First(), out var isGlobal) ? isGlobal : false;
            Limit = headers.TryGetValues("X-RateLimit-Limit", out values) &&
                int.TryParse(values.First(), out var limit) ? limit : (int?)null;
            Remaining = headers.TryGetValues("X-RateLimit-Remaining", out values) &&
                int.TryParse(values.First(), out var remaining) ? remaining : (int?)null;
            Reset = headers.TryGetValues("X-RateLimit-Reset", out values) &&
                int.TryParse(values.First(), out var reset) ? DateTimeUtils.FromUnixSeconds(reset) : (DateTimeOffset?)null;
            RetryAfter = headers.TryGetValues("Retry-After", out values) &&
                int.TryParse(values.First(), out var retryAfter) ? retryAfter : (int?)null;
            Lag = headers.TryGetValues("Date", out values) &&
                DateTimeOffset.TryParse(values.First(), out var date) ? DateTimeOffset.UtcNow - date : (TimeSpan?)null;
        }
    }
}
