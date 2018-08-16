using System;
using System.Net.WebSockets;

namespace Wumpus
{
    public class WebSocketClosedException : Exception
    {
        public int Code { get; }
        public WebSocketCloseStatus? CloseStatus { get; }
        public string Reason { get; }
        
        public WebSocketClosedException(WebSocketCloseStatus status, string reason = null)
            : this(CreateMessage(status, reason), null, status, reason) { }
        public WebSocketClosedException(string message, WebSocketCloseStatus status, string reason = null)
            : this(message, null, status, reason) { }
        public WebSocketClosedException(string message, Exception innerException, WebSocketCloseStatus status, string reason = null)
            : base(message, innerException)
        {
            Code = (int)status;
            if (Code >= 1000 && Code < 2000)
                CloseStatus = status;
            else
                CloseStatus = null;
            Reason = reason;
        }

        private static string CreateMessage(WebSocketCloseStatus status, string reason)
        {
            if (!string.IsNullOrEmpty(reason))
                return $"WebSocket was closed: {status} ({reason})";
            else
                return $"WebSocket was closed: {status}";
        }
    }
}
