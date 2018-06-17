using System;
using System.Buffers;
using System.Runtime.InteropServices;

namespace Voltaic.Serialization.Json
{
    public static partial class JsonWriter
    {
        public static bool TryWrite(ref ResizableMemory<byte> writer, char value)
        {
            ReadOnlySpan<char> chars = stackalloc char[] { value };
            var charBytes = MemoryMarshal.AsBytes(chars);

            if (Encodings.Utf16.ToUtf8Length(charBytes, out var length) != OperationStatus.Done)
                return false;
            var data = writer.Pool.Rent(length);
            try
            {
                if (Encodings.Utf16.ToUtf8(charBytes, data, out _, out _) != OperationStatus.Done)
                    return false;

                writer.Append((byte)'\"');
                if (!TryWriteUtf8String(ref writer, data.AsSpan(0, length)))
                    return false;
                writer.Append((byte)'\"');
            }
            finally
            {
                writer.Pool.Return(data);
            }
            return true;
        }

        public static bool TryWrite(ref ResizableMemory<byte> writer, string value)
        {
            var charBytes = MemoryMarshal.AsBytes(value.AsSpan());

            if (Encodings.Utf16.ToUtf8Length(charBytes, out var length) != OperationStatus.Done)
                return false;
            var data = writer.Pool.Rent(length);
            try
            {
                if (Encodings.Utf16.ToUtf8(charBytes, data, out _, out _) != OperationStatus.Done)
                    return false;

                writer.Append((byte)'\"');
                if (!TryWriteUtf8String(ref writer, data.AsSpan(0, length)))
                    return false;
                writer.Append((byte)'\"');
            }
            finally
            {
                writer.Pool.Return(data);
            }
            return true;
        }

        private static bool TryWriteUtf8String(ref ResizableMemory<byte> writer, ReadOnlySpan<byte> value)
        {            
            int i = 0;
            int start = 0;
            for (; i < value.Length; i++)
            {
                byte b = value[i];

                // Special chars
                switch (b)
                {
                    case (byte)'"':
                    case (byte)'\\':
                    //case (byte)'/':
                        if (i != start)
                        {
                            int bytes = i - start;
                            var buffer = writer.GetSpan(bytes);
                            value.Slice(start, bytes).CopyTo(buffer);
                            writer.Advance(bytes);
                        }
                        writer.Append((byte)'\\');
                        writer.Append(b);

                        start = i + 1;
                        continue;
                }


                if (b < 32) // Control codes
                {
                    if (i != start)
                    {
                        int bytes = i - start;
                        var buffer = writer.GetSpan(bytes);
                        value.Slice(start, bytes).CopyTo(buffer);
                        writer.Advance(bytes);
                    }
                    switch (b)
                    {
                        case (byte)'\b':
                            writer.Append((byte)'\\');
                            writer.Append((byte)'b');
                            break;
                        case (byte)'\f':
                            writer.Append((byte)'\\');
                            writer.Append((byte)'f');
                            break;
                        case (byte)'\n':
                            writer.Append((byte)'\\');
                            writer.Append((byte)'n');
                            break;
                        case (byte)'\r':
                            writer.Append((byte)'\\');
                            writer.Append((byte)'r');
                            break;
                        case (byte)'\t':
                            writer.Append((byte)'\\');
                            writer.Append((byte)'t');
                            break;
                        default:
                            var escape = writer.GetSpan(6);
                            escape[0] = (byte)'\\';
                            escape[1] = (byte)'u';
                            escape[2] = (byte)'0';
                            escape[3] = (byte)'0';
                            escape[4] = ToHex((byte)(b >> 4));
                            escape[5] = ToHex((byte)(b & 0xF));
                            writer.Advance(6);
                            break;
                    }
                    start = i + 1;
                }
                else if (b >= 192) // Multi-byte chars
                {
                    if (i != start)
                    {
                        int bytes = i - start;
                        var buffer = writer.GetSpan(bytes);
                        value.Slice(start, i - start).CopyTo(buffer);
                        writer.Advance(bytes);
                    }
                    int seqStart = i;
                    int length = 1;
                    for (; length < 4 && i < value.Length - 1 && value[i + 1] >= 128; length++, i++) { }

                    Span<ushort> utf16Value = stackalloc ushort[2];
                    if (Encodings.Utf8.ToUtf16(value.Slice(seqStart, length), MemoryMarshal.AsBytes(utf16Value), out _, out int bytesWritten) != OperationStatus.Done)
                        return false;

                    for (int j = 0; j < bytesWritten / 2; j++)
                    {
                        var buffer2 = writer.GetSpan(6);
                        buffer2[0] = (byte)'\\';
                        buffer2[1] = (byte)'u';
                        buffer2[2] = ToHex((byte)((utf16Value[j] >> 12) & 0xF));
                        buffer2[3] = ToHex((byte)((utf16Value[j] >> 8) & 0xF));
                        buffer2[4] = ToHex((byte)((utf16Value[j] >> 4) & 0xF));
                        buffer2[5] = ToHex((byte)(utf16Value[j] & 0xF));
                        writer.Advance(6);
                    }

                    start = i + 1;
                }
                else if (b >= 128) // Multi-byte chars out of sequence
                    return false;
            }
            // Append last part to builder
            if (i != start)
            {
                int bytes = i - start;
                var buffer = writer.GetSpan(bytes);
                value.Slice(start, bytes).CopyTo(buffer);
                writer.Advance(bytes);
            }
            return true;
        }

        private static byte ToHex(byte b)
        {
            switch (b)
            {
                case 0: return (byte)'0';
                case 1: return (byte)'1';
                case 2: return (byte)'2';
                case 3: return (byte)'3';
                case 4: return (byte)'4';
                case 5: return (byte)'5';
                case 6: return (byte)'6';
                case 7: return (byte)'7';
                case 8: return (byte)'8';
                case 9: return (byte)'9';
                case 10: return (byte)'A';
                case 11: return (byte)'B';
                case 12: return (byte)'C';
                case 13: return (byte)'D';
                case 14: return (byte)'E';
                case 15: return (byte)'F';
                default: throw new SerializationException("Requested bad hex digit");
            }
        }
    }
}
