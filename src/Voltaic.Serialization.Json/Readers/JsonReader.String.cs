using System;
using System.Buffers;
using System.Runtime.InteropServices;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonReader
    {
        public enum StringOperationStatus
        {
            Failed,
            Builder,
            Direct
        }

        public static bool TryReadChar(ref ReadOnlySpan<byte> remaining, out char result)
        {
            result = default;

            switch (TryReadUtf8Bytes(ref remaining, out var builder, out var direct))
            {
                case StringOperationStatus.Failed:
                    return false;
                case StringOperationStatus.Builder:
                    try
                    {
                        var span = builder.AsReadOnlySpan();
                        if (!Utf8Reader.TryReadChar(ref span, out result))
                            return false;
                        return true;
                    }
                    finally
                    {
                        builder.Return();
                    }
                case StringOperationStatus.Direct:
                    if (!Utf8Reader.TryReadChar(ref direct, out result))
                        return false;
                    return true;
            }
            return false;
        }

        public static bool TryReadString(ref ReadOnlySpan<byte> remaining, out string result)
        {
            result = default;

            switch (TryReadUtf8Bytes(ref remaining, out var builder, out var direct))
            {
                case StringOperationStatus.Failed:
                    return false;
                case StringOperationStatus.Builder:
                    try
                    {
                        var span = builder.AsReadOnlySpan();
                        if (!Utf8Reader.TryReadString(ref span, out result))
                            return false;
                        return true;
                    }
                    finally
                    {
                        builder.Return();
                    }
                case StringOperationStatus.Direct:
                    if (!Utf8Reader.TryReadString(ref direct, out result))
                        return false;
                    return true;
            }
            return false;
        }

        public static bool TryReadUtf8String(ref ReadOnlySpan<byte> remaining, out Utf8String result)
        {
            result = default;

            switch (TryReadUtf8Bytes(ref remaining, out var builder, out var direct))
            {
                case StringOperationStatus.Failed:
                    return false;
                case StringOperationStatus.Builder:
                    try
                    {
                        result = new Utf8String(builder.AsReadOnlySpan());
                        return true;
                    }
                    finally
                    {
                        builder.Return();
                    }
                case StringOperationStatus.Direct:
                    result = new Utf8String(direct);
                    return true;
            }
            return false;
        }

        public static StringOperationStatus TryReadUtf8Bytes(ref ReadOnlySpan<byte> remaining, out ResizableMemory<byte> builder, out ReadOnlySpan<byte> directResult)
        {
            builder = default;
            directResult = default;

            switch (GetTokenType(ref remaining))
            {
                case JsonTokenType.String:
                    remaining = remaining.Slice(1);

                    int start = 0;
                    int i = 0;
                    bool success = false;
                    for (; i < remaining.Length; i++)
                    {
                        if (remaining[i] == '"')
                        {
                            success = true;
                            break;
                        }
                        else if (remaining[i] == '\\') // Escape sequence
                        {                            
                            if (remaining.Length - i - 1 < 1)
                                return StringOperationStatus.Failed;

                            if (start == 0)
                                builder = new ResizableMemory<byte>(remaining.Length * 2);
                            if (i != start)
                            {
                                // Append previous part to builder
                                int bytes = i - start;
                                var buffer = builder.GetSpan(bytes);
                                remaining.Slice(start, bytes).CopyTo(buffer);
                                builder.Advance(bytes);
                            }

                            switch (remaining[++i])
                            {
                                case (byte)'\"':
                                    builder.Push((byte)'\"');
                                    break;
                                case (byte)'\\':
                                    builder.Push((byte)'\\');
                                    break;
                                case (byte)'/':
                                    builder.Push((byte)'/');
                                    break;
                                case (byte)'b':
                                    builder.Push((byte)'\b');
                                    break;
                                case (byte)'f':
                                    builder.Push((byte)'\f');
                                    break;
                                case (byte)'n':
                                    builder.Push((byte)'\n');
                                    break;
                                case (byte)'r':
                                    builder.Push((byte)'\r');
                                    break;
                                case (byte)'t':
                                    builder.Push((byte)'\t');
                                    break;
                                case (byte)'u':
                                    var sequenceWriter = new ResizableMemory<ushort>(128);
                                    try
                                    {
                                        for (int j = 0; i < remaining.Length; j++)
                                        {
                                            if (j != 0)
                                            {
                                                if (remaining.Length - i < 2 || remaining[i + 1] != '\\' || remaining[i + 2] != 'u')
                                                    break;
                                                i += 2;
                                            }

                                            if (remaining.Length - i - 1 < 4)
                                                return StringOperationStatus.Failed;
                                            ReadOnlySpan<byte> bytes = stackalloc byte[]
                                            {
                                                ToHex(remaining[++i]),
                                                ToHex(remaining[++i]),
                                                ToHex(remaining[++i]),
                                                ToHex(remaining[++i])
                                            };
                                            if (bytes[0] == 255 || bytes[1] == 255 || bytes[2] == 255 || bytes[3] == 255)
                                                return StringOperationStatus.Failed;

                                            ushort value = (ushort)(
                                                (bytes[0] << 12) |
                                                (bytes[1] << 8) |
                                                (bytes[2] << 4) |
                                                bytes[3]);
                                            sequenceWriter.Push(value);
                                        }

                                        var buffer = builder.GetSpan(sequenceWriter.Length * 2);
                                        var sequenceBytes = MemoryMarshal.AsBytes(sequenceWriter.AsReadOnlySpan());
                                        if (Encodings.Utf16.ToUtf8(sequenceBytes, buffer, out _, out int bytesWritten) != OperationStatus.Done)
                                            return StringOperationStatus.Failed;
                                        builder.Advance(bytesWritten);
                                    }
                                    finally
                                    {
                                        sequenceWriter.Return();
                                    }
                                    break;
                                default:
                                    return StringOperationStatus.Failed;
                            }

                            start = i + 1;
                        }
                        else if (remaining[i] < 32) // Control char
                            return StringOperationStatus.Failed;
                    }

                    if (success)
                    {
                        if (start != 0)
                        {
                            // Append last part to builder
                            if (i != start)
                            {
                                int bytes = i - start;
                                var buffer = builder.GetSpan(bytes);
                                remaining.Slice(start, bytes).CopyTo(buffer);
                                builder.Advance(bytes);
                            }
                            directResult = ReadOnlySpan<byte>.Empty;
                            remaining = remaining.Slice(i + 1);
                            return StringOperationStatus.Builder;
                        }
                        else
                        {
                            builder = default;
                            directResult = remaining.Slice(0, i);
                            remaining = remaining.Slice(i + 1);
                            return StringOperationStatus.Direct;
                        }
                    }
                    break;
            }
            return StringOperationStatus.Failed;
        }

        private static byte ToHex(byte value)
        {
            switch (value)
            {
                case (byte)'0': return 0;
                case (byte)'1': return 1;
                case (byte)'2': return 2;
                case (byte)'3': return 3;
                case (byte)'4': return 4;
                case (byte)'5': return 5;
                case (byte)'6': return 6;
                case (byte)'7': return 7;
                case (byte)'8': return 8;
                case (byte)'9': return 9;
                case (byte)'A': return 10;
                case (byte)'a': return 10;
                case (byte)'B': return 11;
                case (byte)'b': return 11;
                case (byte)'C': return 12;
                case (byte)'c': return 12;
                case (byte)'D': return 13;
                case (byte)'d': return 13;
                case (byte)'E': return 14;
                case (byte)'e': return 14;
                case (byte)'F': return 15;
                case (byte)'f': return 15;
                default: return 255;
            }
        }
    }
}
