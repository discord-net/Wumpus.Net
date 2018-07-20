using System;
using System.Buffers.Binary;

namespace Voltaic.Serialization.Etf
{
    public class ObjectEtfConverter<T> : ValueConverter<T>
        where T : class
    {
        private readonly EtfSerializer _serializer;
        private readonly ModelMap<T> _map;

        public ObjectEtfConverter(EtfSerializer serializer)
        {
            _serializer = serializer;
            _map = serializer.GetMap<T>();
        }

        public override bool CanWrite(T value, PropertyMap propMap = null)
            => propMap == null || (!propMap.ExcludeNull && !propMap.ExcludeDefault) || value != null;

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out T result, PropertyMap propMap = null)
        {
            if (EtfReader.TryReadNullSafe(ref remaining))
            {
                result = null;
                return true;
            }

            result = default;

            switch (EtfReader.GetTokenType(ref remaining))
            {
                case EtfTokenType.Map:
                    remaining = remaining.Slice(1);

                    uint arity = BinaryPrimitives.ReadUInt32BigEndian(remaining); // count
                    remaining = remaining.Slice(4);

                    result = _map.CreateUninitialized();
                    if (arity == 0)
                        return true;

                    uint dependencies = 0;
                    var deferred = new DeferredPropertyList<byte, byte>();

                    for (int i = 0; i < arity; i++)
                    {
                        if (!EtfReader.TryReadUtf8String(ref remaining, out var key))
                            return false;

                        // Unknown Property
                        if (!_map.TryGetProperty(key, out var innerPropMap))
                        {
                            _serializer.RaiseUnknownProperty(_map, key);
                            if (!EtfReader.Skip(ref remaining, out _))
                                return false;
                            continue;
                        }

                        if (!innerPropMap.CanRead)
                            return false;

                        // Property depends on another that hasn't been deserialized yet
                        if (!innerPropMap.HasReadConverter(result, dependencies))
                        {
                            if (!EtfReader.Skip(ref remaining, out var skipped))
                                return false;
                            if (!deferred.Add(key, skipped))
                                return false;
                            continue;
                        }

                        var restore = remaining;
                        if (!innerPropMap.TryRead(result, ref remaining, dependencies))
                        {
                            if (innerPropMap.IgnoreErrors)
                            {
                                remaining = restore;
                                if (!EtfReader.Skip(ref remaining, out var skipped))
                                    return false;
                                _serializer.RaiseFailedProperty(_map, innerPropMap);
                                continue;
                            }
                            else
                                return false;
                        }

                        dependencies |= innerPropMap.IndexMask;
                    }

                    // Process all deferred properties
                    for (int i = 0; i < deferred.Count; i++)
                    {
                        if (!_map.TryGetProperty(deferred.GetKey(i), out var innerPropMap))
                            return false;
                        var value = deferred.GetValue(i);

                        if (!innerPropMap.TryRead(result, ref value, dependencies))
                        {
                            if (innerPropMap.IgnoreErrors)
                                _serializer.RaiseFailedProperty(_map, innerPropMap);
                            else
                                return false;
                        }
                    }
                    return true;
                default:
                    return false;
            }
        }

        public override bool TryWrite(ref ResizableMemory<byte> writer, T value, PropertyMap propMap = null)
        {
            if (value == null)
                return EtfWriter.TryWriteNull(ref writer);

            var start = writer.Length;
            writer.Push((byte)EtfTokenType.Map);
            writer.Advance(4);

            uint count = 0;
            var properties = _map.Properties;
            for (int i = 0; i < properties.Count; i++)
            {
                var key = properties[i].Key;
                var innerPropMap = properties[i].Value as PropertyMap<T>;
                if (!innerPropMap.CanWrite(value))
                    continue;

                if (!EtfWriter.TryWriteUtf8Key(ref writer, key.Span))
                    return false;
                if (!innerPropMap.TryWrite(value, ref writer))
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
