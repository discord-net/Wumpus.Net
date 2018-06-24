using System;
using System.Buffers;
using System.Runtime.InteropServices;

namespace Voltaic.Serialization.Utf8
{
    public static partial class Utf8Reader
    {
        public static bool TryReadChar(ref ReadOnlySpan<byte> remaining, out char result)
        {
            result = default;

            if (Encodings.Utf8.ToUtf16Length(remaining, out var byteCount) != OperationStatus.Done || byteCount != 2)
                return false;

            var tmpResult = new char[1];
            var resultBytes = MemoryMarshal.AsBytes(tmpResult.AsSpan());
            if (Encodings.Utf8.ToUtf16(remaining, resultBytes, out _, out _) != OperationStatus.Done)
                return false;
            result = tmpResult[0];
            return true;
        }

        public static bool TryReadString(ref ReadOnlySpan<byte> remaining, out string result)
        {
            if (Encodings.Utf8.ToUtf16Length(remaining, out var byteCount) != OperationStatus.Done)
            {
                result = null;
                return false;
            }

            result = new string(' ', byteCount / 2);
            unsafe
            {
                fixed (char* pResult = result)
                {
                    var resultBytes = new Span<byte>(pResult, byteCount);
                    if (Encodings.Utf8.ToUtf16(remaining, resultBytes, out _, out _) != OperationStatus.Done)
                        return false;
                }
            }
            return true;
        }

        public static bool TryReadUtf8String(ref ReadOnlySpan<byte> remaining, out Utf8String result)
        {
            var utf8Bytes = new Memory<byte>(new byte[remaining.Length]);
            remaining.CopyTo(utf8Bytes.Span);

            remaining = ReadOnlySpan<byte>.Empty;
            result = new Utf8String(utf8Bytes.Span);
            return true;
        }
    }
}
