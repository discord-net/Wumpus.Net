using System;

namespace Voltaic.Serialization.Etf
{
    public class OptionalEtfConverter<T> : ValueConverter<Optional<T>>
    {
        private readonly ValueConverter<T> _innerConverter;

        public OptionalEtfConverter(ValueConverter<T> innerConverter)
        {
            _innerConverter = innerConverter;
        }

        public override bool CanWrite(Optional<T> value, PropertyMap propMap = null)
            => value.IsSpecified && _innerConverter.CanWrite(value.Value, propMap);

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out Optional<T> result, PropertyMap propMap = null)
        {
            if (!_innerConverter.TryRead(ref remaining, out var resultValue, propMap))
            {
                result = default;
                return false;
            }
            result = resultValue;
            return true;
        }

        public override bool TryWrite(ref ResizableMemory<byte> writer, Optional<T> value, PropertyMap propMap = null)
            => _innerConverter.TryWrite(ref writer, value.Value, propMap);
    }
}
