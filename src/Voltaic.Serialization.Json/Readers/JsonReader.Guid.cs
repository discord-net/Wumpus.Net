using System;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonReader
    {
        public static bool TryReadGuid(ref ReadOnlySpan<byte> remaining, out Guid result, char standardFormat)
        {
            result = default;

            switch (GetTokenType(ref remaining))
            {
                case JsonTokenType.String:
                    remaining = remaining.Slice(1);
                    if (!Utf8Reader.TryReadGuid(ref remaining, out result, standardFormat))
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
