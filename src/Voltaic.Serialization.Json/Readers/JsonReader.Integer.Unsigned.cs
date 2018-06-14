using System;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonReader
    {
        public static bool TryReadUInt8(ref ReadOnlySpan<byte> remaining, out byte result)
        {
            result = default;

            if (remaining.Length == 0)
                return false;

            switch (remaining[0])
            {
                case (byte)'0': // Integer
                case (byte)'1':
                case (byte)'2':
                case (byte)'3':
                case (byte)'4':
                case (byte)'5':
                case (byte)'6':
                case (byte)'7':
                case (byte)'8':
                case (byte)'9':
                    if (!Utf8Reader.TryReadUInt8(ref remaining, out result))
                        return false;
                    return true;
                case (byte)'"': // String
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadUInt8(ref remaining, out result))
                        return false;
                    if (remaining.Length == 0 || remaining[0] != '"')
                        return false;
                    remaining = remaining.Slice(1);
                    return true;
            }
            return false;
        }

        public static bool TryReadUInt16(ref ReadOnlySpan<byte> remaining, out ushort result)
        {
            result = default;

            if (remaining.Length == 0)
                return false;

            switch (remaining[0])
            {
                case (byte)'0': // Integer
                case (byte)'1':
                case (byte)'2':
                case (byte)'3':
                case (byte)'4':
                case (byte)'5':
                case (byte)'6':
                case (byte)'7':
                case (byte)'8':
                case (byte)'9':
                    if (!Utf8Reader.TryReadUInt16(ref remaining, out result))
                        return false;
                    return true;
                case (byte)'"': // String
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadUInt16(ref remaining, out result))
                        return false;
                    if (remaining.Length == 0 || remaining[0] != '"')
                        return false;
                    remaining = remaining.Slice(1);
                    return true;
            }
            return false;
        }

        public static bool TryReadUInt32(ref ReadOnlySpan<byte> remaining, out uint result)
        {
            result = default;

            if (remaining.Length == 0)
                return false;

            switch (remaining[0])
            {
                case (byte)'0': // Integer
                case (byte)'1':
                case (byte)'2':
                case (byte)'3':
                case (byte)'4':
                case (byte)'5':
                case (byte)'6':
                case (byte)'7':
                case (byte)'8':
                case (byte)'9':
                    if (!Utf8Reader.TryReadUInt32(ref remaining, out result))
                        return false;
                    return true;
                case (byte)'"': // String
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadUInt32(ref remaining, out result))
                        return false;
                    if (remaining.Length == 0 || remaining[0] != '"')
                        return false;
                    remaining = remaining.Slice(1);
                    return true;
            }
            return false;
        }

        public static bool TryReadUInt64(ref ReadOnlySpan<byte> remaining, out ulong result)
        {
            result = default;

            if (remaining.Length == 0)
                return false;

            switch (remaining[0])
            {
                case (byte)'0': // Integer
                case (byte)'1':
                case (byte)'2':
                case (byte)'3':
                case (byte)'4':
                case (byte)'5':
                case (byte)'6':
                case (byte)'7':
                case (byte)'8':
                case (byte)'9':
                    if (!Utf8Reader.TryReadUInt64(ref remaining, out result))
                        return false;
                    return true;
                case (byte)'"': // String
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadUInt64(ref remaining, out result))
                        return false;
                    if (remaining.Length == 0 || remaining[0] != '"')
                        return false;
                    remaining = remaining.Slice(1);
                    return true;
            }
            return false;
        }
    }
}
