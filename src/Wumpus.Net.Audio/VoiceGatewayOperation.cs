namespace Wumpus.Events
{
    /// <summary>
    ///     Voice connections operate in a similar fashion to the Gateway connection.
    ///     However, they use a different set of payloads and a separate UDP-based connection for voice data transmission.
    ///     https://discordapp.com/developers/docs/topics/voice-connections#voice
    /// </summary>
    public enum VoiceGatewayOperation : byte
    {
        /// <summary> C→S - Used to begin a voice websocket connection. </summary>
        Identify = 0,
        /// <summary> C→S - Used to select the voice protocol. </summary>
        SelectProtocol = 1,
        /// <summary> C←S - Used to complete the websocket handshake. </summary>
        Ready = 2,
        /// <summary> C→S - Used to keep the websocket connection alive. </summary>
        Heartbeat = 3,
        /// <summary> C→S - Used to describe the session. </summary>
        SessionDescription = 4,
        /// <summary> C↔S - Used to indicate which users are speaking. </summary>
        Speaking = 5,
        /// <summary> C←S - Used to reply to a heartbeat. </summary>
        HeartbeatAck = 6,
        /// <summary> C→S - Used to resume a connection. </summary>
        Resume = 7,
        /// <summary> C→S - Used to begin the websocket handshake. </summary>
        Hello = 8,
        /// <summary> C←S - Used to complete the websocket handshake with an existing session. </summary>
        Resumed = 9

        //NOTE: these do not have official names!
        //They are documented here for future expansion purposes

        //ssrc update, occurs when a user connects or changes screenshare settings
        //SsrcUpdate = 12,
        //user disconnected, occurs when a user disconnects
        //UserDisconnected = 13,
        //change channel, occurs whenever the client gets moved into another channel
        //ChangeChannel = 14
    }
}