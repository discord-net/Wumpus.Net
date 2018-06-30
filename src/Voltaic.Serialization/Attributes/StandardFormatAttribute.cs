using System;
using System.Buffers;

namespace Voltaic.Serialization
{
    [AttributeUsage(AttributeTargets.Property)]
    public class StandardFormatAttribute : Attribute
    {
        public StandardFormat Format { get; }

        public StandardFormatAttribute(char symbol, byte precision = 255)
        {
            Format = new StandardFormat(symbol, precision);
        }
    }
}
