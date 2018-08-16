using Voltaic.Serialization;
using System;
using Voltaic;
using System.Collections.Generic;
using Wumpus.Requests;
using Wumpus.Entities;
using Wumpus.Events;

namespace Wumpus
{
    public class RpcPayload
    {
        /// <summary> Payload command </summary>
        [ModelProperty("cmd")]
        public RpcCommand Command { get; set; }
        /// <summary> Subscription event </summary>
        [ModelProperty("evt")]
        public RpcEvent? Event { get; set; } 
        /// <summary> Unique string used once for replies from the server	 </summary>
        [ModelProperty("nonce")]
        public Optional<Guid?> Nonce { get; set; }        

        /// <summary> xxx </summary>
        [ModelProperty("args"), ModelTypeSelector(nameof(Command), nameof(ArgsTypeSelector))]
        public object Args { get; set; }
        /// <summary> xxx </summary>
        [ModelProperty("data")]
        [ModelTypeSelector(nameof(Event), nameof(EventTypeSelector))]
        [ModelTypeSelector(nameof(Command), nameof(ResponseTypeSelector))]
        public object Data { get; set; }
        
        private static Dictionary<RpcCommand, Type> ArgsTypeSelector => new Dictionary<RpcCommand, Type>()
        {
            [RpcCommand.Authorize] = typeof(AuthorizeParams),
            [RpcCommand.Authenticate] = typeof(AuthenticateParams),
            [RpcCommand.GetGuild] = typeof(GetGuildParams),
            [RpcCommand.GetChannel] = typeof(GetChannelParams),
            [RpcCommand.GetChannels] = typeof(GetChannelsParams),
            [RpcCommand.Subscribe] = typeof(SubscriptionParams),
            [RpcCommand.Unsubscribe] = typeof(SubscriptionParams),
            [RpcCommand.SetUserVoiceSettings] = typeof(UserVoiceSettings),
            [RpcCommand.SelectVoiceChannel] = typeof(SelectChannelParams),
            [RpcCommand.SelectTextChannel] = typeof(SelectChannelParams),
            [RpcCommand.SetVoiceSettings] = typeof(VoiceSettings),
            [RpcCommand.CaptureShortcut] = typeof(CaptureShortcutParams),
            [RpcCommand.SetCertifiedDevices] = typeof(SetCertifiedDevicesParams)
        };
        private static Dictionary<RpcCommand, Type> ResponseTypeSelector => new Dictionary<RpcCommand, Type>()
        {
            [RpcCommand.Authorize] = typeof(AuthorizeResponse),
            [RpcCommand.Authenticate] = typeof(AuthenticateResponse),
            [RpcCommand.GetGuild] = typeof(RpcGuild),
            [RpcCommand.GetGuilds] = typeof(GetGuildsResponse),
            [RpcCommand.GetChannel] = typeof(RpcChannel),
            [RpcCommand.GetChannels] = typeof(GetChannelsResponse),
            [RpcCommand.Subscribe] = typeof(SubscriptionResponse),
            [RpcCommand.Unsubscribe] = typeof(SubscriptionResponse),
            [RpcCommand.SetUserVoiceSettings] = typeof(UserVoiceSettings),
            [RpcCommand.SelectVoiceChannel] = typeof(RpcChannel),
            [RpcCommand.SelectTextChannel] = typeof(RpcChannel),
            [RpcCommand.SetVoiceSettings] = typeof(VoiceSettings),
            [RpcCommand.CaptureShortcut] = typeof(CaptureShortcutChangeEvent),
        };
        private static Dictionary<RpcEvent?, Type> EventTypeSelector => new Dictionary<RpcEvent?, Type>()
        {
            [RpcEvent.Ready] = typeof(ReadyEvent),
            [RpcEvent.Error] = typeof(ErrorEvent),
            [RpcEvent.GuildStatus] = typeof(GuildStatusEvent),
            [RpcEvent.GuildCreate] = typeof(GuildCreateEvent),
            [RpcEvent.ChannelCreate] = typeof(ChannelCreateEvent),
            [RpcEvent.VoiceChannelSelect] = typeof(VoiceChannelSelectEvent),
            [RpcEvent.VoiceStateCreate] = typeof(VoiceStateEvent),
            [RpcEvent.VoiceStateUpdate] = typeof(VoiceStateEvent),
            [RpcEvent.VoiceStateDelete] = typeof(VoiceStateEvent),
            [RpcEvent.VoiceSettingsUpdate] = typeof(VoiceSettings),
            [RpcEvent.VoiceConnectionStatus] = typeof(VoiceConnectionStatusEvent),
            [RpcEvent.SpeakingStart] = typeof(SpeakingEvent),
            [RpcEvent.SpeakingStop] = typeof(SpeakingEvent),
            [RpcEvent.MessageCreate] = typeof(MessageEvent),
            [RpcEvent.MessageUpdate] = typeof(MessageEvent),
            [RpcEvent.MessageDelete] = typeof(MessageEvent),
            [RpcEvent.NotificationCreate] = typeof(NotificationCreateEvent),
            [RpcEvent.CaptureShortcutChange] = typeof(CaptureShortcutChangeEvent)
        };
    }
}
