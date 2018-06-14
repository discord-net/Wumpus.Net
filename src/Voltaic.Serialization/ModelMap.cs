using System;

namespace Voltaic.Serialization
{
    public class ModelMap
    {
        private readonly BufferDictionary<PropertyMap> _propDict;

        public string Path { get; }

        internal ModelMap(string path)
        {
            Path = path;
            _propDict = new BufferDictionary<PropertyMap>();
        }

        internal void AddProperty(PropertyMap propMap)
        {
            _propDict.Add(propMap.Key, propMap);
        }

        public bool TryGetProperty(ReadOnlySpan<byte> key, out PropertyMap value)
            => _propDict.TryGetValue(key, out value);
    }
}
