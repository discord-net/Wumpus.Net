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
                        remaining = remaining.Slice(i);
                        return JsonTokenType.Null;
                    case (byte)'t':
                        remaining = remaining.Slice(i);
                        return JsonTokenType.True;
                    case (byte)'f':
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
                        return JsonTokenType.None;
                        //if (c < 32)
                        //    throw new SerializationException($"Unexpected control char ({remaining[i]})");
                        //else
                        //    throw new SerializationException($"Unexpected char: {(char)remaining[i]} ({remaining[i]})");
                }
            }
            return JsonTokenType.None;
        }

        // TODO: Add tests
        public static bool Skip(ref ReadOnlySpan<byte> remaining, out ReadOnlySpan<byte> skipped)
        {
            skipped = default;

            var stack = new ResizableMemory<byte>(32);
            var currentToken = JsonTokenType.None;

            int i = 0;
            for (; i < remaining.Length || currentToken != JsonTokenType.None;)
            {
                byte c = remaining[i];
                switch (c)
                {
                    case (byte)' ': // Whitespace
                    case (byte)'\n':
                    case (byte)'\r':
                    case (byte)'\t':
                        i++;
                        continue;
                    case (byte)'{':
                        i++;
                        stack.Push((byte)currentToken);
                        currentToken = JsonTokenType.StartObject;
                        break;
                    case (byte)'}':
                        if (currentToken != JsonTokenType.StartObject)
                            return false;
                        i++;
                        currentToken = (JsonTokenType)stack.Pop();
                        break;
                    case (byte)'[':
                        i++;
                        stack.Push((byte)currentToken);
                        currentToken = JsonTokenType.StartArray;
                        break;
                    case (byte)']':
                        if (currentToken != JsonTokenType.StartArray)
                            return false;
                        i++;
                        currentToken = (JsonTokenType)stack.Pop();
                        break;
                    case (byte)',':
                        if (currentToken != JsonTokenType.StartObject && currentToken != JsonTokenType.StartArray)
                            return false;
                        i++;
                        break;
                    case (byte)':':
                        if (currentToken != JsonTokenType.StartObject)
                            return false;
                        i++;
                        break;
                    case (byte)'n':
                        i += 4; // ull
                        break;
                    case (byte)'t':
                        i += 4; // rue
                        break;
                    case (byte)'f':
                        i += 5; // alse
                        break;
                    case (byte)'"':
                        i++;
                        bool incomplete = true;
                        while (i < remaining.Length)
                        {
                            switch (remaining[i])
                            {
                                case (byte)'\\':
                                    i += 2; // Skip next char
                                    continue;
                                case (byte)'"':
                                    i++;
                                    incomplete = false;
                                    break;
                                default:
                                    i++;
                                    continue;
                            }
                            break;
                        }
                        if (incomplete)
                            return false;
                        break;
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
                        i++;
                        while (i < remaining.Length)
                        {
                            switch (remaining[i])
                            {
                                case (byte)'+':
                                case (byte)'-':
                                case (byte)'.':
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
                                case (byte)'e':
                                case (byte)'E':
                                    i++;
                                    continue;
                                default: // Crossed into next token
                                    break;
                            }
                            break;
                        }
                        break;
                    default:
                        return false;
                }
            }

            // Incomplete object/array
            if (currentToken != JsonTokenType.None)
                return false;
            skipped = remaining.Slice(0, i);
            remaining = remaining.Slice(i);
            return true;
        }
    }
}
