using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Voltaic.Serialization
{
    public class ConverterCollection
    {
        private struct GenericConverterType
        {
            public readonly Type Type;
            public readonly Func<TypeInfo, Type>[] InnerTypeSelectors;

            public GenericConverterType(Type type, params Func<TypeInfo, Type>[] innerTypeSelectors)
            {
                Type = type;
                InnerTypeSelectors = innerTypeSelectors;
            }

            public Type BuildClosedType(TypeInfo valueType)
            {
                var innerTypes = new Type[InnerTypeSelectors.Length];
                for (int i = 0; i < InnerTypeSelectors.Length; i++)
                    innerTypes[i] = InnerTypeSelectors[i](valueType);
                return valueType.MakeGenericType(innerTypes);
            }
        }

        private struct ConditionalConverter
        {
            public readonly Func<TypeInfo, PropertyInfo, bool> Condition;
            public readonly Type Type;

            public ConditionalConverter(Type type, Func<TypeInfo, PropertyInfo, bool> condition)
            {
                Type = type;
                Condition = condition;
            }
        }
        private struct GenericConditionalConverter
        {
            public GenericConverterType GenericType;
            public Func<TypeInfo, PropertyInfo, bool> Condition;

            public GenericConditionalConverter(GenericConverterType genericType, Func<TypeInfo, PropertyInfo, bool> condition)
            {
                GenericType = genericType;
                Condition = condition;
            }
        }

        private class ConverterTypeCollection
        {
            public Type DefaultConverter;
            public List<ConditionalConverter> ConditionalConverters = new List<ConditionalConverter>();
        }
        private class GenericConverterTypeCollection
        {
            public GenericConverterType? DefaultConverter;
            public List<GenericConditionalConverter> ConditionalConverters = new List<GenericConditionalConverter>();
        }

        private static readonly TypeInfo _serializerType = typeof(Serializer).GetTypeInfo();

        private readonly ConcurrentDictionary<Type, object> _cache;
        private readonly Dictionary<Type, ConverterTypeCollection> _types;
        private readonly Dictionary<Type, GenericConverterTypeCollection> _mappedGenericTypes;
        private readonly GenericConverterTypeCollection _globalGenericTypes;

        internal ConverterCollection()
        {
            _cache = new ConcurrentDictionary<Type, object>();
            _types = new Dictionary<Type, ConverterTypeCollection>();
            _mappedGenericTypes = new Dictionary<Type, GenericConverterTypeCollection>();
            _globalGenericTypes = new GenericConverterTypeCollection();
        }

        // A -> B
        public void SetDefault<TType, TConverter>()
            where TConverter : ValueConverter<TType>
            => SetDefault(typeof(TType), typeof(TConverter));
        public void SetDefault(Type type, Type converterType)
        {
            if (!_types.TryGetValue(type, out var converters))
                _types.Add(type, converters = new ConverterTypeCollection());
            converters.DefaultConverter = converterType;
        }
        public void AddConditional<TType, TConverter>(Func<TypeInfo, PropertyInfo, bool> condition)
            where TConverter : ValueConverter<TType>
            => AddConditional(typeof(TType), typeof(TConverter), condition);
        public void AddConditional(Type type, Type converterType, Func<TypeInfo, PropertyInfo, bool> condition)
        {
            if (!_types.TryGetValue(type, out var converters))
                _types.Add(type, converters = new ConverterTypeCollection());
            converters.ConditionalConverters.Add(new ConditionalConverter(converterType, condition));
        }

        // A<B> -> B
        public void SetGlobalDefault(Type openConverterType, params Func<TypeInfo, Type>[] innerTypeSelectors)
        {
            if (openConverterType.IsConstructedGenericType)
                throw new InvalidOperationException($"{nameof(openConverterType)} must be an open generic");
            if (openConverterType.GenericTypeArguments.Length != 1)
                throw new InvalidOperationException($"{nameof(openConverterType)} must have 1 generic argument");

            _globalGenericTypes.DefaultConverter = new GenericConverterType(openConverterType, innerTypeSelectors);
        }
        public void AddGlobalConditional(Type openConverterType, Func<TypeInfo, PropertyInfo, bool> condition, params Func<TypeInfo, Type>[] innerTypeSelectors)
        {
            if (openConverterType.IsConstructedGenericType)
                throw new InvalidOperationException($"{nameof(openConverterType)} must be an open generic");
            if (openConverterType.GenericTypeArguments.Length != 1)
                throw new InvalidOperationException($"{nameof(openConverterType)} must have 1 generic argument");

            _globalGenericTypes.ConditionalConverters.Add(new GenericConditionalConverter(new GenericConverterType(openConverterType, innerTypeSelectors), condition));
        }

        // A<C> -> B<C>
        public void SetGenericDefault(Type openType, Type openConverterType, params Func<TypeInfo, Type>[] innerTypeSelectors)
        {
            if (openType.IsConstructedGenericType)
                throw new InvalidOperationException($"{nameof(openType)} must be an open generic");
            if (openConverterType.IsConstructedGenericType)
                throw new InvalidOperationException($"{nameof(openConverterType)} must be an open generic");
            if (innerTypeSelectors.Length != openConverterType.GenericTypeArguments.Length)
                throw new InvalidOperationException($"{nameof(innerTypeSelectors)} must be the same length as generic args in {nameof(openConverterType)}");

            if (!_mappedGenericTypes.TryGetValue(openType, out var converters))
                _mappedGenericTypes.Add(openType, converters = new GenericConverterTypeCollection());
            converters.DefaultConverter = new GenericConverterType(openConverterType, innerTypeSelectors);
        }
        public void AddGenericConditional(Type openType, Type openConverterType, Func<TypeInfo, PropertyInfo, bool> condition, params Func<TypeInfo, Type>[] innerTypeSelectors)
        {
            if (openType.IsConstructedGenericType)
                throw new InvalidOperationException($"{nameof(openType)} must be an open generic");
            if (openConverterType.IsConstructedGenericType)
                throw new InvalidOperationException($"{nameof(openConverterType)} must be an open generic");
            if (innerTypeSelectors.Length != openConverterType.GenericTypeArguments.Length)
                throw new InvalidOperationException($"{nameof(innerTypeSelectors)} must be the same length as generic args in {nameof(openConverterType)}");

            if (!_mappedGenericTypes.TryGetValue(openType, out var converters))
                _mappedGenericTypes.Add(openType, converters = new GenericConverterTypeCollection());
            converters.ConditionalConverters.Add(new GenericConditionalConverter(new GenericConverterType(openConverterType, innerTypeSelectors), condition));
        }

        internal ValueConverter<T> Get<T>(Serializer serializer, PropertyInfo propInfo = null, bool throwOnNotFound = true)
            => Get(serializer, typeof(T), propInfo, throwOnNotFound) as ValueConverter<T>;
        internal object Get(Serializer serializer, Type type, PropertyInfo propInfo = null, bool throwOnNotFound = true)
        {
            bool canCache = propInfo == null; // Can't cache top-level due to attribute influences

            if (canCache && _cache.TryGetValue(type, out var converter))
                return converter;

            var converterType = FindConverterType(type, type.GetTypeInfo(), propInfo);
            if (converterType == null && throwOnNotFound)
                throw new InvalidOperationException($"There is no converter available for {type.Name}");

            converter = BuildConverter(serializer, converterType, throwOnNotFound);
            if (canCache)
                _cache.TryAdd(type, converter);
            return converter;
        }

        private object BuildConverter(Serializer serializer, Type converterType, bool throwOnNotFound)
        {
            var converterTypeInfo = converterType.GetTypeInfo();

            var constructors = converterTypeInfo.DeclaredConstructors.Where(x => !x.IsStatic).ToArray();
            if (constructors.Length == 0)
                throw new SerializationException($"{converterType.Name} is missing a constructor");
            if (constructors.Length > 1)
                throw new SerializationException($"{converterType.Name} has multiple constructors");
            var constructor = constructors[0];
            var parameters = constructor.GetParameters();

            var args = new object[parameters.Length];
            for (int i = 0; i < args.Length; i++)
            {
                var paramType = parameters[i].ParameterType;
                if (parameters[i].Name == "innerConverters")
                    args[i] = converterTypeInfo.GenericTypeArguments.Select(x => Get(serializer, x, null, throwOnNotFound)).ToArray();
                else if (_serializerType.IsAssignableFrom(paramType.GetTypeInfo()))
                    args[i] = serializer;
                else
                    throw new SerializationException($"{converterType.Name} has an unsupported constructor");
            }

            return constructor.Invoke(args);
        }

        private Type FindConverterType(Type valueType, TypeInfo valueTypeInfo, PropertyInfo propInfo)
        {
            var converterType = FindDirectConverterType(valueType, valueTypeInfo, propInfo);
            if (converterType != null)
                return converterType;

            if (valueTypeInfo.IsGenericType && valueTypeInfo.GenericTypeArguments.Length != 0)
            {
                converterType = FindMappedGenericConverterType(valueType, valueTypeInfo, propInfo);
                if (converterType != null)
                    return converterType;
            }

            converterType = FindGlobalGenericConverterType(valueType, valueTypeInfo, propInfo);
            if (converterType != null)
                return converterType;
            return null;
        }

        private Type FindDirectConverterType(Type valueType, TypeInfo valueTypeInfo, PropertyInfo propInfo)
        {
            if (!_types.TryGetValue(valueType, out var converters))
                return null;

            for (int i = 0; i < converters.ConditionalConverters.Count; i++)
            {
                if (converters.ConditionalConverters[i].Condition(valueTypeInfo, propInfo))
                    return converters.ConditionalConverters[i].Type;
            }
            return converters.DefaultConverter;
        }

        private Type FindMappedGenericConverterType(Type valueType, TypeInfo valueTypeInfo, PropertyInfo propInfo)
        {
            if (!_mappedGenericTypes.TryGetValue(valueType.GetGenericTypeDefinition(), out var converters))
                return null;

            for (int i = 0; i < converters.ConditionalConverters.Count; i++)
            {
                if (converters.ConditionalConverters[i].Condition(valueTypeInfo, propInfo))
                    return converters.ConditionalConverters[i].GenericType.BuildClosedType(valueTypeInfo);
            }
            return converters.DefaultConverter?.BuildClosedType(valueTypeInfo);
        }

        private Type FindGlobalGenericConverterType(Type valueType, TypeInfo valueTypeInfo, PropertyInfo propInfo)
        {
            for (int i = 0; i < _globalGenericTypes.ConditionalConverters.Count; i++)
            {
                if (_globalGenericTypes.ConditionalConverters[i].Condition(valueTypeInfo, propInfo))
                    return _globalGenericTypes.ConditionalConverters[i].GenericType.BuildClosedType(valueTypeInfo);
            }
            return _globalGenericTypes.DefaultConverter?.BuildClosedType(valueTypeInfo);
        }
    }
}
