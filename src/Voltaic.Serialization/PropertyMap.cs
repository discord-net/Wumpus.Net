using System;
using System.Collections.Generic;
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
        public PropertyMap Dependency { get; protected set; }

        protected readonly bool _supportsRead, _supportsWrite;

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
        public ValueConverter<TValue> Converter { get; }
        public Func<TModel, TValue> GetFunc { get; }
        public Action<TModel, TValue> SetFunc { get; }

        public PropertyMap(
            Serializer serializer,
            ModelMap modelMap,
            PropertyInfo propInfo,
            ModelPropertyAttribute attr,
            ValueConverter<TValue> converter)
            : base(serializer, modelMap, propInfo, attr)
        {
            Converter = converter;
            GetFunc = propInfo.GetMethod?.CreateDelegate(typeof(Func<TModel, TValue>)) as Func<TModel, TValue>;
            SetFunc = propInfo.SetMethod?.CreateDelegate(typeof(Action<TModel, TValue>)) as Action<TModel, TValue>;
        }

        public override bool TryRead(TModel model, ref ReadOnlySpan<byte> data)
        {
            if (!Converter.TryRead(Serializer, ref data, out var result, this))
                return false;
            SetFunc(model, result);
            return true;
        }

        public override bool CanWrite(TModel model) => _supportsWrite && Converter.CanWrite(GetFunc(model), this);
        public override bool TryWrite(TModel model, ref ResizableMemory<byte> buffer)
        {
            if (!Converter.TryWrite(Serializer, ref buffer, GetFunc(model), this))
                return false;
            return true;
        }
    }

    public class DependentPropertyMap<TModel, TKey, TValue> : PropertyMap<TModel>
    {
        public IReadOnlyDictionary<TKey, ValueConverter<TValue>> ValueConverters { get; }
        public Func<TModel, TValue> GetFunc { get; }
        public Action<TModel, TValue> SetFunc { get; }

        public DependentPropertyMap(
            Serializer serializer,
            ModelMap modelMap,
            PropertyInfo propInfo,
            ModelPropertyAttribute attr,
            PropertyMap<TModel, TKey> keyProperty,
            Dictionary<TKey, ValueConverter<TValue>> converters)
            : base(serializer, modelMap, propInfo, attr)
        {
            Dependency = keyProperty;
            ValueConverters = converters;
            GetFunc = propInfo.GetMethod?.CreateDelegate(typeof(Func<TModel, TValue>)) as Func<TModel, TValue>;
            SetFunc = propInfo.SetMethod?.CreateDelegate(typeof(Action<TModel, TValue>)) as Action<TModel, TValue>;
        }

        public override bool TryRead(TModel model, ref ReadOnlySpan<byte> data)
        {
            if (!GetConverter(model, out var converter))
                return false;
            if (!converter.TryRead(Serializer, ref data, out var result, this))
                return false;
            SetFunc(model, result);
            return true;
        }

        public override bool CanWrite(TModel model)
        {
            if (!GetConverter(model, out var converter))
                return false;
            return _supportsWrite && converter.CanWrite(GetFunc(model), this);
        }
        public override bool TryWrite(TModel model, ref ResizableMemory<byte> buffer)
        {
            if (!GetConverter(model, out var converter))
                return false;
            if (!converter.TryWrite(Serializer, ref buffer, GetFunc(model), this))
                return false;
            return true;
        }

        private bool GetConverter(TModel model, out ValueConverter<TValue> converter)
            => ValueConverters.TryGetValue((Dependency as PropertyMap<TModel, TKey>).GetFunc(model), out converter);
    }
}
