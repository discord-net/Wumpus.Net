using System;

namespace Wumpus
{
    [Flags]
    public enum SpeakingState : byte
    {
        NotSpeaking = 0b0,
        Speaking = 0b1,

        Priority = 0b100,

        PrioritySpeaking = Speaking | Priority
    }
}