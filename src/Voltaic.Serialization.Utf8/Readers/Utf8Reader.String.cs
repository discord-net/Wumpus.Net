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
            {
                DebugLog.WriteFailure("ToUtf16Length failed");
                return false;
            }
            if (byteCount != 2)
            {
                DebugLog.WriteFailure("String too long");
                return false;
            }

            var tmpResult = new char[1];
            var resultBytes = MemoryMarshal.AsBytes(tmpResult.AsSpan());
            if (Encodings.Utf8.ToUtf16(remaining, resultBytes, out _, out _) != OperationStatus.Done)
            {
                DebugLog.WriteFailure("ToUtf16 failed");
                return false;
            }
            result = tmpResult[0];
            return true;
        }

        public static bool TryReadString(ref ReadOnlySpan<byte> remaining, out string result)
        {
            if (Encodings.Utf8.ToUtf16Length(remaining, out var byteCount) != OperationStatus.Done)
            {
                DebugLog.WriteFailure("ToUtf16Length failed");
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
                    {
                        DebugLog.WriteFailure("ToUtf16 failed");
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
