using System.IO;
using Voltaic;

namespace Wumpus
{
    public struct Image
    {
        public Stream Stream { get; }
        public ImageFormat StreamFormat { get; }
        public Utf8String Hash { get; }

        public Image(Stream stream, ImageFormat format)
        {
            Stream = stream;
            StreamFormat = format;
            Hash = null;
        }
        public Image(Utf8String hash)
        {
            Stream = null;
            StreamFormat = ImageFormat.Jpeg;
            Hash = hash;
        }
    }
}
