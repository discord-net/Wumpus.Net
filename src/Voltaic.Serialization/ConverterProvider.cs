using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Voltaic.Serialization
{
    public abstract class ConverterProvider<TModel>
    {
        public PropertyMap<TModel> KeyProperty { get; }

        public ConverterProvider(PropertyMap<TModel> keyProperty)
        {
            KeyProperty = keyProperty;
        }
    }

    public abstract class ConverterProvider<TModel, TValue> : ConverterProvider <TModel>
    {
        public abstract bool TryGet(TModel model, out ValueConverter<TValue> converter);

        public ConverterProvider(PropertyMap<TModel> keyProperty)
            : base(keyProperty) { }
    }

    public class ConverterProvider<TModel, TKey, TValue> : ConverterProvider<TModel, TValue>
    {
        public new PropertyMap<TModel, TKey> KeyProperty => base.KeyProperty as PropertyMap<TModel, TKey>;
        public IReadOnlyDictionary<TKey, ValueConverter<TValue>> Converters { get; }

        public ConverterProvider(
            Serializer serializer,
            PropertyInfo propInfo,
            PropertyMap<TModel, TKey> keyProperty,
            PropertyInfo mapProperty)
            : base(keyProperty)
        {
            if (mapProperty.GetMethod == null)
                throw new InvalidOperationException($"\"{mapProperty.Name}\" has no accessor");
            if (!mapProperty.GetMethod.IsStatic)
                throw new InvalidOperationException($"\"{mapProperty.Name}\" is not static");
            if (!(mapProperty.GetValue(null) is IReadOnlyDictionary<TKey, Type> map))
                throw new InvalidOperationException($"Map must return an {typeof(IReadOnlyDictionary<TKey,Type>).Name}");

            Converters = map.ToDictionary(x => x.Key, x => serializer.GetConverter<TValue>(x.Value, propInfo, true));
        }

        public override bool TryGet(TModel model, out ValueConverter<TValue> converter)
        {
            var key = KeyProperty.GetFunc(model);
            if (key == null)
            {
                converter = default;
                return false;
            }
            return Converters.TryGetValue(key, out converter);
        }
    }
}
