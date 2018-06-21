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
        public bool ExcludeDefault { get; }
        public bool ExcludeNull { get; }
        public int? Index { get; internal set; }
        public PropertyMap Dependency { get; internal set; }

        protected bool _supportsRead, _supportsWrite;

        protected PropertyMap(Serializer serializer, ModelMap modelMap, PropertyInfo propInfo, ModelPropertyAttribute attr)
        {
            Serializer = serializer;

            Name = propInfo.Name;
            Path = $"{modelMap.Path}.{propInfo.Name}";

            _supportsWrite = propInfo.GetMethod != null;
            _supportsRead = propInfo.SetMethod != null;

            Key = new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(attr.Key));
            ExcludeNull = attr.ExcludeNull;
            ExcludeDefault = attr.ExcludeDefault;
        }
    }

    public abstract class PropertyMap<TModel> : PropertyMap
    {
        protected PropertyMap(Serializer serializer, ModelMap modelMap, PropertyInfo propInfo, ModelPropertyAttribute attr)
            : base(serializer, modelMap, propInfo, attr) { }

        public bool CanRead => _supportsRead;
        public abstract bool TryRead(TModel model, ref ReadOnlySpan<byte> data);

        public abstract bool CanWrite(TModel model);
        public abstract bool TryWrite(TModel model, ref ResizableMemory<byte> buffer);
    }

    public class PropertyMap<TModel, TValue> : PropertyMap<TModel>
    {
        private readonly ValueConverter<TValue> _converter;
        private readonly Func<TModel, TValue> _getFunc;
        private readonly Action<TModel, TValue> _setFunc;

        public PropertyMap(
            Serializer serializer,
            ModelMap modelMap,
            PropertyInfo propInfo,
            ModelPropertyAttribute attr,
            ValueConverter<TValue> converter)
            : base(serializer, modelMap, propInfo, attr)
        {
            _converter = converter;
            _getFunc = propInfo.GetMethod?.CreateDelegate(typeof(Func<TModel, TValue>)) as Func<TModel, TValue>;
            _setFunc = propInfo.SetMethod?.CreateDelegate(typeof(Action<TModel, TValue>)) as Action<TModel, TValue>;
        }

        public override bool TryRead(TModel model, ref ReadOnlySpan<byte> data)
        {
            if (!_converter.TryRead(Serializer, ref data, out var result, this))
                return false;
            _setFunc(model, result);
            return true;
        }

        public override bool CanWrite(TModel model) => _supportsWrite && _converter.CanWrite(_getFunc(model), this);
        public override bool TryWrite(TModel model, ref ResizableMemory<byte> buffer)
        {
            if (!_converter.TryWrite(Serializer, ref buffer, _getFunc(model), this))
                return false;
            return true;
        }
    }
}
