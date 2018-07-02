using System;

namespace Voltaic.Serialization.Etf
{
    public static partial class EtfWriter
    {
        private readonly static ReadOnlyMemory<byte> _nilValue = new ReadOnlyMemory<byte>(
            new byte[] { (byte)EtfTokenType.SmallAtom, 3, (byte)'n', (byte)'i', (byte)'l' });

        public static bool TryWriteNull(ref ResizableMemory<byte> writer)
        {
            _nilValue.Span.CopyTo(writer.GetSpan(5));
            writer.Advance(5);
            return true;
        }
    }
}
