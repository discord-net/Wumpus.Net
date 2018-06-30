using System;
using System.Reflection;
using Voltaic.Serialization;
using Voltaic.Serialization.Etf;

namespace Wumpus.Serialization
{
    public class EntityOrIdEtfConverter<T> : ValueConverter<EntityOrId<T>>
    {
        private readonly ValueConverter<Snowflake> _idConverter;
        private readonly ValueConverter<T> _entityConverter;

        public EntityOrIdEtfConverter(Serializer serializer, PropertyInfo propInfo)
        {
            _idConverter = serializer.GetConverter<Snowflake>(propInfo, true);
            _entityConverter = serializer.GetConverter<T>(propInfo, true);
        }

        public override bool CanWrite(EntityOrId<T> value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value.Object != default || value.Id != default;

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out EntityOrId<T> result, PropertyMap propMap = null)
        {
            result = default;

            switch (EtfReader.GetTokenType(ref remaining))
            {
                case EtfTokenType.SmallIntegerExt:
                case EtfTokenType.IntegerExt:
                case EtfTokenType.SmallBigExt:
                case EtfTokenType.LargeBigExt:
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
