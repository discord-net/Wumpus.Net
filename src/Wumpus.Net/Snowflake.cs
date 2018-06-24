using System;

namespace Wumpus
{
    public struct Snowflake
    {
        private const ulong DiscordEpoch = 1420070400000UL;

        public ulong Value { get; }

        public Snowflake(ulong value)
        {
            Value = value;
        }
        public Snowflake(DateTimeOffset dto)
        {
            Value = ((ulong)dto.ToUnixTimeMilliseconds() - DiscordEpoch) << 22;
        }
        public Snowflake(DateTime dt)
            : this(new DateTimeOffset(dt)) { }

        public DateTimeOffset ToDateTimeOffset()
            => DateTimeOffset.FromUnixTimeMilliseconds((long)((Value >> 22) + DiscordEpoch));

        public static implicit operator ulong(Snowflake snowflake) => snowflake.Value;
        public static implicit operator Snowflake(ulong value) => new Snowflake(value);
    }
}
