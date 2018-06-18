using System;
using System.Buffers;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
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
        public int SelectorIndex { get; internal set; }
        public IReadOnlyList<Memory<byte>> Dependencies { get; }

        protected bool _supportsRead, _supportsWrite;
        
        protected PropertyMap(Serializer serializer, ModelMap modelMap, PropertyInfo propInfo)
        {
            Serializer = serializer;

            Name = propInfo.Name;
            Path = $"{modelMap.Path}.{propInfo.Name}";

            _supportsWrite = propInfo.GetMethod != null;
            _supportsRead = propInfo.SetMethod != null;

            var attr = propInfo.GetCustomAttribute<ModelPropertyAttribute>();
            Key = new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(attr.Key));
            ExcludeNull = attr.ExcludeNull;
            ExcludeDefault = attr.ExcludeDefault;

            var typeSelector = propInfo.GetCustomAttribute<ModelTypeSelectorAttribute>();
            var dependencies = new List<Memory<byte>>();
            if (typeSelector != null)
            {
                for (int i = 0; i < typeSelector.SelectorProperty.Length; i++)
                {
                    var bytes = MemoryMarshal.AsBytes(typeSelector.SelectorProperty[i].AsSpan());
                    if (Encodings.Utf16.ToUtf8Length(bytes, out int length) != OperationStatus.Done)
                        throw new InvalidOperationException("Failed to convert dependency key to UTF8");
                    var utf8Key = new Memory<byte>(new byte[length]);
                    if (Encodings.Utf16.ToUtf8(bytes, utf8Key.Span, out _, out _) != OperationStatus.Done)
                        throw new InvalidOperationException("Failed to convert dependency key to UTF8");
                    dependencies.Add(utf8Key);
                }
            }
            Dependencies = dependencies;
        }
    }

    public abstract class PropertyMap<TModel> : PropertyMap
    {
        protected PropertyMap(Serializer serializer, ModelMap modelMap, PropertyInfo propInfo)
            : base(serializer, modelMap, propInfo) { }
        
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
            ValueConverter<TValue> converter)
            : base(serializer, modelMap, propInfo)
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
