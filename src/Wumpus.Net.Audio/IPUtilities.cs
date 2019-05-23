using System;
using System.Net;
using Voltaic.Serialization.Utf8;

namespace Wumpus
{
    public static class IPUtilities
    {
        public static bool TryParseIPv4Address(ReadOnlySpan<byte> buffer, out IPAddress address)
            => TryParseIPv4Address(ref buffer, out address);

        public static bool TryParseIPv4Address(ref ReadOnlySpan<byte> buffer, out IPAddress address)
        {
            address = default;
            ulong value = 0;

            for (int i = 0; i < 4; i++)
            {
                if (!Utf8Reader.TryReadUInt8(ref buffer, out byte section, 'g'))
                    return false;

                value |= (ulong)section << (i * 8);

                // last value does not have a dot following it
                if (i != 3)
                {
                    if (buffer[0] != '.')
                        return false;

                    buffer = buffer.Slice(1);
                }
            }

            address = new IPAddress((long)value);
            return true;
        }
    }
}