using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Voltaic.Serialization
{
    public abstract class Serializer
    {
        protected readonly ArrayPool<byte> _pool;

        private static readonly MethodInfo _createPropertyMapMethod
            = typeof(Serializer).GetTypeInfo().GetDeclaredMethod(nameof(CreatePropertyMap));
        private static readonly MethodInfo _writeMethod
            = typeof(Serializer).GetTypeInfo().GetDeclaredMethods(nameof(Write)).Single(x => x.IsGenericMethodDefinition);

        // public event Action<string, Exception> ModelError;
        // public event Action<string> UnmappedProperty;

        protected readonly ConcurrentDictionary<Type, ModelMap> _modelMaps;
        protected readonly ConcurrentDictionary<Type, Func<object, object, ArraySegment<byte>>> _writeMethods;
        protected readonly ConverterCollection _converters;

        protected Serializer(ConverterCollection converters = null, ArrayPool<byte> pool = null)
        {
            _pool = pool ?? ArrayPool<byte>.Shared;
            _modelMaps = new ConcurrentDictionary<Type, ModelMap>();
            _converters = converters ?? new ConverterCollection();
        }

        protected T Read<T>(ReadOnlySpan<byte> data, ValueConverter<T> converter = null)
        {
            if (converter == null)
                converter = _converters.Get<T>(this);
            if (!converter.TryRead(this, ref data, out var result))
                throw new SerializationException($"Failed to deserialize {typeof(T).Name}");
            return result;
        }

        protected ArraySegment<byte> Write(object value, object converter = null)
        {
            var type = value.GetType();
            var method = _writeMethods.GetOrAdd(type, t =>
            {
                return _writeMethod.MakeGenericMethod(t)
                    .CreateDelegate(typeof(Func<object, object, ArraySegment<byte>>))
                    as Func<object, object, ArraySegment<byte>>;
            });
            return method.Invoke(value, converter);
        }
        protected ArraySegment<byte> Write<T>(T value, ValueConverter<T> converter = null)
        {
            var writer = new MemoryBufferWriter<byte>(pool: _pool);
            if (converter == null)
                converter = _converters.Get<T>(this);
            if (!converter.TryWrite(this, ref writer, value))
                throw new SerializationException($"Failed to serialize {typeof(T).Name}");
            return writer.Formatted;
        }

        public ModelMap GetMap(Type modelType)
        {
            return _modelMaps.GetOrAdd(modelType, _ =>
            {
                var type = modelType.GetTypeInfo();
                var map = new ModelMap(type.Name);

                while (type != null)
                {
                    foreach (var propInfo in type.DeclaredProperties)
                    {
                        if (propInfo.GetCustomAttribute<ModelPropertyAttribute>() != null)
                        {
                            var propMap = MapProperty(map, modelType, propInfo);
                            if (propMap != null)
                                map.AddProperty(propMap);
                        }
                    }

                    type = type.BaseType?.GetTypeInfo();
                }
                return map;
            });
        }

        // Only used for top-level converters
        public ValueConverter<T> GetConverter<T>()
            => _converters.Get(this, typeof(T), null, true) as ValueConverter<T>;

        private PropertyMap MapProperty(ModelMap modelMap, Type modelType, PropertyInfo propInfo)
        {
            var method = _createPropertyMapMethod.MakeGenericMethod(modelType, propInfo.PropertyType);
            return method.Invoke(this, new object[] { modelMap, propInfo }) as PropertyMap;
        }
        protected PropertyMap<TModel, TValue> CreatePropertyMap<TModel, TValue>(ModelMap modelMap, PropertyInfo propInfo)
        {
            var converter = _converters.Get(this, typeof(TValue), propInfo) as ValueConverter<TValue>;
            return new PropertyMap<TModel, TValue>(this, modelMap, propInfo, converter);
        }
    }
}
