using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Voltaic.Serialization
{
    public abstract class PropertyMap
    {
        public Serializer Serializer { get; }
        public ReadOnlyMemory<byte> Key { get; }
        public string Name { get; }
        public bool ExcludeDefault { get; }
        public bool ExcludeNull { get; }
        public int? Index { get; internal set; }
        public uint IndexMask { get; internal set; }
        public bool IgnoreErrors { get; }

        public abstract Type ValueType { get; }

        protected readonly bool _supportsRead, _supportsWrite;

        protected PropertyMap(Serializer serializer, ModelMap modelMap, PropertyInfo propInfo, ModelPropertyAttribute attr)
        {
            Serializer = serializer;
            Name = propInfo.Name;

            _supportsWrite = propInfo.GetMethod != null;
            _supportsRead = propInfo.SetMethod != null;

            Key = new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(attr.Key));
            ExcludeNull = attr.ExcludeNull;
            ExcludeDefault = attr.ExcludeDefault;
            IgnoreErrors = propInfo.GetCustomAttribute<IgnoreErrorsAttribute>() != null || modelMap.ModelType.GetTypeInfo().GetCustomAttribute<IgnoreErrorsAttribute>() != null;
        }
    }

    public abstract class PropertyMap<TModel> : PropertyMap
    {
        protected PropertyMap(Serializer serializer, ModelMap modelMap, PropertyInfo propInfo, ModelPropertyAttribute attr)
            : base(serializer, modelMap, propInfo, attr) { }

        public abstract bool HasReadConverter(TModel model, uint dependencies);

        public bool CanRead => _supportsRead;
        public abstract bool TryRead(TModel model, ref ReadOnlySpan<byte> remaining, uint dependencies);

        public abstract bool CanWrite(TModel model);
        public abstract bool TryWrite(TModel model, ref ResizableMemory<byte> writer);
    }

    public class PropertyMap<TModel, TValue> : PropertyMap<TModel>
    {
        public ValueConverter<TValue> Converter { get; }
        public Func<TModel, TValue> GetFunc { get; }
        public Action<TModel, TValue> SetFunc { get; }

        public override Type ValueType => typeof(TValue);

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

        public override bool HasReadConverter(TModel model, uint dependencies) => true;

        public override bool TryRead(TModel model, ref ReadOnlySpan<byte> data, uint dependencies)
        {
            if (!Converter.TryRead(ref data, out var result, this))
                return false;
            SetFunc(model, result);
            return true;
        }

        public override bool CanWrite(TModel model) => _supportsWrite && Converter.CanWrite(GetFunc(model), this);
        public override bool TryWrite(TModel model, ref ResizableMemory<byte> writer)
        {
            if (!Converter.TryWrite(ref writer, GetFunc(model), this))
                return false;
            return true;
        }
    }

    public class DependentPropertyMap<TModel, TValue> : PropertyMap<TModel>
    {
        public IReadOnlyList<ConverterProvider<TModel, TValue>> ConverterProviders { get; }
        public Func<TModel, TValue> GetFunc { get; }
        public Action<TModel, TValue> SetFunc { get; }

        public override Type ValueType => typeof(TValue);

        private readonly List<PropertyMap> _dependencies;

        public DependentPropertyMap(
            Serializer serializer,
            ModelMap<TModel> modelMap,
            PropertyInfo propInfo,
            ModelPropertyAttribute attr,
            List<ModelTypeSelectorAttribute> typeSelectorAttrs,
            Dictionary<string, PropertyMap<TModel>> props)
            : base(serializer, modelMap, propInfo, attr)
        {
            GetFunc = propInfo.GetMethod?.CreateDelegate(typeof(Func<TModel, TValue>)) as Func<TModel, TValue>;
            SetFunc = propInfo.SetMethod?.CreateDelegate(typeof(Action<TModel, TValue>)) as Action<TModel, TValue>;

            var dependencyDict = new MemoryDictionary<PropertyMap>();
            var dependencies = new List<PropertyMap>();
            var converterProviders = new List<ConverterProvider<TModel, TValue>>();
            for (int i = 0; i < typeSelectorAttrs.Count; i++)
            {
                var typeSelectorAttr = typeSelectorAttrs[i];
                if (!props.TryGetValue(typeSelectorAttr.KeyProperty, out var keyProp))
                    throw new InvalidOperationException($"Unable to find dependency \"{typeSelectorAttr.KeyProperty}\"");
                var keyType = keyProp.ValueType;

                // TODO: Does this search subtypes?
                var mapProp = typeof(TModel).GetTypeInfo().GetDeclaredProperty(typeSelectorAttr.MapProperty);
                if (mapProp == null)
                    throw new InvalidOperationException($"Unable to find map \"{typeSelectorAttr.MapProperty}\"");

                var converterProviderType = typeof(ConverterProvider<,,>).MakeGenericType(typeof(TModel), keyType, typeof(TValue)).GetTypeInfo();
                var converterConstructor = converterProviderType.DeclaredConstructors.Single();
                var converterProvider = converterConstructor.Invoke(new object[] { serializer, propInfo, keyProp, mapProp }) as ConverterProvider<TModel, TValue>;
                converterProviders.Add(converterProvider);

                if (keyProp.Index == null)
                    modelMap.RegisterDependency(keyProp);

                if (dependencyDict.TryAdd(converterProvider.KeyProperty.Key, converterProvider.KeyProperty))
                    dependencies.Add(converterProvider.KeyProperty);
            }
            ConverterProviders = converterProviders;
            _dependencies = dependencies;
        }

        public override bool TryRead(TModel model, ref ReadOnlySpan<byte> data, uint dependencies)
        {
            // Unknown keys are ignored during reads
            if (TryGetReadConverter(model, out var converter, dependencies))
            {
                if (!converter.TryRead(ref data, out var result, this))
                    return false;
                SetFunc(model, result);
            }
            return true;
        }

        public override bool CanWrite(TModel model)
        {
            if (!TryGetWriteConverter(model, out var converter))
                return false;
            return _supportsWrite && converter.CanWrite(GetFunc(model), this);
        }
        public override bool TryWrite(TModel model, ref ResizableMemory<byte> buffer)
        {
            if (!TryGetWriteConverter(model, out var converter))
                return false;
            if (!converter.TryWrite(ref buffer, GetFunc(model), this))
                return false;
            return true;
        }

        public override bool HasReadConverter(TModel model, uint dependencies)
            => TryGetReadConverter(model, out _, dependencies);
        private bool TryGetReadConverter(TModel model, out ValueConverter<TValue> converter, uint dependencies)
        {
            converter = default;
            for (int i = 0; i < ConverterProviders.Count; i++)
            {
                var provider = ConverterProviders[i];
                if ((dependencies & provider.KeyProperty.IndexMask) != 0 && provider.TryGet(model, out converter))
                    return true;
            }
            return false;
        }
        private bool TryGetWriteConverter(TModel model, out ValueConverter<TValue> converter)
        {
            converter = default;
            for (int i = 0; i < ConverterProviders.Count; i++)
            {
                if (ConverterProviders[i].TryGet(model, out converter))
                    return true;
            }
            return false;
        }
    }
}
