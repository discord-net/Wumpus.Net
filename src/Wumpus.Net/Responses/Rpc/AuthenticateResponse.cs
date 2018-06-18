using Voltaic.Serialization;
using System;
using Wumpus.Entities;

namespace Wumpus.Events
{
    public class AuthenticateResponse
    {
        [ModelProperty("application")]
        public Application Application { get; set; }
        [ModelProperty("expires")]
        public DateTimeOffset Expires { get; set; }
        [ModelProperty("user")]
        public User User { get; set; }
        [ModelProperty("scopes")]
        public string[] Scopes { get; set; }
    }
}
