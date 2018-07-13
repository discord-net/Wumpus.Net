using System;
using System.Collections.Generic;
using Voltaic.Serialization;
using Wumpus.Entities;
using Wumpus.Requests;

namespace Wumpus.Events
{
    /// <summary> https://discordapp.com/developers/docs/topics/gateway#payloads </summary>
    public class GatewayPayload
    {
        /// <summary> Opcode for the <see cref="GatewayPayload"/>. </summary>
        [ModelProperty("op")]
        public GatewayOpCode Operation { get; set; }
        /// <summary> The event name for this <see cref="GatewayPayload"/>. </summary>
        [ModelProperty("t", ExcludeNull = true)]
        public GatewayDispatchType? DispatchType { get; set; }
        /// <summary> Sequence number, used for resuming sessions and heartbeats. </summary>
        [ModelProperty("s", ExcludeNull = true)]
        public int? Sequence { get; set; }

        /// <summary> Event data. </summary>
        [ModelProperty("d"),
            ModelTypeSelector(nameof(Operation), nameof(OpCodeTypeSelector)),
            ModelTypeSelector(nameof(DispatchType), nameof(DispatchTypeSelector))]
        public object Payload { get; set; }

        private static Dictionary<GatewayOpCode, Type> OpCodeTypeSelector => new Dictionary<GatewayOpCode, Type>()
        {
            [GatewayOpCode.Hello] = typeof(HelloEvent),
            [GatewayOpCode.InvalidSession] = typeof(bool),

            [GatewayOpCode.Identify] = typeof(IdentifyParams),
            [GatewayOpCode.Resume] = typeof(ResumeParams),
            [GatewayOpCode.Heartbeat] = typeof(int?),
            [GatewayOpCode.RequestGuildMembers] = typeof(RequestMembersParams),
            [GatewayOpCode.VoiceStateUpdate] = typeof(UpdateVoiceStateParams),
            [GatewayOpCode.StatusUpdate] = typeof(UpdateStatusParams)
        };

        private static Dictionary<GatewayDispatchType?, Type> DispatchTypeSelector => new Dictionary<GatewayDispatchType?, Type>()
        {
            [GatewayDispatchType.Ready] = typeof(SocketReadyEvent),
            [GatewayDispatchType.GuildCreate] = typeof(GatewayGuild),
            [GatewayDispatchType.GuildUpdate] = typeof(Guild),
            [GatewayDispatchType.GuildDelete] = typeof(GatewayGuild),
            [GatewayDispatchType.GuildEmojisUpdate] = typeof(GuildEmojiUpdateEvent),
            [GatewayDispatchType.GuildSync] = typeof(GuildSyncEvent),
            [GatewayDispatchType.ChannelCreate] = typeof(Channel),
            [GatewayDispatchType.ChannelUpdate] = typeof(Channel),
            [GatewayDispatchType.ChannelDelete] = typeof(Channel),
            [GatewayDispatchType.GuildMemberAdd] = typeof(GuildMemberAddEvent),
            [GatewayDispatchType.GuildMemberUpdate] = typeof(GuildMemberUpdateEvent),
            [GatewayDispatchType.GuildMemberRemove] = typeof(GuildMemberRemoveEvent),
            [GatewayDispatchType.GuildMembersChunk] = typeof(GuildMembersChunkEvent),
            [GatewayDispatchType.ChannelRecipientAdd] = typeof(RecipientEvent),
            [GatewayDispatchType.ChannelRecipientRemove] = typeof(RecipientEvent),
            [GatewayDispatchType.GuildRoleCreate] = typeof(GuildRoleCreateEvent),
            [GatewayDispatchType.GuildRoleUpdate] = typeof(GuildRoleUpdateEvent),
            [GatewayDispatchType.GuildRoleDelete] = typeof(GuildRoleDeleteEvent),
            [GatewayDispatchType.GuildBanAdd] = typeof(GuildBanEvent),
            [GatewayDispatchType.GuildBanRemove] = typeof(GuildBanEvent),
            [GatewayDispatchType.MessageCreate] = typeof(Message),
            [GatewayDispatchType.MessageUpdate] = typeof(Message),
            [GatewayDispatchType.MessageDelete] = typeof(Message),
            [GatewayDispatchType.MessageDeleteBulk] = typeof(MessageDeleteBulkEvent),
            [GatewayDispatchType.MessageReactionAdd] = typeof(GatewayReaction),
            [GatewayDispatchType.MessageReactionRemove] = typeof(MessageReactionRemoveEvent),
            [GatewayDispatchType.MessageReactionRemoveAll] = typeof(MessageReactionRemoveAllEvent),
            [GatewayDispatchType.PresenceUpdate] = typeof(Presence),
            [GatewayDispatchType.UserUpdate] = typeof(User),
            [GatewayDispatchType.TypingStart] = typeof(TypingStartEvent),
            [GatewayDispatchType.VoiceStateUpdate] = typeof(VoiceState),
            [GatewayDispatchType.VoiceServerUpdate] = typeof(VoiceServerUpdateEvent),
            [GatewayDispatchType.WebhooksUpdate] = typeof(WebhookUpdateEvent)
        };
    }
}
