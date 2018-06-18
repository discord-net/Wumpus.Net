using System.IO;

namespace Wumpus.Requests
{
    public struct MultipartFile
    {
        public Stream Stream { get; }
        public string Filename { get; }

        public MultipartFile(Stream stream, string filename)
        {
            Stream = stream;
            Filename = filename;
        }
    }
}
