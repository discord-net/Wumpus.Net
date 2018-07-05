using System;

namespace Voltaic.Serialization
{
    public abstract class ValueConverter { }
    public abstract class ValueConverter<TValue> : ValueConverter
    {
        public virtual bool CanWrite(TValue value, PropertyMap propMap = null) => true;
        public abstract bool TryRead(ref ReadOnlySpan<byte> remaining, out TValue result, PropertyMap propMap = null);
        public abstract bool TryWrite(ref ResizableMemory<byte> writer, TValue value, PropertyMap propMap = null);
    }
}
