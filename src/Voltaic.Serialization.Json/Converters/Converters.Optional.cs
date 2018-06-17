using System;

namespace Voltaic.Serialization.Json
{
    public class OptionalJsonConverter<T> : ValueConverter<Optional<T>>
    {
        private readonly ValueConverter<T> _innerConverter;

        public OptionalJsonConverter(ValueConverter<T> innerConverter)
        {
            _innerConverter = innerConverter;
        }

        public override bool CanWrite(Optional<T> value, PropertyMap propMap)
            => value.IsSpecified && _innerConverter.CanWrite(value.Value, propMap);

        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out Optional<T> result, PropertyMap propMap = null)
        {
            result = default;
            if (JsonReader.GetTokenType(ref remaining) == TokenType.Null)
                return true;
            if (!_innerConverter.TryRead(serializer, ref remaining, out var resultValue, propMap))
                return false;
            result = resultValue;
            return true;
        }

        public override bool TryWrite(Serializer serializer, ref ResizableMemory<byte> writer, Optional<T> value, PropertyMap propMap = null)
            => _innerConverter.TryWrite(serializer, ref writer, value.Value, propMap);
    }
}
