using System;

namespace Voltaic.Serialization
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ModelTypeSelectorAttribute : Attribute
    {
        public string KeyProperty { get; }
        public string MapProperty { get; }

        public ModelTypeSelectorAttribute(string keyProp, string mapProp)
        {
            KeyProperty = keyProp;
            MapProperty = mapProp;
        }
    }
}
