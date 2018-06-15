using System;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonReader
    {
        public static TokenType GetTokenType(ref ReadOnlySpan<byte> remaining)
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
                        return TokenType.StartObject;
                    case (byte)'}':
                        remaining = remaining.Slice(i);
                        return TokenType.EndObject;
                    case (byte)'[':
                        remaining = remaining.Slice(i);
                        return TokenType.StartArray;
                    case (byte)']':
                        remaining = remaining.Slice(i);
                        return TokenType.EndArray;
                    case (byte)',':
                        remaining = remaining.Slice(i);
                        return TokenType.ListSeparator;
                    case (byte)':':
                        remaining = remaining.Slice(i);
                        return TokenType.KeyValueSeparator;
                    case (byte)'n':
                    case (byte)'N':
                        remaining = remaining.Slice(i);
                        return TokenType.Null;
                    case (byte)'t':
                    case (byte)'T':
                        remaining = remaining.Slice(i);
                        return TokenType.True;
                    case (byte)'f':
                    case (byte)'F':
                        remaining = remaining.Slice(i);
                        return TokenType.False;
                    case (byte)'"':
                        remaining = remaining.Slice(i);
                        return TokenType.String;
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
                        return TokenType.Number;
                    default:
                        if (c < 32)
                            throw new SerializationException($"Unexpected control char ({remaining[i]})");
                        else
                            throw new SerializationException($"Unexpected char: {(char)remaining[i]} ({remaining[i]})");
                }
            }
            return TokenType.None;
        }

    }
}
