using System;
using System.Net;
using Voltaic;

namespace Wumpus
{
    public class WumpusRpcException : Exception
    {
        public int Code { get; }
        public Utf8String Reason { get; }

        public WumpusRpcException(int code, Utf8String reason)
            : base($"The server responded with error {code}: {reason.ToString()}")
        {
            Code = code;
            Reason = reason;
        }
    }
}
