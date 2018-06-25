using System.IO;
using Voltaic;

namespace Wumpus.Requests
{
    /// <summary> xxx </summary>
    public struct MultipartFile
    {
        /// <summary> xxx </summary>
        public Stream Stream { get; }
        /// <summary> xxx </summary>
        public Utf8String Filename { get; }

        public MultipartFile(Stream stream, Utf8String filename)
        {
            Stream = stream;
            Filename = filename;
        }
    }
}
