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
        private static readonly MethodInfo _writeMethod
            = typeof(Serializer).GetTypeInfo().GetDeclaredMethods(nameof(WriteInternal))
            .Single(x => x.IsGenericMethodDefinition);

        protected readonly ConcurrentDictionary<Type, ModelMap> _modelMaps;
        protected readonly ConcurrentDictionary<Type, Func<object, object, ResizableMemory<byte>>> _writeMethods;
        protected readonly ConverterCollection _converters;

        protected Serializer(ConverterCollection converters = null, ArrayPool<byte> pool = null)
        {
            _pool = pool ?? ArrayPool<byte>.Shared;
            _modelMaps = new ConcurrentDictionary<Type, ModelMap>();
            _writeMethods = new ConcurrentDictionary<Type, Func<object, object, ResizableMemory<byte>>>();
            _converters = converters ?? new ConverterCollection();
        }

        public T Read<T>(ReadOnlySpan<byte> data, ValueConverter<T> converter = null)
        {
            if (converter == null)
                converter = _converters.Get<T>(this);
            if (!converter.TryRead(ref data, out var result))
                throw new SerializationException($"Failed to deserialize {typeof(T).Name}");
            return result;
        }

        public ResizableMemory<byte> Write(object value, object converter = null)
        {
            var type = value.GetType();
            var method = _writeMethods.GetOrAdd(type, t =>
            {
                return _writeMethod.MakeGenericMethod(t)
                    .CreateDelegate(typeof(Func<object, object, ResizableMemory<byte>>), this)
                    as Func<object, object, ResizableMemory<byte>>;
            });
            return method.Invoke(value, converter);
        }
        private ResizableMemory<byte> WriteInternal<T>(object value, object converter = null)
            => Write((T)value, (ValueConverter<T>)converter);
        public ResizableMemory<byte> Write<T>(T value, ValueConverter<T> converter = null)
        {
            var writer = new ResizableMemory<byte>(1024, pool: _pool);
            if (converter == null)
                converter = _converters.Get<T>(this);
            if (!converter.TryWrite(ref writer, value))
                throw new SerializationException($"Failed to serialize {typeof(T).Name}");
            return writer;
        }

        public ModelMap GetMap(Type modelType, PropertyInfo propInfo = null)
        {
            return _modelMaps.GetOrAdd(modelType, _ =>
            {
                var method = typeof(ModelMap<>).MakeGenericType(modelType).GetTypeInfo().DeclaredConstructors.Single();
                return method.Invoke(new object[] { this, modelType.Name, propInfo }) as ModelMap;
            });
        }
        public ModelMap<T> GetMap<T>(PropertyInfo propInfo = null)
            => _modelMaps.GetOrAdd(typeof(T), _ => new ModelMap<T>(this, typeof(T).Name, propInfo)) as ModelMap<T>;

        public ValueConverter GetConverter(Type type, PropertyInfo propInfo = null, bool throwOnNotFound = false)
            => _converters.Get(this, type, propInfo, throwOnNotFound);
        public ValueConverter GetConverter(PropertyInfo propInfo, bool throwOnNotFound = false)
            => _converters.Get(this, propInfo.PropertyType, propInfo, throwOnNotFound);
        public ValueConverter<T> GetConverter<T>(Type type, PropertyInfo propInfo = null, bool throwOnNotFound = false)
            => _converters.Get<T>(this, type, propInfo, throwOnNotFound);
        public ValueConverter<T> GetConverter<T>(PropertyInfo propInfo = null, bool throwOnNotFound = false)
            => _converters.Get<T>(this, typeof(T), propInfo, throwOnNotFound);

    }
}
