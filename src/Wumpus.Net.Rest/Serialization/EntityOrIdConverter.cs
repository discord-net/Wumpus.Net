using System;
using System.Reflection;
using Voltaic.Serialization;
using Voltaic.Serialization.Json;

namespace Wumpus.Serialization
{
    public class EntityOrIdConverter<T> : ValueConverter<EntityOrId<T>>
    {
        private readonly ValueConverter<Snowflake> _idConverter;
        private readonly ValueConverter<T> _entityConverter;

        public EntityOrIdConverter(Serializer serializer, PropertyInfo propInfo)
        {
            _idConverter = serializer.GetConverter<Snowflake>(propInfo, true);
            _entityConverter = serializer.GetConverter<T>(propInfo, true);
        }

        public override bool CanWrite(EntityOrId<T> value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value.Object != default || value.Id != default;

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out EntityOrId<T> result, PropertyMap propMap = null)
        {
            result = default;

            switch (JsonReader.GetTokenType(ref remaining))
            {
                case JsonTokenType.Number:
                    if (!_idConverter.TryRead(ref remaining, out var idValue, propMap))
                        return false;
                    result = new EntityOrId<T>(idValue);
                    return true;
                default:
                    if (!_entityConverter.TryRead(ref remaining, out var entityValue, propMap))
                        return false;
                    result = new EntityOrId<T>(entityValue);
                    return true;
            }
        }

        public override bool TryWrite(ref ResizableMemory<byte> remaining, EntityOrId<T> value, PropertyMap propMap = null)
        {
            if (value.Object == null)
                return _idConverter.TryWrite(ref remaining, value.Id, propMap);
            else
                return _entityConverter.TryWrite(ref remaining, value.Object, propMap);
        }
    }
}
