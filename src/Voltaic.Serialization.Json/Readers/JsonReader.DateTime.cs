using System;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonReader
    {
        public static bool TryReadDateTime(ref ReadOnlySpan<byte> remaining, out DateTime result, char standardFormat)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case JsonTokenType.String:
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadDateTime(ref remaining, out result, standardFormat))
                        return false;
                    if (remaining.Length == 0 || remaining[0] != '"')
                        return false;
                    remaining = remaining.Slice(1);
                    return true;
            }
            return false;
        }

        public static bool TryReadDateTimeOffset(ref ReadOnlySpan<byte> remaining, out DateTimeOffset result, char standardFormat)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case JsonTokenType.String:
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadDateTimeOffset(ref remaining, out result, standardFormat))
                        return false;
                    if (remaining.Length == 0 || remaining[0] != '"')
                        return false;
                    remaining = remaining.Slice(1);
                    return true;
            }
            return false;
        }

        public static bool TryReadTimeSpan(ref ReadOnlySpan<byte> remaining, out TimeSpan result, char standardFormat)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case JsonTokenType.String:
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadTimeSpan(ref remaining, out result, standardFormat))
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
