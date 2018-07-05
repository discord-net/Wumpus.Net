using System;

namespace Voltaic.Serialization
{
    internal class ValueConverterAdapter<TBase, TValue> : ValueConverter<TBase>
        where TValue : TBase
    {
        private readonly ValueConverter<TValue> _innerConverter;

        public ValueConverterAdapter(ValueConverter<TValue> innerConverter)
        {
            _innerConverter = innerConverter;
        }

        public override bool CanWrite(TBase baseValue, PropertyMap propMap = null)
        {
            //if (!(baseValue is TValue value))
            //    return true;
            var value = (TValue)baseValue;
            return _innerConverter.CanWrite(value, propMap);
        }

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out TBase result, PropertyMap propMap = null)
        {
            if (!_innerConverter.TryRead(ref remaining, out var innerResult, propMap))
            {
                result = default;
                return false;
            }
            result = innerResult;
            return true;
        }

        public override bool TryWrite(ref ResizableMemory<byte> writer, TBase baseValue, PropertyMap propMap = null)
        {
            //if (!(baseValue is TValue value))
            //    return false;
            var value = (TValue)baseValue;
            return _innerConverter.TryWrite(ref writer, value, propMap);
        }
    }
}
