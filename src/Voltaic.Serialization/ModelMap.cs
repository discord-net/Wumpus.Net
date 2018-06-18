using System;
using System.Collections.Generic;

namespace Voltaic.Serialization
{
    public abstract class ModelMap
    {
        internal readonly MemoryDictionary<PropertyMap> _propDict;
        internal readonly List<KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>> _propList;

        public string Path { get; }
        public IReadOnlyList<KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>> Properties => _propList;

        internal ModelMap(string path)
        {
            Path = path;
            _propDict = new MemoryDictionary<PropertyMap>();
            _propList = new List<KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>>();
        }

        public bool TryGetProperty(ReadOnlySpan<byte> key, out PropertyMap value)
            => _propDict.TryGetValue(key, out value);
    }

    public class ModelMap<T> : ModelMap
    {
        internal ModelMap(string path)
            : base(path) { }

        internal void AddProperty(PropertyMap propMap)
        {
            _propDict.Add(propMap.Key, propMap);
            _propList.Add(new KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>(propMap.Key, propMap));
        }

        public bool TryGetProperty(ReadOnlySpan<byte> key, out PropertyMap<T> value)
        {
            if (!_propDict.TryGetValue(key, out var untypedValue))
            {
                value = default;
                return false;
            }
            value = untypedValue as PropertyMap<T>;
            return true;
        }
    }
}
