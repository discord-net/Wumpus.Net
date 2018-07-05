using System;

namespace Voltaic.Serialization.Etf
{
    public class NullableEtfConverter<T> : ValueConverter<T?>
        where T : struct
    {
        private readonly ValueConverter<T> _innerConverter;

        public NullableEtfConverter(ValueConverter<T> innerConverter)
        {
            _innerConverter = innerConverter;
        }

        public override bool CanWrite(T? value, PropertyMap propMap = null)
            => propMap == null || (!propMap.ExcludeNull || value != null) && (!value.HasValue || _innerConverter.CanWrite(value.Value, propMap));

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out T? result, PropertyMap propMap = null)
        {
            result = null;

            if (EtfReader.TryReadNullSafe(ref remaining))
                return true;
            if (!_innerConverter.TryRead(ref remaining, out var resultValue, propMap))
                return false;
            result = resultValue;
            return true;
        }

        public override bool TryWrite(ref ResizableMemory<byte> writer, T? value, PropertyMap propMap = null)
        {
            if (value == null)
                return EtfWriter.TryWriteNull(ref writer);
            return _innerConverter.TryWrite(ref writer, value.Value, propMap);
        }
    }
}
