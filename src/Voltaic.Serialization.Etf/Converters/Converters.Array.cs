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

        public override bool CanWrite(T[] value, PropertyMap propMap)
            => (!propMap.ExcludeNull && !propMap.ExcludeDefault) || value != null;

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out T[] result, PropertyMap propMap = null)
        {
            if (EtfReader.TryReadNullSafe(ref remaining))
            {
                result = null;
                return true;
            }

            if (!JsonCollectionReader.TryRead(_serializer, ref remaining, out result, propMap, _innerConverter, _pool))
                return false;

            return true;
        }

        public override bool TryWrite(ref ResizableMemory<byte> writer, T[] value, PropertyMap propMap = null)
        {
            if (value == null)
                return EtfWriter.TryWriteNull(ref writer);

            var start = writer.Length;
            if (value.Length < 256)
            {
                writer.Push((byte)EtfTokenType.SmallTuple);
                writer.Advance(1);
            }
            else
            {
                writer.Push((byte)EtfTokenType.LargeTuple);
                writer.Advance(4);
            }

            uint count = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (!_innerConverter.CanWrite(value[i], propMap))
                    continue;
                if (!_innerConverter.TryWrite(ref writer, value[i], propMap))
                    return false;
                count++;
            }

            if (value.Length < 256)
                writer.Array[start + 1] = (byte)count;
            else
            {
                writer.Array[start + 1] = (byte)(count >> 24);
                writer.Array[start + 2] = (byte)(count >> 16);
                writer.Array[start + 3] = (byte)(count >> 8);
                writer.Array[start + 4] = (byte)count;
            }
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

        public override bool CanWrite(List<T> value, PropertyMap propMap)
            => (!propMap.ExcludeNull && !propMap.ExcludeDefault) || value != null;

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out List<T> result, PropertyMap propMap = null)
        {
            if (EtfReader.TryReadNullSafe(ref remaining))
            {
                result = null;
                return true;
            }

            if (!JsonCollectionReader.TryRead(_serializer, ref remaining, out var resultArray, propMap, _innerConverter, _pool))
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
            if (value.Count < 256)
            {
                writer.Push((byte)EtfTokenType.SmallTuple);
                writer.Advance(1);
            }
            else
            {
                writer.Push((byte)EtfTokenType.LargeTuple);
                writer.Advance(4);
            }

            uint count = 0;
            for (int i = 0; i < value.Count; i++)
            {
                if (!_innerConverter.CanWrite(value[i], propMap))
                    continue;
                if (!_innerConverter.TryWrite(ref writer, value[i], propMap))
                    return false;
                count++;
            }

            if (value.Count < 256)
                writer.Array[start + 1] = (byte)count;
            else
            {
                writer.Array[start + 1] = (byte)(count >> 24);
                writer.Array[start + 2] = (byte)(count >> 16);
                writer.Array[start + 3] = (byte)(count >> 8);
                writer.Array[start + 4] = (byte)count;
            }
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
            PropertyMap propMap, ValueConverter<T> innerConverter, ArrayPool<T> pool)
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
