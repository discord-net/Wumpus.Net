using System;

namespace Wumpus
{
    public struct Snowflake
    {
        private const ulong DiscordEpoch = 1420070400000UL;

        public ulong RawValue { get; }

        public Snowflake(ulong value)
        {
            RawValue = value;
        }
        public Snowflake(DateTimeOffset dto)
        {
            RawValue = ((ulong)dto.ToUniversalTime().ToUnixTimeMilliseconds() - DiscordEpoch) << 22;
        }
        public Snowflake(DateTime dt)
            : this(new DateTimeOffset(dt)) { }

        public DateTimeOffset ToDateTimeOffset() => DateTimeOffset.FromUnixTimeMilliseconds((long)((RawValue >> 22) + DiscordEpoch));
        public DateTimeOffset ToDateTime() => ToDateTimeOffset().UtcDateTime;

        public static implicit operator ulong(Snowflake snowflake) => snowflake.RawValue;
        public static implicit operator Snowflake(ulong value) => new Snowflake(value);

        public override string ToString()
            => RawValue.ToString();
    }
}
