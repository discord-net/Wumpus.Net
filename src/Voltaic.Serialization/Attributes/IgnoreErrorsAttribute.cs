using System;

namespace Voltaic.Serialization
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IgnoreErrorsAttribute : Attribute
    {
    }
}
