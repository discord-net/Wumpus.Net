using System.IO;
using Voltaic;

namespace Wumpus.Requests
{
    public struct MultipartFile
    {
        public Stream Stream { get; }
        public Utf8String Filename { get; }

        public MultipartFile(Stream stream, Utf8String filename)
        {
            Stream = stream;
            Filename = filename;
        }
    }
}
