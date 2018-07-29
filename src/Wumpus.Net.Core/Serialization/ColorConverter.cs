using System;
using System.Reflection;
using Voltaic;
using Voltaic.Serialization;
using Wumpus.Entities;

namespace Wumpus.Serialization
{
    public class ColorConverter : ValueConverter<Color>
    {
        private readonly ValueConverter<uint> _valueConverter;

        public ColorConverter(Serializer serializer, PropertyInfo propInfo)
        {
            _valueConverter = serializer.GetConverter<uint>(propInfo, true);
        }

        public override bool CanWrite(Color value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value.RawValue != default;

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out Color result, PropertyMap propMap = null)
        {
            if (!_valueConverter.TryRead(ref remaining, out var uintValue, propMap))
            {
                result = default;
                return false;
            }
            result = new Color(uintValue);
            return true;
        }

        public override bool TryWrite(ref ResizableMemory<byte> remaining, Color value, PropertyMap propMap = null)
            => _valueConverter.TryWrite(ref remaining, value.RawValue, propMap);
    }
}
