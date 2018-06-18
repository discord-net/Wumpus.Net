using System;

namespace Voltaic.Serialization
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ModelTypeSelectorAttribute : Attribute
    {
        public string SelectorProperty { get; }
        public string MapProperty { get; }

        public ModelTypeSelectorAttribute(string selectorKey, string mapKey)
        {
            SelectorProperty = selectorKey;
            MapProperty = mapKey;
        }
    }
}
