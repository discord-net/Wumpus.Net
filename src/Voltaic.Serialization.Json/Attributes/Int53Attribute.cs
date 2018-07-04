using System;

namespace Voltaic.Serialization.Json
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Int53Attribute : Attribute
    {
        internal const ulong MaxValue = (1 << 53) - 1;
    }
}
