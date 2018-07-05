using System;

namespace Voltaic.Serialization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
    public class IgnoreErrorsAttribute : Attribute
    {
    }
}
