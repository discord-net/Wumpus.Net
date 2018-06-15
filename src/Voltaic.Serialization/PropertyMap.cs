using System;
using System.Reflection;
using System.Text;

namespace Voltaic.Serialization
{
    public abstract class PropertyMap
    {
        public Serializer Serializer { get; }
        public ReadOnlyMemory<byte> Key { get; }
        public string Name { get; }
        public string Path { get; }
        public bool CanWrite { get; }
        public bool CanRead { get; }
        public bool ExcludeNull { get; }

        protected PropertyMap(Serializer serializer, ModelMap modelMap, PropertyInfo propInfo)
        {
            Serializer = serializer;

            Name = propInfo.Name;
            Path = $"{modelMap.Path}.{propInfo.Name}";

            CanWrite = propInfo.GetMethod != null;
            CanRead = propInfo.SetMethod != null;

            var attr = propInfo.GetCustomAttribute<ModelPropertyAttribute>();
            // TODO: Add support for other key types
            Key = new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(attr.Key));
            ExcludeNull = attr.ExcludeNull;
        }
    }

    public class PropertyMap<TModel, TValue> : PropertyMap
    {
        private readonly ValueConverter<TValue> _converter;
        private readonly Func<TModel, TValue> _getFunc;
        private readonly Action<TModel, TValue> _setFunc;

        public PropertyMap(
            Serializer serializer,
            ModelMap modelMap,
            PropertyInfo propInfo,
            ValueConverter<TValue> converter)
            : base(serializer, modelMap, propInfo)
        {
            _converter = converter;
            _getFunc = propInfo.GetMethod?.CreateDelegate(typeof(Func<TModel, TValue>)) as Func<TModel, TValue>;
            _setFunc = propInfo.SetMethod?.CreateDelegate(typeof(Action<TModel, TValue>)) as Action<TModel, TValue>;
        }

        public void Read(TModel model, ref ReadOnlySpan<byte> data)
        {
            if (!_converter.TryRead(Serializer, ref data, out var result, this))
                throw new SerializationException($"Failed to deserialize property {Path}");
            _setFunc(model, result);
        }

        public void Write(TModel model, ref ResizableMemory<byte> buffer)
        {
            if (!_converter.TryWrite(Serializer, ref buffer, _getFunc(model), this))
                throw new SerializationException($"Failed to serialize property {Path}");
        }
    }
}
