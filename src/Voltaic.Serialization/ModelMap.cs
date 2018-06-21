using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Voltaic.Serialization
{
    public abstract class ModelMap
    {
        public const int MaxDependencies = 8;

        public string Path { get; }

        internal readonly MemoryDictionary<PropertyMap> _propDict;
        internal readonly List<KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>> _propList;
        internal readonly MemoryDictionary<int> _selectorDict;

        public IReadOnlyList<KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>> Properties => _propList;

        internal ModelMap(string path)
        {
            _propDict = new MemoryDictionary<PropertyMap>();
            _propList = new List<KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>>();
            _selectorDict = new MemoryDictionary<int>();
        }

        public bool TryGetProperty(ReadOnlySpan<byte> key, out PropertyMap value)
            => _propDict.TryGetValue(key, out value);
    }

    public class ModelMap<T> : ModelMap
    {
        internal ModelMap(Serializer serializer, string path, PropertyInfo propInfo = null)
            : base(path)
        {
            var type = typeof(T).GetTypeInfo();
            var dependencies = new List<KeyValuePair<PropertyMap, ModelTypeSelectorAttribute>>();
            while (type != null)
            {
                foreach (var itemPropInfo in type.DeclaredProperties)
                {
                    var propAttr = itemPropInfo.GetCustomAttribute<ModelPropertyAttribute>();
                    if (propAttr != null)
                    {
                        var constructor = typeof(PropertyMap<,>).MakeGenericType(typeof(T), itemPropInfo.PropertyType).GetTypeInfo().DeclaredConstructors.Single();
                        var converter = serializer.GetConverter(itemPropInfo);
                        var propMap = constructor.Invoke(new object[] { serializer, this, propAttr, propAttr, converter }) as PropertyMap<T>;
                        _propDict.Add(propMap.Key, propMap);
                        _propList.Add(new KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>(propMap.Key, propMap));

                        var typeSelectorAttr = itemPropInfo.GetCustomAttribute<ModelTypeSelectorAttribute>();
                        if (typeSelectorAttr != null)
                            dependencies.Add(new KeyValuePair<PropertyMap, ModelTypeSelectorAttribute>(propMap, typeSelectorAttr));
                    }
                }
            }

            for (int i = 0; i < dependencies.Count; i++)
            {
                var depInfo = dependencies[i];

                var bytes = MemoryMarshal.AsBytes(depInfo.Value.KeyProperty.AsSpan());
                if (Encodings.Utf16.ToUtf8Length(bytes, out int length) != OperationStatus.Done)
                    throw new InvalidOperationException("Failed to convert dependency key to UTF8");
                var utf8Key = new Memory<byte>(new byte[length]);
                if (Encodings.Utf16.ToUtf8(bytes, utf8Key.Span, out _, out _) != OperationStatus.Done)
                    throw new InvalidOperationException("Failed to convert dependency key to UTF8");
                if (!_propDict.TryGetValue(utf8Key, out var depProp))
                    throw new InvalidOperationException($"Unable to find dependency \"{depInfo.Value.KeyProperty}\"");
                depInfo.Key.Dependency = depProp;

                if (depProp.Index == 0)
                {
                    depProp.Index = _selectorDict.Count;
                    if (depProp.Index >= MaxDependencies)
                        throw new InvalidOperationException($"Model has more than {MaxDependencies} dependencies");
                    _selectorDict.Add(utf8Key, depProp.Index.Value);
                }
            }
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
