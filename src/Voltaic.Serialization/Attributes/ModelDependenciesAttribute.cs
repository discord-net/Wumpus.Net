using System;

namespace Voltaic.Serialization
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ModelDependenciesAttribute : Attribute
    {
        public string[] Dependencies { get; }

        public ModelDependenciesAttribute(params string[] dependencies)
        {
            Dependencies = dependencies;
        }
    }
}
