using System;
using System.Buffers;
using System.Collections.Generic;

namespace Voltaic.Serialization.Etf
{
    public class DictionaryEtfConverter<T> : ValueConverter<Dictionary<string, T>>
    {
        private readonly ValueConverter<T> _innerConverter;
        private readonly ArrayPool<T> _pool;

        public DictionaryEtfConverter(ValueConverter<T> innerConverter, ArrayPool<T> pool = null)
        {
            _innerConverter = innerConverter;
            _pool = pool;
        }

        public override bool CanWrite(Dictionary<string, T> value, PropertyMap propMap)
            => (!propMap.ExcludeNull && !propMap.ExcludeDefault) || value != null;

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out Dictionary<string, T> result, PropertyMap propMap = null)
        {
            throw new NotImplementedException();
        }

        public override bool TryWrite(ref ResizableMemory<byte> writer, Dictionary<string, T> value, PropertyMap propMap = null)
        {
            if (value == null)
                return EtfWriter.TryWriteNull(ref writer);

            throw new NotImplementedException();
        }
    }
}
