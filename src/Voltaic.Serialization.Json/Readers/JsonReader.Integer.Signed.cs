using System;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonReader
    {
        public static bool TryReadInt8(ref ReadOnlySpan<byte> remaining, out sbyte result)
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
                    if (!Utf8Reader.TryReadInt8(ref remaining, out result))
                        return false;
                    return true;
                case (byte)'"': // String
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadInt8(ref remaining, out result))
                        return false;
                    if (remaining.Length == 0 || remaining[0] != '"')
                        return false;
                    remaining = remaining.Slice(1);
                    return true;
            }
            return false;
        }

        public static bool TryReadInt16(ref ReadOnlySpan<byte> remaining, out short result)
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
                    if (!Utf8Reader.TryReadInt16(ref remaining, out result))
                        return false;
                    return true;
                case (byte)'"': // String
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadInt16(ref remaining, out result))
                        return false;
                    if (remaining.Length == 0 || remaining[0] != '"')
                        return false;
                    remaining = remaining.Slice(1);
                    return true;
            }
            return false;
        }

        public static bool TryReadInt32(ref ReadOnlySpan<byte> remaining, out int result)
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
                    if (!Utf8Reader.TryReadInt32(ref remaining, out result))
                        return false;
                    return true;
                case (byte)'"': // String
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadInt32(ref remaining, out result))
                        return false;
                    if (remaining.Length == 0 || remaining[0] != '"')
                        return false;
                    remaining = remaining.Slice(1);
                    return true;
            }
            return false;
        }

        public static bool TryReadInt64(ref ReadOnlySpan<byte> remaining, out long result)
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
                    if (!Utf8Reader.TryReadInt64(ref remaining, out result))
                        return false;
                    return true;
                case (byte)'"': // String
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadInt64(ref remaining, out result))
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
