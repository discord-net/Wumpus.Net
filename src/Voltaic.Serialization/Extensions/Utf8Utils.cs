using System;
using System.Text;

namespace Voltaic.Serialization
{
    internal static class Utf8Utils
    {
        public static ReadOnlyMemory<byte> ToUtf8Memory(this string str)
            => new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(str));
    }
}
