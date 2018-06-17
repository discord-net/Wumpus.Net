using System;

namespace Voltaic.Serialization.Json
{
    [AttributeUsage(AttributeTargets.Property)]
    public class KeyAttribute : Attribute
    {
        public string Name { get; }

        public KeyAttribute(string name)
        {
            Name = name;
        }
    }
}
