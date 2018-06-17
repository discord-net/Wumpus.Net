using System;
using System.Buffers;

namespace Voltaic.Serialization.Json
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class StandardFormatAttribute : Attribute
    {
        public StandardFormat Format { get; }

        public StandardFormatAttribute(StandardFormat format)
        {
            Format = format;
        }
    }
}
