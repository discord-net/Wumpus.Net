using System;

namespace Wumpus.Entities
{
    /// <summary> https://discordapp.com/developers/docs/topics/permissions#permissions </summary>
    [Flags]
    public enum GuildPermissions : ulong
    {
        None                = 0,

        // General
        /// <summary> Allow creation of instant <see cref="Invite"/>s. </summary>
        CreateInstantInvite = 0x00000001,
        /// <summary> Allows kicking <see cref="GuildMember"/>s. </summary>
        KickMembers         = 0x00000002,
        /// <summary> Allows banning <see cref="GuildMember"/>s. </summary>
        BanMembers          = 0x00000004,
        /// <summary> Allows all permissions and bypasses <see cref="Channel"/> permission <see cref="Overwrite"/>s. </summary>
        Administrator       = 0x00000008,
        /// <summary> Allows management and editing of <see cref="Channel"/>s. </summary>
        ManageChannels      = 0x00000010,
        /// <summary> Allows management and editing of the <see cref="Guild"/>. </summary>
        ManageGuild         = 0x00000020,
        /// <summary> Allows for viewing of <see cref="AuditLog"/>s. </summary>
        ViewAuditLog        = 0x00000080,
        /// <summary> Allows <see cref="GuildMember"/>s to view a <see cref="Channel"/>, which includes reading <see cref="Message"/>s in text <see cref="Channel"/>s. </summary>
        ViewChannel         = 0x00000400,
        /// <summary> Allows for modification of own nickname. </summary>
        ChangeNickname      = 0x04000000,
        /// <summary> Allows for modification of other <see cref="GuildMember"/>'s nicknames. </summary>
        ManageNicknames     = 0x08000000,
        /// <summary> Allows management and editing of <see cref="Role"/>s. </summary>
        ManageRoles         = 0x10000000,
        /// <summary> Allows management and editing of <see cref="Webhook"/>s. </summary>
        ManageWebhooks      = 0x20000000,
        /// <summary> Allows management and editing of <see cref="Emoji"/>s. </summary>
        ManageEmojis        = 0x40000000,

        // Text
        /// <summary> Allow for the addition of <see cref="Reaction"/>s to <see cref="Message"/>s. </summary>
        AddReactions        = 0x00000040,
        /// <summary> Allows for sending <see cref="Message"/>s in a <see cref="Channel"/>. </summary>
        SendMessages        = 0x00000800,
        /// <summary> Allows for sending of /tts <see cref="Message"/>s. </summary>
        SendTTSMessages     = 0x00001000,
        /// <summary> Allows for deletion of other <see cref="User"/>s' <see cref="Message"/>s. </summary>
        ManageMessages      = 0x00002000,
        /// <summary> Links sent by <see cref="User"/>s with this permission will be auto-embedded. </summary>
        EmbedLinks          = 0x00004000,
        /// <summary> Allows for uploading images and files. </summary>
        AttachFiles         = 0x00008000,
        /// <summary> Allows for reading of <see cref="Message"/> history. </summary>
        ReadMessageHistory  = 0x00010000,
        /// <summary> Allows for using the @everyone tag to notify all <see cref="User"/>s in a <see cref="Channel"/>, and the @here tag to notify all online <see cref="User"/>s in a <see cref="Channel"/>. </summary>
        MentionEveryone     = 0x00020000,
        /// <summary> Allows the usage of custom <see cref="Emoji"/>s from other <see cref="Guild"/>s. </summary>
        UseExternalEmojis   = 0x00040000,

        // Voice
        /// <summary> Allows for joining of a voice <see cref="Channel"/>. </summary>
        Connect             = 0x00100000,
        /// <summary> Allows for speaking in a voice <see cref="Channel"/>. </summary>
        Speak               = 0x00200000,
        /// <summary> Allows for muting <see cref="GuildMember"/>s in a voice <see cref="Channel"/>. </summary>
        MuteMembers         = 0x00400000,
        /// <summary> Allows for deafening of <see cref="GuildMember"/>s in a voice <see cref="Channel"/>. </summary>
        DeafenMembers       = 0x00800000,
        /// <summary> Allows for moving of <see cref="GuildMember"/>s between voice <see cref="Channel"/>s. </summary>
        MoveMembers         = 0x01000000,
        /// <summary> Allows for using voice-activity-detection in a voice <see cref="Channel"/>. </summary>
        UseVAD              = 0x02000000
    }
}
