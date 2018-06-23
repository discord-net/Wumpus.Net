using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Voltaic.Serialization
{
    public abstract class ModelMap
    {
        public const int MaxDependencies = 8;

        public string Path { get; }

        internal readonly MemoryDictionary<PropertyMap> _propDict;
        internal readonly List<KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>> _propList;

        public IReadOnlyList<KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>> Properties => _propList;

        internal ModelMap(string path)
        {
            _propDict = new MemoryDictionary<PropertyMap>();
            _propList = new List<KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>>();
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
            var normalProps = new Dictionary<string, PropertyMap<T>>();
            var selectorProps = new MemoryDictionary<int>();

            // Normal props
            var currentType = type;
            while (currentType != null)
            {
                foreach (var itemPropInfo in currentType.DeclaredProperties)
                {
                    var propAttr = itemPropInfo.GetCustomAttribute<ModelPropertyAttribute>();
                    if (propAttr != null && itemPropInfo.GetCustomAttribute<ModelTypeSelectorAttribute>() == null)
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
                    var typeSelectorAttr = itemPropInfo.GetCustomAttribute<ModelTypeSelectorAttribute>();
                    if (propAttr != null && typeSelectorAttr != null)
                    {
                        if (!normalProps.TryGetValue(typeSelectorAttr.KeyProperty, out var depProp))
                            throw new InvalidOperationException($"Unable to find dependency \"{typeSelectorAttr.KeyProperty}\"");

                        // TODO: Does this search subtypes?
                        var typeMapAccessor = currentType.GetDeclaredProperty(typeSelectorAttr.MapProperty);
                        if (typeMapAccessor == null)
                            throw new InvalidOperationException($"Unable to find map \"{typeSelectorAttr.MapProperty}\"");
                        if (!typeMapAccessor.PropertyType.IsConstructedGenericType || 
                            typeMapAccessor.PropertyType.GetGenericTypeDefinition() != typeof(IReadOnlyDictionary<,>) ||
                            typeMapAccessor.PropertyType.GenericTypeArguments[1] != typeof(Type))
                            throw new InvalidOperationException($"Map must return an IReadOnlyDictionary<TKey,Type>");
                        var keyType = typeMapAccessor.PropertyType.GenericTypeArguments[0];
                        var converters = new object(); // TODO: Impl

                        var propMapType = typeof(DependentPropertyMap<,,>).MakeGenericType(typeof(T), keyType, itemPropInfo.PropertyType).GetTypeInfo();
                        var constructor = propMapType.DeclaredConstructors.Single();
                        var propMap = constructor.Invoke(new object[] { serializer, this, itemPropInfo, propAttr, depProp, converters }) as PropertyMap<T>;

                        _propDict.Add(propMap.Key, propMap);
                        _propList.Add(new KeyValuePair<ReadOnlyMemory<byte>, PropertyMap>(propMap.Key, propMap));

                        if (depProp.Index == 0)
                        {
                            depProp.Index = selectorProps.Count;
                            if (depProp.Index >= MaxDependencies)
                                throw new InvalidOperationException($"Model has more than {MaxDependencies} dependencies");
                            selectorProps.Add(depProp.Key, depProp.Index.Value);
                        }
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
    }
}
