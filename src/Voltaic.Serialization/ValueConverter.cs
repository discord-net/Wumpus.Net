using System;

namespace Voltaic.Serialization
{
    public abstract class ValueConverter<TValue>
    {
        public virtual bool CanWrite(TValue value, PropertyMap propMap = null) => true;

        public abstract bool TryRead(
            Serializer serializer,
            ref ReadOnlySpan<byte> remaining,
            out TValue result,
            PropertyMap propMap = null);

        public abstract bool TryWrite(
            Serializer serializer,
            ref ResizableMemory<byte> remaining,
            TValue value,
            PropertyMap propMap = null);
    }
}
