using System.Threading;
using System.Threading.Tasks;

namespace Wumpus.Net
{
    public interface IRateLimiter
    {
        Task EnterLockAsync(string bucketId, CancellationToken cancelToken);
        void UpdateLimit(string bucketId, RateLimitInfo info);
    }
}
