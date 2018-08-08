using System;

namespace Wumpus
{
    public struct PayloadInfo
    {
        public ReadOnlyMemory<byte> Data { get; }
        public int UncompressedBytes => Data.Length;
        public int CompressedBytes { get;}

        internal PayloadInfo(ReadOnlyMemory<byte> uncompressedData, int compressedBytes)
        {
            Data = uncompressedData;
            CompressedBytes = compressedBytes;
        }
    }
}
