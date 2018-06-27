using System;
using System.Reflection;
using Voltaic.Serialization;

namespace Wumpus.Serialization
{
    public class SnowflakeJsonConverter : ValueConverter<Snowflake>
    {
        private readonly ValueConverter<ulong> _valueConverter;

        public SnowflakeJsonConverter(Serializer serializer, PropertyInfo propInfo)
        {
            _valueConverter = serializer.GetConverter<ulong>(propInfo, true);
        }

        public override bool CanWrite(Snowflake value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value.RawValue != default;

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out Snowflake result, PropertyMap propMap = null)
        {
            if (!_valueConverter.TryRead(ref remaining, out var longValue, propMap))
            {
                result = default;
                return false;
            }
            result = new Snowflake(longValue);
            return true;
        }

        public override bool TryWrite(ref ResizableMemory<byte> remaining, Snowflake value, PropertyMap propMap = null)
            => _valueConverter.TryWrite(ref remaining, value.RawValue, propMap);
    }
}
