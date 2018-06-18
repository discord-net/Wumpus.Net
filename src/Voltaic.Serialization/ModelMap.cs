using System;
using System.Collections.Generic;

namespace Voltaic.Serialization
{
    public abstract class ModelMap
    {
        internal readonly MemoryDictionary<PropertyMap> _propDict;
        internal readonly List<KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>> _propList;
        internal readonly MemoryDictionary<int> _selectorDict;

        public string Path { get; }
        public IReadOnlyList<KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>> Properties => _propList;

        internal ModelMap(string path)
        {
            Path = path;
            _propDict = new MemoryDictionary<PropertyMap>();
            _propList = new List<KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>>();
            _selectorDict = new MemoryDictionary<int>();
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

        internal void MapTypeSelectors()
        {
            for (int i = 0; i < _propList.Count; i++)
            {
                for (int j = 0; j < _propList[i].Value.Dependencies.Count; j++)
                {
                    var dependencyKey = _propList[i].Value.Dependencies[j];
                    if (_selectorDict.TryGetValue(dependencyKey, out _))
                        continue;
                    if (!_propDict.TryGetValue(dependencyKey, out var propMap))
                        throw new InvalidOperationException("Model referenced an unknown dependency");
                    int index = _selectorDict.Count;
                    if (index > 7)
                        throw new InvalidOperationException("Model has more than 8 dependencies");
                    propMap.SelectorIndex = index;
                    _selectorDict.Add(dependencyKey, index);
                }
            }
        }
    }
}
