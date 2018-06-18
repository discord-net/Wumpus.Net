using System;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonReader
    {
        public static JsonTokenType GetTokenType(ref ReadOnlySpan<byte> remaining)
        {
            for (int i = 0; i < remaining.Length; i++)
            {
                byte c = remaining[i];
                switch (c)
                {
                    case (byte)' ': // Whitespace
                    case (byte)'\n':
                    case (byte)'\r':
                    case (byte)'\t':
                        continue;
                    case (byte)'{':
                        remaining = remaining.Slice(i); // Strip whitespace
                        return JsonTokenType.StartObject;
                    case (byte)'}':
                        remaining = remaining.Slice(i);
                        return JsonTokenType.EndObject;
                    case (byte)'[':
                        remaining = remaining.Slice(i);
                        return JsonTokenType.StartArray;
                    case (byte)']':
                        remaining = remaining.Slice(i);
                        return JsonTokenType.EndArray;
                    case (byte)',':
                        remaining = remaining.Slice(i);
                        return JsonTokenType.ListSeparator;
                    case (byte)':':
                        remaining = remaining.Slice(i);
                        return JsonTokenType.KeyValueSeparator;
                    case (byte)'n':
                    case (byte)'N':
                        remaining = remaining.Slice(i);
                        return JsonTokenType.Null;
                    case (byte)'t':
                    case (byte)'T':
                        remaining = remaining.Slice(i);
                        return JsonTokenType.True;
                    case (byte)'f':
                    case (byte)'F':
                        remaining = remaining.Slice(i);
                        return JsonTokenType.False;
                    case (byte)'"':
                        remaining = remaining.Slice(i);
                        return JsonTokenType.String;
                    case (byte)'-':
                    case (byte)'0':
                    case (byte)'1':
                    case (byte)'2':
                    case (byte)'3':
                    case (byte)'4':
                    case (byte)'5':
                    case (byte)'6':
                    case (byte)'7':
                    case (byte)'8':
                    case (byte)'9':
                        remaining = remaining.Slice(i);
                        return JsonTokenType.Number;
                    default:
                        if (c < 32)
                            throw new SerializationException($"Unexpected control char ({remaining[i]})");
                        else
                            throw new SerializationException($"Unexpected char: {(char)remaining[i]} ({remaining[i]})");
                }
            }
            return JsonTokenType.None;
        }

    }
}
