using System;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonReader
    {
        public static bool TryReadUInt8(ref ReadOnlySpan<byte> remaining, out byte result)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case TokenType.Number:
                    if (!Utf8Reader.TryReadUInt8(ref remaining, out result))
                        return false;
                    return true;
                case TokenType.String:
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

            switch (GetTokenType(ref remaining))
            {
                case TokenType.Number:
                    if (!Utf8Reader.TryReadUInt16(ref remaining, out result))
                        return false;
                    return true;
                case TokenType.String:
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

            switch (GetTokenType(ref remaining))
            {
                case TokenType.Number:
                    if (!Utf8Reader.TryReadUInt32(ref remaining, out result))
                        return false;
                    return true;
                case TokenType.String:
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

            switch (GetTokenType(ref remaining))
            {
                case TokenType.Number:
                    if (!Utf8Reader.TryReadUInt64(ref remaining, out result))
                        return false;
                    return true;
                case TokenType.String:
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
