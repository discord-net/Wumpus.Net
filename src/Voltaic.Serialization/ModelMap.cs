using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Voltaic.Serialization
{
    public abstract class ModelMap
    {
        protected int _selectorKeyCount;

        public abstract string Name { get; }
        public abstract Type ModelType { get; }

        internal readonly MemoryDictionary<PropertyMap> _propDict;
        internal readonly List<KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>> _propList;

        public IReadOnlyList<KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>> Properties => _propList;

        internal ModelMap()
        {
            _propDict = new MemoryDictionary<PropertyMap>();
            _propList = new List<KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>>();
        }

        public bool TryGetProperty(ReadOnlySpan<byte> key, out PropertyMap value)
            => _propDict.TryGetValue(key, out value);
    }

    public class ModelMap<T> : ModelMap
    {
        public override string Name => typeof(T).Name;
        public override Type ModelType => typeof(T);

        internal ModelMap(Serializer serializer)
        {
            var type = typeof(T).GetTypeInfo();
            var normalProps = new Dictionary<string, PropertyMap<T>>();

            // Normal props
            var currentType = type;
            while (currentType != null)
            {
                foreach (var itemPropInfo in currentType.DeclaredProperties)
                {
                    var propAttr = itemPropInfo.GetCustomAttribute<ModelPropertyAttribute>();
                    if (propAttr != null && itemPropInfo.GetCustomAttributes<ModelTypeSelectorAttribute>().Count() == 0)
                    {
                        var propMapType = typeof(PropertyMap<,>).MakeGenericType(typeof(T), itemPropInfo.PropertyType).GetTypeInfo();
                        var constructor = propMapType.DeclaredConstructors.Single();
                        var converter = serializer.GetConverter(itemPropInfo, true);
                        var propMap = constructor.Invoke(new object[] { serializer, this, itemPropInfo, propAttr, converter }) as PropertyMap<T>;

                        _propDict.Add(propMap.Key, propMap);
                        _propList.Add(new KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>(propMap.Key, propMap));
                        normalProps.Add(itemPropInfo.Name, propMap);
                    }
                }
                currentType = currentType.BaseType?.GetTypeInfo();
            }

            // Dependent props
            currentType = type;
            while (currentType != null)
            {
                foreach (var itemPropInfo in currentType.DeclaredProperties)
                {
                    var propAttr = itemPropInfo.GetCustomAttribute<ModelPropertyAttribute>();
                    var typeSelectorAttrs = itemPropInfo.GetCustomAttributes<ModelTypeSelectorAttribute>().ToList();
                    if (propAttr != null && typeSelectorAttrs.Count > 0)
                    {
                        var propMapType = typeof(DependentPropertyMap<,>).MakeGenericType(typeof(T), itemPropInfo.PropertyType).GetTypeInfo();
                        var constructor = propMapType.DeclaredConstructors.Single();
                        var propMap = constructor.Invoke(new object[] { serializer, this, itemPropInfo, propAttr, typeSelectorAttrs, normalProps }) as PropertyMap<T>;

                        _propDict.Add(propMap.Key, propMap);
                        _propList.Add(new KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>(propMap.Key, propMap));
                    }
                }
                currentType = currentType.BaseType?.GetTypeInfo();
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

        internal void RegisterDependency(PropertyMap<T> keyProp)
        {
            if (keyProp.Index >= 32)
                throw new InvalidOperationException($"Model has more than 32 dependency keys");
            keyProp.Index = _selectorKeyCount++;
            keyProp.IndexMask = 1U << keyProp.Index.Value;
        }

        public T CreateUninitialized() => (T)FormatterServices.GetUninitializedObject(typeof(T));
    }
}
