using System;

namespace Voltaic.Serialization
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ModelTypeSelectorAttribute : Attribute
    {
        public string[] RequiredProperties { get; }

        public ModelTypeSelectorAttribute(string[] requiredProperties)
        {
            RequiredProperties = requiredProperties;
        }
    }
}
