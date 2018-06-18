using System;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonReader
    {
        // TODO: Utf8Reader currently causes allocations
        public static bool TryReadSingle(ref ReadOnlySpan<byte> remaining, out float result, char standardFormat)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case JsonTokenType.Number:
                    if (!Utf8Reader.TryReadSingle(ref remaining, out result, JsonSerializer.FloatFormat.Symbol))
                        return false;
                    return true;
                case JsonTokenType.String:
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadSingle(ref remaining, out result, standardFormat))
                        return false;
                    if (remaining.Length == 0 || remaining[0] != '"')
                        return false;
                    remaining = remaining.Slice(1);
                    return true;
            }
            return false;
        }

        public static bool TryReadDouble(ref ReadOnlySpan<byte> remaining, out double result, char standardFormat)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case JsonTokenType.Number:
                    if (!Utf8Reader.TryReadDouble(ref remaining, out result, JsonSerializer.FloatFormat.Symbol))
                        return false;
                    return true;
                case JsonTokenType.String:
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadDouble(ref remaining, out result, standardFormat))
                        return false;
                    if (remaining.Length == 0 || remaining[0] != '"')
                        return false;
                    remaining = remaining.Slice(1);
                    return true;
            }
            return false;
        }

        public static bool TryReadDecimal(ref ReadOnlySpan<byte> remaining, out decimal result, char standardFormat)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case JsonTokenType.Number:
                    if (!Utf8Reader.TryReadDecimal(ref remaining, out result, JsonSerializer.FloatFormat.Symbol))
                        return false;
                    return true;
                case JsonTokenType.String:
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadDecimal(ref remaining, out result, standardFormat))
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
