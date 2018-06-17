using System;

namespace Voltaic.Serialization.Json
{
    public class NullableJsonConverter<T> : ValueConverter<T?>
        where T : struct
    {
        private readonly ValueConverter<T> _innerConverter;

        public NullableJsonConverter(ValueConverter<T> innerConverter)
        {
            _innerConverter = innerConverter;
        }

        public override bool CanWrite(T? value, PropertyMap propMap)
            => (!propMap.ExcludeNull || value != null) && _innerConverter.CanWrite(value.Value, propMap);

        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out T? result, PropertyMap propMap = null)
        {
            result = null;
            if (JsonReader.GetTokenType(ref remaining) == TokenType.Null)
                return true;
            if (!_innerConverter.TryRead(serializer, ref remaining, out var resultValue, propMap))
                return false;
            result = resultValue;
            return true;
        }

        public override bool TryWrite(Serializer serializer, ref ResizableMemory<byte> writer, T? value, PropertyMap propMap = null)
        {
            if (value == null)
                return JsonWriter.TryWriteNull(ref writer);
            return _innerConverter.TryWrite(serializer, ref writer, value.Value, propMap);
        }
    }
}
