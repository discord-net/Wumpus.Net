using System;

namespace Voltaic.Serialization
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class IgnorePropertiesAttribute : Attribute
    {
        public string[] PropertyNames { get; }

        public IgnorePropertiesAttribute(params string[] propertyNames)
        {
            PropertyNames = propertyNames;
        }
    }
}
