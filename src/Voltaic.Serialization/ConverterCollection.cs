using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Voltaic.Serialization
{
    public class ConverterCollection
    {
        private struct Definition
        {
            public readonly Type Type;
            public readonly Func<TypeInfo, PropertyInfo, ValueConverter> Factory;

            public Definition(Type type, Func<TypeInfo, PropertyInfo, ValueConverter> factory)
            {
                Type = type;
                Factory = factory;
            }
        }
        private struct GenericDefinition
        {
            public readonly Type Type;
            public readonly Func<TypeInfo, PropertyInfo, ValueConverter> Factory;
            public readonly Func<TypeInfo, Type>[] InnerTypeSelectors;

            public GenericDefinition(Type type, Func<TypeInfo, PropertyInfo, ValueConverter> factory)
            {
                Type = type;
                Factory = factory;
                InnerTypeSelectors = new Func<TypeInfo, Type>[0];
            }
            public GenericDefinition(Type type, params Func<TypeInfo, Type>[] innerTypeSelectors)
            {
                Type = type;
                Factory = null;
                InnerTypeSelectors = innerTypeSelectors;
            }

            public Type BuildClosedType(TypeInfo valueType)
            {
                var innerTypes = new Type[InnerTypeSelectors.Length];
                for (int i = 0; i < InnerTypeSelectors.Length; i++)
                    innerTypes[i] = InnerTypeSelectors[i](valueType);
                return Type.MakeGenericType(innerTypes);
            }
        }

        private struct ConditionalDefinition
        {
            public readonly Definition InnerDef;
            public readonly Func<TypeInfo, PropertyInfo, bool> Condition;

            public ConditionalDefinition(Definition innerDef, Func<TypeInfo, PropertyInfo, bool> condition)
            {
                InnerDef = innerDef;
                Condition = condition;
            }
        }
        private struct GenericConditionalDefinition
        {
            public readonly GenericDefinition InnerDef;
            public readonly Func<TypeInfo, PropertyInfo, bool> Condition;

            public GenericConditionalDefinition(GenericDefinition innerDef, Func<TypeInfo, PropertyInfo, bool> condition)
            {
                InnerDef = innerDef;
                Condition = condition;
            }
        }

        private class ConverterTypeCollection
        {
            public Definition? DefaultConverter;
            public List<ConditionalDefinition> ConditionalConverters = new List<ConditionalDefinition>();
        }
        private class GenericConverterTypeCollection
        {
            public GenericDefinition? DefaultConverter;
            public List<GenericConditionalDefinition> ConditionalConverters = new List<GenericConditionalDefinition>();
        }

        private static readonly TypeInfo _serializerType = typeof(Serializer).GetTypeInfo();

        private readonly ConcurrentDictionary<Type, ValueConverter> _cache;
        private readonly Dictionary<Type, ConverterTypeCollection> _types;
        private readonly Dictionary<Type, GenericConverterTypeCollection> _mappedGenericTypes;
        private readonly GenericConverterTypeCollection _globalGenericTypes;

        internal ConverterCollection()
        {
            _cache = new ConcurrentDictionary<Type, ValueConverter>();
            _types = new Dictionary<Type, ConverterTypeCollection>();
            _mappedGenericTypes = new Dictionary<Type, GenericConverterTypeCollection>();
            _globalGenericTypes = new GenericConverterTypeCollection();
        }

        // A -> B
        public void SetDefault<TType, TConverter>(Func<TypeInfo, PropertyInfo, TConverter> factory = null)
            where TConverter : ValueConverter<TType>
            => SetDefault(typeof(TType), typeof(TConverter), factory);
        public void SetDefault(Type type, Type converterType, Func<TypeInfo, PropertyInfo, ValueConverter> factory = null)
        {
            if (!_types.TryGetValue(type, out var converters))
                _types.Add(type, converters = new ConverterTypeCollection());
            converters.DefaultConverter = new Definition(converterType, factory);
        }
        public void AddConditional<TType, TConverter>(Func<TypeInfo, PropertyInfo, bool> condition, Func<TypeInfo, PropertyInfo, TConverter> factory = null)
            where TConverter : ValueConverter<TType>
            => AddConditional(typeof(TType), typeof(TConverter), condition, factory);
        public void AddConditional(Type type, Type converterType, Func<TypeInfo, PropertyInfo, bool> condition, Func<TypeInfo, PropertyInfo, ValueConverter> factory = null)
        {
            if (!_types.TryGetValue(type, out var converters))
                _types.Add(type, converters = new ConverterTypeCollection());
            converters.ConditionalConverters.Add(new ConditionalDefinition(new Definition(converterType, factory), condition));
        }

        // A<B> -> B
        public void SetGlobalDefault(Type openConverterType, params Func<TypeInfo, Type>[] innerTypeSelectors)
        {
            if (openConverterType.IsConstructedGenericType)
                throw new InvalidOperationException($"{nameof(openConverterType)} must be an open generic");
            if (openConverterType.GetTypeInfo().GenericTypeParameters.Length != 1)
                throw new InvalidOperationException($"{nameof(openConverterType)} must have 1 generic parameter");

            _globalGenericTypes.DefaultConverter = new GenericDefinition(openConverterType, innerTypeSelectors);
        }
        public void SetGlobalDefault(Type openConverterType, Func<TypeInfo, PropertyInfo, ValueConverter> factory)
        {
            if (openConverterType.IsConstructedGenericType)
                throw new InvalidOperationException($"{nameof(openConverterType)} must be an open generic");
            if (openConverterType.GetTypeInfo().GenericTypeParameters.Length != 1)
                throw new InvalidOperationException($"{nameof(openConverterType)} must have 1 generic parameter");

            _globalGenericTypes.DefaultConverter = new GenericDefinition(openConverterType, factory);
        }
        public void AddGlobalConditional(Type openConverterType, Func<TypeInfo, PropertyInfo, bool> condition, params Func<TypeInfo, Type>[] innerTypeSelectors)
        {
            if (openConverterType.IsConstructedGenericType)
                throw new InvalidOperationException($"{nameof(openConverterType)} must be an open generic");
            if (openConverterType.GetTypeInfo().GenericTypeParameters.Length != 1)
                throw new InvalidOperationException($"{nameof(openConverterType)} must have 1 generic parameter");

            _globalGenericTypes.ConditionalConverters.Add(new GenericConditionalDefinition(new GenericDefinition(openConverterType, innerTypeSelectors), condition));
        }
        public void AddGlobalConditional(Type openConverterType, Func<TypeInfo, PropertyInfo, bool> condition, Func<TypeInfo, PropertyInfo, ValueConverter> factory)
        {
            if (openConverterType.IsConstructedGenericType)
                throw new InvalidOperationException($"{nameof(openConverterType)} must be an open generic");
            if (openConverterType.GetTypeInfo().GenericTypeParameters.Length != 1)
                throw new InvalidOperationException($"{nameof(openConverterType)} must have 1 generic parameter");

            _globalGenericTypes.ConditionalConverters.Add(new GenericConditionalDefinition(new GenericDefinition(openConverterType, factory), condition));
        }

        // A<C> -> B<C>
        public void SetGenericDefault(Type openType, Type openConverterType, params Func<TypeInfo, Type>[] innerTypeSelectors)
        {
            if (openType.IsConstructedGenericType)
                throw new InvalidOperationException($"{nameof(openType)} must be an open generic");
            if (openConverterType.IsConstructedGenericType)
                throw new InvalidOperationException($"{nameof(openConverterType)} must be an open generic");
            if (innerTypeSelectors.Length != openConverterType.GetTypeInfo().GenericTypeParameters.Length)
                throw new InvalidOperationException($"{nameof(innerTypeSelectors)} must be the same length as generic params in {nameof(openConverterType)}");

            if (!_mappedGenericTypes.TryGetValue(openType, out var converters))
                _mappedGenericTypes.Add(openType, converters = new GenericConverterTypeCollection());
            converters.DefaultConverter = new GenericDefinition(openConverterType, innerTypeSelectors);
        }
        public void AddGenericConditional(Type openType, Type openConverterType, Func<TypeInfo, PropertyInfo, bool> condition, params Func<TypeInfo, Type>[] innerTypeSelectors)
        {
            if (openType.IsConstructedGenericType)
                throw new InvalidOperationException($"{nameof(openType)} must be an open generic");
            if (openConverterType.IsConstructedGenericType)
                throw new InvalidOperationException($"{nameof(openConverterType)} must be an open generic");
            if (innerTypeSelectors.Length != openConverterType.GetTypeInfo().GenericTypeParameters.Length)
                throw new InvalidOperationException($"{nameof(innerTypeSelectors)} must be the same length as generic params in {nameof(openConverterType)}");

            if (!_mappedGenericTypes.TryGetValue(openType, out var converters))
                _mappedGenericTypes.Add(openType, converters = new GenericConverterTypeCollection());
            converters.ConditionalConverters.Add(new GenericConditionalDefinition(new GenericDefinition(openConverterType, innerTypeSelectors), condition));
        }

        internal ValueConverter<T> Get<T>(Serializer serializer, PropertyInfo propInfo = null, bool throwOnNotFound = true)
            => Get<T>(serializer, typeof(T), propInfo, throwOnNotFound);
        internal ValueConverter<T> Get<T>(Serializer serializer, Type type, PropertyInfo propInfo = null, bool throwOnNotFound = true)
        {
            var converter = Get(serializer, type, propInfo, throwOnNotFound);
            if (converter is ValueConverter<T> genericConverter)
                return genericConverter;

            // Types dont match, build an adapter
            // TODO: How much of a perf is doing uncached reflection here?
            if (!typeof(T).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
                throw new InvalidOperationException($"Converter for {type.Name} is not assignable to {typeof(T).Name}");
            var adapterType = typeof(ValueConverterAdapter<,>).MakeGenericType(typeof(T), type).GetTypeInfo();
            var constructor = adapterType.DeclaredConstructors.Single();
            return constructor.Invoke(new object[] { converter }) as ValueConverter<T>;
        }
        internal ValueConverter Get(Serializer serializer, Type type, PropertyInfo propInfo = null, bool throwOnNotFound = true)
        {
            bool canCache = propInfo == null; // Can't cache top-level due to attribute influences
            if (canCache && _cache.TryGetValue(type, out var converter))
                return converter;

            converter = FindAndBuildConverter(serializer, type.GetTypeInfo(), propInfo, throwOnNotFound);
            if (converter == null && throwOnNotFound)
                throw new InvalidOperationException($"There is no converter available for {type.Name}");

            if (canCache)
                _cache.TryAdd(type, converter);
            return converter;
        }        

        private ValueConverter FindAndBuildConverter(Serializer serializer, TypeInfo valueTypeInfo, PropertyInfo propInfo, bool throwOnNotFound)
        {
            var converterType = FindDirectConverterType(valueTypeInfo, propInfo);
            if (converterType != null)
            {
                if (converterType.Value.Factory != null)
                    return converterType.Value.Factory(valueTypeInfo, propInfo);
                else
                    return BuildConverter(serializer, converterType.Value.Type, propInfo, throwOnNotFound);
            }

            if (valueTypeInfo.IsGenericType && valueTypeInfo.GenericTypeArguments.Length != 0)
            {
                var genericType = FindMappedGenericConverterType(valueTypeInfo, propInfo);
                if (genericType != null)
                {
                    if (genericType.Value.Factory != null)
                        return genericType.Value.Factory(valueTypeInfo, propInfo);
                    else
                        return BuildConverter(serializer, genericType.Value.BuildClosedType(valueTypeInfo), propInfo, throwOnNotFound);
                }
            }

            var globalType = FindGlobalGenericConverterType(valueTypeInfo, propInfo);
            if (globalType != null)
            {
                if (globalType.Value.Factory != null)
                    return globalType.Value.Factory(valueTypeInfo, propInfo);
                else
                    return BuildConverter(serializer, globalType.Value.BuildClosedType(valueTypeInfo), propInfo, throwOnNotFound);
            }
            return null;
        }

        private ValueConverter BuildConverter(Serializer serializer, Type converterType, PropertyInfo propInfo, bool throwOnNotFound)
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
            int innerConverters = 0;
            for (int i = 0; i < args.Length; i++)
            {
                var paramType = parameters[i].ParameterType;
                var paramInfo = paramType.GetTypeInfo();
                if (paramInfo.IsGenericType && paramInfo.GetGenericTypeDefinition() == typeof(ValueConverter<>))
                    args[i] = Get(serializer, converterTypeInfo.GenericTypeArguments[innerConverters++], propInfo, throwOnNotFound);
                else if (_serializerType.IsAssignableFrom(paramInfo))
                    args[i] = serializer;
                else if (paramType == typeof(PropertyInfo))
                    args[i] = propInfo;
                else if (!parameters[i].HasDefaultValue)
                    throw new SerializationException($"{converterType.Name} has an unsupported constructor");
            }

            return constructor.Invoke(args) as ValueConverter;
        }

        private Definition? FindDirectConverterType(TypeInfo valueTypeInfo, PropertyInfo propInfo)
        {
            if (!_types.TryGetValue(valueTypeInfo.AsType(), out var converters))
                return null;

            for (int i = 0; i < converters.ConditionalConverters.Count; i++)
            {
                if (converters.ConditionalConverters[i].Condition(valueTypeInfo, propInfo))
                    return converters.ConditionalConverters[i].InnerDef;
            }
            return converters.DefaultConverter;
        }
        private GenericDefinition? FindMappedGenericConverterType(TypeInfo valueTypeInfo, PropertyInfo propInfo)
        {
            if (!_mappedGenericTypes.TryGetValue(valueTypeInfo.GetGenericTypeDefinition(), out var converters))
                return null;

            for (int i = 0; i < converters.ConditionalConverters.Count; i++)
            {
                if (converters.ConditionalConverters[i].Condition(valueTypeInfo, propInfo))
                    return converters.ConditionalConverters[i].InnerDef;
            }
            return converters.DefaultConverter;
        }
        private GenericDefinition? FindGlobalGenericConverterType(TypeInfo valueTypeInfo, PropertyInfo propInfo)
        {
            for (int i = 0; i < _globalGenericTypes.ConditionalConverters.Count; i++)
            {
                if (_globalGenericTypes.ConditionalConverters[i].Condition(valueTypeInfo, propInfo))
                    return _globalGenericTypes.ConditionalConverters[i].InnerDef;
            }
            return _globalGenericTypes.DefaultConverter;
        }
    }
}
