using System;

namespace Wumpus.Entities
{
    [Flags]
    public enum DiscordOAuthScope
    {
        None                    = 1 << 0,
        Bot                     = 1 << 1,
        Connections             = 1 << 2,
        Email                   = 1 << 3,
        Identify                = 1 << 4,
        Guilds                  = 1 << 5,
        GuildsJoin              = 1 << 6,
        GroupDMJoin             = 1 << 7,
        MessagesRead            = 1 << 8,
        Rpc                     = 1 << 9,
        RpcApi                  = 1 << 10,
        RpcNotificationsRead    = 1 << 11,
        WebhookIncoming         = 1 << 12
    }
}
