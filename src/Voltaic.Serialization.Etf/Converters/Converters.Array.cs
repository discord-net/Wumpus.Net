using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;

namespace Voltaic.Serialization.Etf
{
    public class ArrayEtfConverter<T> : ValueConverter<T[]>
    {
        private readonly EtfSerializer _serializer;
        private readonly ValueConverter<T> _innerConverter;
        private readonly ArrayPool<T> _pool;

        public ArrayEtfConverter(EtfSerializer serializer, ValueConverter<T> innerConverter, ArrayPool<T> pool = null)
        {
            _serializer = serializer;
            _innerConverter = innerConverter;
            _pool = pool;
        }

        public override bool CanWrite(T[] value, PropertyMap propMap = null)
            => propMap == null || (!propMap.ExcludeNull && !propMap.ExcludeDefault) || value != null;

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out T[] result, PropertyMap propMap = null)
        {
            if (EtfReader.TryReadNullSafe(ref remaining))
            {
                result = null;
                return true;
            }

            if (!JsonCollectionReader.TryRead(_serializer, ref remaining, out result, propMap, _innerConverter, _pool, _serializer.Pool))
                return false;

            return true;
        }

        public override bool TryWrite(ref ResizableMemory<byte> writer, T[] value, PropertyMap propMap = null)
        {
            if (value == null)
                return EtfWriter.TryWriteNull(ref writer);

            var start = writer.Length;
            writer.Push((byte)EtfTokenType.List);
            writer.Advance(4);

            uint count = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (!_innerConverter.CanWrite(value[i], propMap))
                    continue;
                if (!_innerConverter.TryWrite(ref writer, value[i], propMap))
                    return false;
                count++;
            }
            
            writer.Array[start + 1] = (byte)(count >> 24);
            writer.Array[start + 2] = (byte)(count >> 16);
            writer.Array[start + 3] = (byte)(count >> 8);
            writer.Array[start + 4] = (byte)count;

            writer.Push((byte)EtfTokenType.Nil); // Tail
            return true;
        }
    }

    public class ListEtfConverter<T> : ValueConverter<List<T>>
    {
        private readonly EtfSerializer _serializer;
        private readonly ValueConverter<T> _innerConverter;
        private readonly ArrayPool<T> _pool;

        public ListEtfConverter(EtfSerializer serializer, ValueConverter<T> innerConverter, ArrayPool<T> pool = null)
        {
            _serializer = serializer;
            _innerConverter = innerConverter;
            _pool = pool;
        }

        public override bool CanWrite(List<T> value, PropertyMap propMap = null)
            => propMap == null || (!propMap.ExcludeNull && !propMap.ExcludeDefault) || value != null;

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out List<T> result, PropertyMap propMap = null)
        {
            if (EtfReader.TryReadNullSafe(ref remaining))
            {
                result = null;
                return true;
            }

            if (!JsonCollectionReader.TryRead(_serializer, ref remaining, out var resultArray, propMap, _innerConverter, _pool, _serializer.Pool))
            {
                result = default;
                return false;
            }
            result = new List<T>(resultArray); // TODO: This is probably inefficient
            return true;
        }

        public override bool TryWrite(ref ResizableMemory<byte> writer, List<T> value, PropertyMap propMap = null)
        {
            if (value == null)
                return EtfWriter.TryWriteNull(ref writer);

            var start = writer.Length;
            writer.Push((byte)EtfTokenType.List);
            writer.Advance(4);

            uint count = 0;
            for (int i = 0; i < value.Count; i++)
            {
                if (!_innerConverter.CanWrite(value[i], propMap))
                    continue;
                if (!_innerConverter.TryWrite(ref writer, value[i], propMap))
                    return false;
                count++;
            }
            
            writer.Array[start + 1] = (byte)(count >> 24);
            writer.Array[start + 2] = (byte)(count >> 16);
            writer.Array[start + 3] = (byte)(count >> 8);
            writer.Array[start + 4] = (byte)count;

            writer.Push((byte)EtfTokenType.Nil); // Tail
            return true;
        }
    }

    internal static class JsonCollectionReader
    {
        private static class EmptyArray<T>
        {
            // Array.Empty<T> wasn't added until .NET Standard 1.3
            public static readonly T[] Value = new T[0];
        }

        public static bool TryRead<T>(EtfSerializer serializer, ref ReadOnlySpan<byte> remaining, out T[] result, 
            PropertyMap propMap, ValueConverter<T> innerConverter, ArrayPool<T> itemPool, ArrayPool<byte> bytePool)
        {
            result = default;
            int resultCount = 0;

            switch (EtfReader.GetTokenType(ref remaining))
            {
                case EtfTokenType.SmallTuple:
                    {
                        if (remaining.Length < 2)
                            return false;

                        // remaining = remaining.Slice(1);
                        byte count = remaining[1];
                        remaining = remaining.Slice(2);

                        result = new T[count];
                        for (int i = 0; i < count; i++, resultCount++)
                        {
                            var restore = remaining;
                            if (!innerConverter.TryRead(ref remaining, out result[resultCount], propMap))
                            {
                                if (propMap?.IgnoreErrors == true)
                                {
                                    remaining = restore;
                                    if (!EtfReader.Skip(ref remaining, out _))
                                        return false;
                                    serializer.RaiseFailedProperty(propMap, i);
                                    resultCount--;
                                }
                                else
                                    return false;
                            }
                        }
                        break;
                    }

                case EtfTokenType.LargeTuple:
                    {
                        if (remaining.Length < 5)
                            return false;

                        remaining = remaining.Slice(1);
                        uint count = BinaryPrimitives.ReadUInt32BigEndian(remaining);
                        remaining = remaining.Slice(4);

                        if (count > int.MaxValue)
                            return false;

                        result = new T[count];
                        for (int i = 0; i < count; i++, resultCount++)
                        {
                            var restore = remaining;
                            if (!innerConverter.TryRead(ref remaining, out result[resultCount], propMap))
                            {
                                if (propMap?.IgnoreErrors == true)
                                {
                                    remaining = restore;
                                    if (!EtfReader.Skip(ref remaining, out _))
                                        return false;
                                    serializer.RaiseFailedProperty(propMap, i);
                                    resultCount--;
                                }
                                else
                                    return false;
                            }
                        }
                        break;
                    }

                case EtfTokenType.List:
                    {
                        if (remaining.Length < 5)
                            return false;

                        remaining = remaining.Slice(1);
                        uint count = BinaryPrimitives.ReadUInt32BigEndian(remaining);
                        remaining = remaining.Slice(4);

                        if (count > int.MaxValue)
                            return false;

                        result = new T[count];
                        for (int i = 0; i < count; i++, resultCount++)
                        {
                            var restore = remaining;
                            if (!innerConverter.TryRead(ref remaining, out result[resultCount], propMap))
                            {
                                if (propMap?.IgnoreErrors == true)
                                {
                                    remaining = restore;
                                    if (!EtfReader.Skip(ref remaining, out _))
                                        return false;
                                    serializer.RaiseFailedProperty(propMap, i);
                                    resultCount--;
                                }
                                else
                                    return false;
                            }
                        }

                        // Tail element
                        if (EtfReader.GetTokenType(ref remaining) != EtfTokenType.Nil)
                            return false;
                        remaining = remaining.Slice(1);
                        break;
                    }

                case EtfTokenType.String:
                    {
                        if (remaining.Length < 3)
                            return false;

                        remaining = remaining.Slice(1);
                        ushort count = BinaryPrimitives.ReadUInt16BigEndian(remaining);
                        remaining = remaining.Slice(2);

                        if (remaining.Length < count)
                            return false;

                        // String optimizes away the per-item header, but our item readers depend on them.
                        // Unfortunately this means we need to inject a header
                        // In the case of reading a byte[] however, we can do a direct copy
                        if (typeof(T) == typeof(byte))
                        {
                            result = new T[count];
                            remaining.Slice(0, count).CopyTo((result as byte[]).AsSpan());
                            remaining = remaining.Slice(count);
                        }
                        else
                        {
                            var tmpArr = bytePool.Rent(2);
                            try
                            {
                                tmpArr[0] = (byte)EtfTokenType.SmallInteger;
                                result = new T[count];
                                for (int i = 0; i < count; i++, resultCount++)
                                {
                                    tmpArr[1] = remaining[i];
                                    var span = new ReadOnlySpan<byte>(tmpArr, 0, 2);
                                    if (!innerConverter.TryRead(ref span, out result[resultCount], propMap))
                                    {
                                        if (propMap?.IgnoreErrors == true)
                                        {
                                            serializer.RaiseFailedProperty(propMap, i);
                                            resultCount--;
                                        }
                                        else
                                            return false;
                                    }
                                }
                                remaining = remaining.Slice(count);
                            }
                            finally { bytePool.Return(tmpArr); }
                        }
                        break;
                    }

                case EtfTokenType.Binary:
                    {
                        if (remaining.Length < 5)
                            return false;

                        remaining = remaining.Slice(1);
                        uint count = BinaryPrimitives.ReadUInt32BigEndian(remaining);
                        if (count > int.MaxValue || remaining.Length < count + 4U)
                            return false;
                        remaining = remaining.Slice(4);

                        if (remaining.Length < count)
                            return false;

                        // String optimizes away the per-item header, but our item readers depend on them.
                        // Unfortunately this means we need to inject a header
                        // In the case of reading a byte[] however, we can do a direct copy
                        if (typeof(T) == typeof(byte))
                        {
                            result = new T[count];
                            remaining.Slice(0, (int)count).CopyTo((result as byte[]).AsSpan());
                            remaining = remaining.Slice((int)count);
                        }
                        else
                        {
                            var tmpArr = bytePool.Rent(2);
                            try
                            {
                                tmpArr[0] = (byte)EtfTokenType.SmallInteger;
                                result = new T[count];
                                for (int i = 0; i < count; i++, resultCount++)
                                {
                                    tmpArr[1] = remaining[i];
                                    var span = new ReadOnlySpan<byte>(tmpArr, 0, 2);
                                    if (!innerConverter.TryRead(ref span, out result[resultCount], propMap))
                                    {
                                        if (propMap?.IgnoreErrors == true)
                                        {
                                            serializer.RaiseFailedProperty(propMap, i);
                                            resultCount--;
                                        }
                                        else
                                            return false;
                                    }
                                }
                                remaining = remaining.Slice((int)count);
                            }
                            finally { bytePool.Return(tmpArr); }
                        }
                        break;
                    }
                default:
                    return false;
            }

            // Resize array if any elements failed
            if (resultCount != result.Length)
                Array.Resize(ref result, resultCount);
            return true;
        }
    }
}
