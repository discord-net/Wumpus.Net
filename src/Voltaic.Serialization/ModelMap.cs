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

            // Ignored props
            var currentType = type;
            while (currentType != null)
            {
                var ignoredProps = currentType.GetCustomAttribute<IgnorePropertiesAttribute>();
                if (ignoredProps != null)
                {
                    for (int i = 0; i < ignoredProps.PropertyNames.Length; i++)
                        _propDict.Add(new Utf8String(ignoredProps.PropertyNames[i]), null);
                    currentType = currentType.BaseType?.GetTypeInfo();
                }
                currentType = currentType.BaseType?.GetTypeInfo();
            }

            // Normal props
            currentType = type;
            while (currentType != null)
            {
                foreach (var propInfo in currentType.DeclaredProperties)
                {
                    var propAttr = propInfo.GetCustomAttribute<ModelPropertyAttribute>();
                    if (propAttr != null && propInfo.GetCustomAttributes<ModelTypeSelectorAttribute>().Count() == 0)
                    {
                        var propMapType = typeof(PropertyMap<,>).MakeGenericType(typeof(T), propInfo.PropertyType).GetTypeInfo();
                        var constructor = propMapType.DeclaredConstructors.Single();
                        var converter = serializer.GetConverter(propInfo, true);
                        var propMap = constructor.Invoke(new object[] { serializer, this, propInfo, propAttr, converter }) as PropertyMap<T>;

                        _propDict.Add(propMap.Key, propMap);
                        _propList.Add(new KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>(propMap.Key, propMap));
                        normalProps.Add(propInfo.Name, propMap);
                    }
                }
                currentType = currentType.BaseType?.GetTypeInfo();
            }

            // Dependent props
            currentType = type;
            while (currentType != null)
            {
                foreach (var propInfo in currentType.DeclaredProperties)
                {
                    var propAttr = propInfo.GetCustomAttribute<ModelPropertyAttribute>();
                    var typeSelectorAttrs = propInfo.GetCustomAttributes<ModelTypeSelectorAttribute>().ToList();
                    if (propAttr != null && typeSelectorAttrs.Count > 0)
                    {
                        var propMapType = typeof(DependentPropertyMap<,>).MakeGenericType(typeof(T), propInfo.PropertyType).GetTypeInfo();
                        var constructor = propMapType.DeclaredConstructors.Single();
                        var propMap = constructor.Invoke(new object[] { serializer, this, propInfo, propAttr, typeSelectorAttrs, normalProps }) as PropertyMap<T>;

                        _propDict.Add(propMap.Key, propMap);
                        _propList.Add(new KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>(propMap.Key, propMap));
                    }
                }
                currentType = currentType.BaseType?.GetTypeInfo();
            }
        }

        public bool TryGetProperty(ReadOnlySpan<byte> key, out PropertyMap<T> value, out bool isIgnored)
        {
            value = default;
            isIgnored = false;

            if (!_propDict.TryGetValue(key, out var untypedValue))
                return false;
            if (untypedValue == null)
            {
                isIgnored = true;
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
