using System;
using System.Collections.Generic;
using Voltaic.Serialization;
using Wumpus.Entities;
using Wumpus.Requests;

namespace Wumpus.Events
{
    public class VoiceGatewayPayload
    {
        [ModelProperty("op")]
        public VoiceGatewayOperation Operation { get; set; }

        [ModelProperty("d")]
        [ModelTypeSelector(nameof(Operation), nameof(OpCodeTypeSelector))]
        public object Data { get; set; }

        private static Dictionary<VoiceGatewayOperation, Type> OpCodeTypeSelector => new Dictionary<VoiceGatewayOperation, Type>()
        {
            [VoiceGatewayOperation.Hello] = typeof(VoiceHelloEvent),
            [VoiceGatewayOperation.Ready] = typeof(VoiceReadyEvent),
            [VoiceGatewayOperation.HeartbeatAck] = typeof(int),

            [VoiceGatewayOperation.Identify] = typeof(VoiceIdentifyParams),
            [VoiceGatewayOperation.SelectProtocol] = typeof(VoiceSelectProtocolParams),
            [VoiceGatewayOperation.SessionDescription] = typeof(VoiceSessionDescriptionEvent),
            [VoiceGatewayOperation.Resume] = typeof(VoiceResumeParams),
            [VoiceGatewayOperation.Heartbeat] = typeof(int),

            [VoiceGatewayOperation.Speaking] = typeof(VoiceSpeakingParams),
        };
    }
}