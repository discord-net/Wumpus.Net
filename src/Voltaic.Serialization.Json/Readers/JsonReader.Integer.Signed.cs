using System;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonReader
    {

        public static bool TryReadInt8(ref ReadOnlySpan<byte> remaining, out sbyte result, char standardFormat)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case JsonTokenType.Number:
                    if (!Utf8Reader.TryReadInt8(ref remaining, out result, JsonSerializer.IntFormat.Symbol))
                        return false;
                    return true;
                case JsonTokenType.String:
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadInt8(ref remaining, out result, standardFormat))
                        return false;
                    if (remaining.Length == 0 || remaining[0] != '"')
                        return false;
                    remaining = remaining.Slice(1);
                    return true;
            }
            return false;
        }

        public static bool TryReadInt16(ref ReadOnlySpan<byte> remaining, out short result, char standardFormat)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case JsonTokenType.Number:
                    if (!Utf8Reader.TryReadInt16(ref remaining, out result, JsonSerializer.IntFormat.Symbol))
                        return false;
                    return true;
                case JsonTokenType.String:
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadInt16(ref remaining, out result, standardFormat))
                        return false;
                    if (remaining.Length == 0 || remaining[0] != '"')
                        return false;
                    remaining = remaining.Slice(1);
                    return true;
            }
            return false;
        }

        public static bool TryReadInt32(ref ReadOnlySpan<byte> remaining, out int result, char standardFormat)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case JsonTokenType.Number:
                    if (!Utf8Reader.TryReadInt32(ref remaining, out result, JsonSerializer.IntFormat.Symbol))
                        return false;
                    return true;
                case JsonTokenType.String:
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadInt32(ref remaining, out result, standardFormat))
                        return false;
                    if (remaining.Length == 0 || remaining[0] != '"')
                        return false;
                    remaining = remaining.Slice(1);
                    return true;
            }
            return false;
        }

        public static bool TryReadInt64(ref ReadOnlySpan<byte> remaining, out long result, char standardFormat)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case JsonTokenType.Number:
                    if (!Utf8Reader.TryReadInt64(ref remaining, out result, JsonSerializer.IntFormat.Symbol))
                        return false;
                    return true;
                case JsonTokenType.String:
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadInt64(ref remaining, out result, standardFormat))
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
