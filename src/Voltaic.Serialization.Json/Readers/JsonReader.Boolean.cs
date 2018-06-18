using System;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonReader
    {
        public static bool TryReadBoolean(ref ReadOnlySpan<byte> remaining, out bool result, char standardFormat)
        {
            result = default;
            
            switch (GetTokenType(ref remaining))
            {
                case JsonTokenType.True:
                    if (remaining.Length < 4)
                        return false;
                    result = true;
                    remaining = remaining.Slice(4);
                    return true;
                case JsonTokenType.False:
                    if (remaining.Length < 5)
                        return false;
                    result = false;
                    remaining = remaining.Slice(5);
                    return true;
                case JsonTokenType.String:
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadBoolean(ref remaining, out result, standardFormat))
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
