using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Voltaic.Serialization
{
    public abstract class Serializer
    {
        public event Action<string> UnknownProperty;
        public event Action<string> FailedProperty;

        private delegate object ReadMethod(ReadOnlySpan<byte> data, ValueConverter converter);
        private delegate ResizableMemory<byte> WriteMethod(object data, ValueConverter converter);

        protected readonly ArrayPool<byte> _pool;
        private static readonly MethodInfo _readMethod
            = typeof(Serializer).GetTypeInfo().GetDeclaredMethods(nameof(ReadInternal))
            .Single(x => x.IsGenericMethodDefinition);
        private static readonly MethodInfo _writeMethod
            = typeof(Serializer).GetTypeInfo().GetDeclaredMethods(nameof(WriteInternal))
            .Single(x => x.IsGenericMethodDefinition);

        protected readonly ConverterCollection _converters;
        protected readonly ConcurrentDictionary<Type, ModelMap> _modelMaps;
        private readonly ConcurrentDictionary<Type, ReadMethod> _readMethods;
        private readonly ConcurrentDictionary<Type, WriteMethod> _writeMethods;

        protected Serializer(ConverterCollection converters = null, ArrayPool<byte> pool = null)
        {
            _pool = pool ?? ArrayPool<byte>.Shared;
            _modelMaps = new ConcurrentDictionary<Type, ModelMap>();
            _readMethods = new ConcurrentDictionary<Type, ReadMethod>();
            _writeMethods = new ConcurrentDictionary<Type, WriteMethod>();
            _converters = converters ?? new ConverterCollection();
        }

        public object Read(Type type, ReadOnlyMemory<byte> data, ValueConverter converter = null)
            => Read(type, data.Span, converter);
        public virtual object Read(Type type, ReadOnlySpan<byte> data, ValueConverter converter = null)
        {
            var method = _readMethods.GetOrAdd(type, t =>
                _readMethod.MakeGenericMethod(t).CreateDelegate(typeof(ReadMethod), this) as ReadMethod);
            return method.Invoke(data, converter);
        }
        private object ReadInternal<T>(ReadOnlySpan<byte> data, ValueConverter converter = null)
            => Read<T>(data, (ValueConverter<T>)converter);
        public T Read<T>(ReadOnlyMemory<byte> data, ValueConverter<T> converter = null)
            => Read<T>(data.Span, converter);
        public virtual T Read<T>(ReadOnlySpan<byte> data, ValueConverter<T> converter = null)
        {
            if (converter == null)
                converter = _converters.Get<T>(this);
            if (!converter.TryRead(ref data, out var result))
                throw new SerializationException($"Failed to deserialize {typeof(T).Name}");
            return result;
        }

        public ResizableMemory<byte> Write(object value, ValueConverter converter = null)
        {
            var type = value.GetType();
            var method = _writeMethods.GetOrAdd(type, t =>
                _writeMethod.MakeGenericMethod(t).CreateDelegate(typeof(WriteMethod), this) as WriteMethod);
            return method.Invoke(value, converter);
        }
        private ResizableMemory<byte> WriteInternal<T>(object value, ValueConverter converter = null)
            => Write((T)value, (ValueConverter<T>)converter);
        public ResizableMemory<byte> Write<T>(T value, ValueConverter<T> converter = null)
        {
            var writer = new ResizableMemory<byte>(1024, pool: _pool);
            Write(value, ref writer, converter);
            return writer;
        }
        public virtual void Write<T>(T value, ref ResizableMemory<byte> writer, ValueConverter<T> converter = null)
        {
            if (converter == null)
                converter = _converters.Get<T>(this);
            if (!converter.TryWrite(ref writer, value))
                throw new SerializationException($"Failed to serialize {typeof(T).Name}");
        }

        public ModelMap GetMap(Type modelType)
        {
            return _modelMaps.GetOrAdd(modelType, _ =>
            {
                var method = typeof(ModelMap<>).MakeGenericType(modelType).GetTypeInfo().DeclaredConstructors.Single();
                return method.Invoke(new object[] { this, modelType.Name }) as ModelMap;
            });
        }
        public ModelMap<T> GetMap<T>()
            => _modelMaps.GetOrAdd(typeof(T), _ => new ModelMap<T>(this)) as ModelMap<T>;

        public ValueConverter GetConverter(Type type, PropertyInfo propInfo = null, bool throwOnNotFound = false)
            => _converters.Get(this, type, propInfo, throwOnNotFound);
        public ValueConverter GetConverter(PropertyInfo propInfo, bool throwOnNotFound = false)
            => _converters.Get(this, propInfo.PropertyType, propInfo, throwOnNotFound);
        public ValueConverter<T> GetConverter<T>(Type type, PropertyInfo propInfo = null, bool throwOnNotFound = false)
            => _converters.Get<T>(this, type, propInfo, throwOnNotFound);
        public ValueConverter<T> GetConverter<T>(PropertyInfo propInfo = null, bool throwOnNotFound = false)
            => _converters.Get<T>(this, typeof(T), propInfo, throwOnNotFound);

        protected void RaiseUnknownProperty(ModelMap model, Utf8String propName)
        {
            if (UnknownProperty != null)
                UnknownProperty?.Invoke($"{model.Name}.{propName.ToString()}");
        }
        protected void RaiseUnknownProperty(ModelMap model, string propName)
        {
            if (UnknownProperty != null)
                UnknownProperty?.Invoke($"{model.Name}.{propName}");
        }
        protected void RaiseFailedProperty(ModelMap model, PropertyMap prop)
        {
            if (FailedProperty != null)
                FailedProperty?.Invoke($"{model.Name}.{prop.Name}");
        }
        protected void RaiseFailedProperty(PropertyMap prop, int i)
        {
            if (FailedProperty != null)
            {
                if (prop != null)
                    FailedProperty?.Invoke($"{prop.Name}[{i}]");
                else
                    FailedProperty?.Invoke($"[{i}]");
            }
        }
    }
}
