using System;

namespace Voltaic.Serialization
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ModelTypeSelectorAttribute : Attribute
    {
        public string KeyProperty { get; }
        public string MapProperty { get; }

        public ModelTypeSelectorAttribute(string keyProp, string mapDictProp)
        {
            KeyProperty = keyProp;
            MapProperty = mapDictProp;
        }
    }
}
