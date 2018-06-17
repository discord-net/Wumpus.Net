using System;

namespace Voltaic.Serialization
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ModelPropertyAttribute : Attribute
    {
        public string Key { get; }
        public bool ExcludeDefault { get; set; }
        public bool ExcludeNull { get; set; }

        public ModelPropertyAttribute()
            : this(null) { }
        public ModelPropertyAttribute(string key)
        {
            Key = key;
        }
    }
}
