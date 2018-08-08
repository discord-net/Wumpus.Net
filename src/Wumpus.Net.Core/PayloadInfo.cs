using System;

namespace Wumpus
{
    public struct PayloadInfo
    {
        public int UncompressedBytes { get; }
        public int CompressedBytes { get; }

        internal PayloadInfo(int uncompressedBytes, int compressedBytes)
        {
            UncompressedBytes = uncompressedBytes;
            CompressedBytes = compressedBytes;
        }
    }
}
