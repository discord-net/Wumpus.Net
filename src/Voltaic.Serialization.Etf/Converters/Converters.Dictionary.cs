using System;
using System.Buffers;
using System.Buffers.Binary;
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

        public override bool CanWrite(Dictionary<string, T> value, PropertyMap propMap = null)
            => propMap == null || (!propMap.ExcludeNull && !propMap.ExcludeDefault) || value != null;

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out Dictionary<string, T> result, PropertyMap propMap = null)
        {
            result = default;
            if (EtfReader.TryReadNullSafe(ref remaining))
            {
                result = null;
                return true;
            }

            remaining = remaining.Slice(1);
            uint length = BinaryPrimitives.ReadUInt32BigEndian(remaining);
            remaining = remaining.Slice(4);

            result = new Dictionary<string, T>(); // TODO: We need a resizable dictionary w/ pooling
            for (int i = 0; i < length; i++)
            {
                if (!EtfReader.TryReadString(ref remaining, out var key))
                    return false;
                if (!_innerConverter.TryRead(ref remaining, out var value, propMap))
                    return false;
                if (result.ContainsKey(key))
                    return false;
                result[key] = value;
            }
            return true;
        }

        public override bool TryWrite(ref ResizableMemory<byte> writer, Dictionary<string, T> value, PropertyMap propMap = null)
        {
            if (value == null)
                return EtfWriter.TryWriteNull(ref writer);

            var start = writer.Length;
            writer.Push((byte)EtfTokenType.Map);
            writer.Advance(4);

            uint count = 0;
            foreach (var pair in value)
            {
                if (!_innerConverter.CanWrite(pair.Value, propMap))
                    continue;

                if (!EtfWriter.TryWriteUtf16Key(ref writer, pair.Key))
                    return false;
                if (!_innerConverter.TryWrite(ref writer, pair.Value, propMap))
                    return false;
                count++;
            }

            writer.Array[start + 1] = (byte)(count >> 24);
            writer.Array[start + 2] = (byte)(count >> 16);
            writer.Array[start + 3] = (byte)(count >> 8);
            writer.Array[start + 4] = (byte)count;
            return true;
        }
    }
}
