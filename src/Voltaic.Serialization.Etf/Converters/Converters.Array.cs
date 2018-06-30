using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;

namespace Voltaic.Serialization.Etf
{
    public class ArrayEtfConverter<T> : ValueConverter<T[]>
    {
        private readonly ValueConverter<T> _innerConverter;
        private readonly ArrayPool<T> _pool;

        public ArrayEtfConverter(ValueConverter<T> innerConverter, ArrayPool<T> pool = null)
        {
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

            if (!JsonCollectionReader.TryRead(ref remaining, out result, propMap, _innerConverter, _pool))
                return false;
            return true;
        }

        public override bool TryWrite(ref ResizableMemory<byte> writer, T[] value, PropertyMap propMap = null)
        {
            if (value == null)
                return EtfWriter.TryWriteNull(ref writer);

            throw new NotImplementedException();
        }
    }

    public class ListEtfConverter<T> : ValueConverter<List<T>>
    {
        private readonly ValueConverter<T> _innerConverter;
        private readonly ArrayPool<T> _pool;

        public ListEtfConverter(ValueConverter<T> innerConverter, ArrayPool<T> pool = null)
        {
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

            if (!JsonCollectionReader.TryRead(ref remaining, out var resultArray, propMap, _innerConverter, _pool))
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

            throw new NotImplementedException();
        }
    }

    internal static class JsonCollectionReader
    {
        private static class EmptyArray<T>
        {
            // Array.Empty<T> wasn't added until .NET Standard 1.3
            public static readonly T[] Value = new T[0];
        }

        public static bool TryRead<T>(ref ReadOnlySpan<byte> remaining, out T[] result,
            PropertyMap propMap, ValueConverter<T> innerConverter, ArrayPool<T> pool)
        {
            result = default;

            switch (EtfReader.GetTokenType(ref remaining))
            {
                case EtfTokenType.SmallTupleExt:
                    {
                        if (remaining.Length < 2)
                            return false;

                        // remaining = remaining.Slice(1);
                        byte count = remaining[1];
                        remaining = remaining.Slice(2);

                        result = new T[count];
                        for (int i = 0; i < count; i++)
                        {
                            if (!innerConverter.TryRead(ref remaining, out var item, propMap))
                                return false;
                            result[i] = item;
                        }
                        return true;
                    }
                case EtfTokenType.LargeTupleExt:
                    {
                        if (remaining.Length < 5)
                            return false;

                        remaining = remaining.Slice(1);
                        uint count = BinaryPrimitives.ReadUInt32BigEndian(remaining);
                        remaining = remaining.Slice(4);

                        if (count > int.MaxValue)
                            return false;

                        result = new T[count];
                        for (int i = 0; i < count; i++)
                        {
                            if (!innerConverter.TryRead(ref remaining, out var item, propMap))
                                return false;
                            result[i] = item;
                        }
                        return true;
                    }

                case EtfTokenType.ListExt:
                    {
                        if (remaining.Length < 5)
                            return false;

                        remaining = remaining.Slice(1);
                        uint count = BinaryPrimitives.ReadUInt32BigEndian(remaining);
                        remaining = remaining.Slice(4);

                        if (count > int.MaxValue)
                            return false;

                        result = new T[count];
                        for (int i = 0; i < count; i++)
                        {
                            if (!innerConverter.TryRead(ref remaining, out result[i], propMap))
                                return false;
                        }

                        // Tail element
                        if (EtfReader.GetTokenType(ref remaining) != EtfTokenType.NilExt)
                            return false;
                        remaining = remaining.Slice(1);

                        return true;
                    }
                default:
                    return false;
            }
        }
    }
}
