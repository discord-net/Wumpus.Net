using System;
using System.Buffers;
using Voltaic.Serialization.Utf8;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfWriter
    {
        private readonly static ReadOnlyMemory<byte> _trueValue = new ReadOnlyMemory<byte>(
            new byte[] { (byte)EtfTokenType.SmallAtom, 4, (byte)'t', (byte)'r', (byte)'u', (byte)'e' });
        private readonly static ReadOnlyMemory<byte> _falseValue = new ReadOnlyMemory<byte>(
            new byte[] { (byte)EtfTokenType.SmallAtom, 5, (byte)'f', (byte)'a', (byte)'l', (byte)'s', (byte)'e' });

        public static bool TryWrite(ref ResizableMemory<byte> writer, bool value, StandardFormat standardFormat)
        {
            if (standardFormat.IsDefault)
            {
                if (value)
                {
                    _trueValue.Span.CopyTo(writer.GetSpan(6));
                    writer.Advance(6);
                }
                else
                {
                    _falseValue.Span.CopyTo(writer.GetSpan(7));
                    writer.Advance(7);
                }
            }
            else
            {
                int start = writer.Length;
                writer.Push((byte)EtfTokenType.Binary);
                writer.Advance(4);
                if (!Utf8Writer.TryWrite(ref writer, value, standardFormat))
                    return false;
                int length = writer.Length - start - 5;
                writer.Array[start + 1] = (byte)(length >> 24);
                writer.Array[start + 2] = (byte)(length >> 16);
                writer.Array[start + 3] = (byte)(length >> 8);
                writer.Array[start + 4] = (byte)length;
            }
            return true;
        }
    }
}
