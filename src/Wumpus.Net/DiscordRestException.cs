using System;
using System.Net;

namespace Wumpus.Net
{
    public class DiscordRestException : Exception
    {
        public HttpStatusCode HttpCode { get; }
        public int? DiscordCode { get; }
        public string Reason { get; }

        public DiscordRestException(HttpStatusCode httpCode, int? discordCode = null, string reason = null)
            : base(CreateMessage(httpCode, discordCode, reason))
        {
            HttpCode = httpCode;
            DiscordCode = discordCode;
            Reason = reason;
        }

        private static string CreateMessage(HttpStatusCode httpCode, int? discordCode = null, string reason = null)
            => $"The server responded with error {discordCode ?? (int)httpCode}: {reason ?? httpCode.ToString()}";
    }
}
