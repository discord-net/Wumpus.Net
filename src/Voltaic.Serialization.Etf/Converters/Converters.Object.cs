﻿using System;
using System.Buffers.Binary;

namespace Voltaic.Serialization.Etf
{
    public class ObjectEtfConverter<T> : ValueConverter<T>
        where T : class, new()
    {
        private readonly ModelMap<T> _map;

        public ObjectEtfConverter(Serializer serializer)
        {
            _map = serializer.GetMap<T>();
        }

        public override bool CanWrite(T value, PropertyMap propMap)
            => (!propMap.ExcludeNull && !propMap.ExcludeDefault) || value != null;

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
                case EtfTokenType.MapExt:
                    remaining = remaining.Slice(1);

                    uint arity = BinaryPrimitives.ReadUInt32BigEndian(remaining); // count
                    remaining = remaining.Slice(4);

                    result = new T();
                    if (arity == 0)
                        return true;

                    uint dependencies = 0;
                    var deferred = new DeferredPropertyList<byte, byte>();

                    for (int i = 0; i < arity; i++)
                    {
                        if (!EtfReader.TryReadKey(ref remaining, out var key))
                            return false;

                        // Unknown Property
                        if (!_map.TryGetProperty(key, out var innerPropMap))
                        {
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

                        if (!innerPropMap.TryRead(result, ref remaining, dependencies))
                            return false;

                        dependencies |= innerPropMap.IndexMask;
                    }

                    // Process all deferred properties
                    for (int i = 0; i < deferred.Count; i++)
                    {
                        if (!_map.TryGetProperty(deferred.GetKey(i), out var innerPropMap))
                            return false;
                        var value = deferred.GetValue(i);
                        if (!innerPropMap.TryRead(result, ref value, dependencies))
                            return false;
                    }
                    return true;
                default:
                    return false;
            }
        }

        public override bool TryWrite(ref ResizableMemory<byte> writer, T value, PropertyMap propMap = null)
        {
            throw new NotImplementedException();
            //if (value == null)
            //    return EtfWriter.TryWriteNull(ref writer);

            //writer.Push(116); // MAP_EXT

            //var properties = _map.Properties;

            //uint count = 0;            
            //for (int i = 0; i < properties.Count; i++)
            //{
            //    var key = properties[i].Key;
            //    var innerPropMap = properties[i].Value as PropertyMap<T>;
            //    if (!innerPropMap.CanWrite(value))
            //        continue;

            //    if (!EtfWriter.TryWriteUtf8String(ref writer, key.Span))
            //        return false;
            //    if (!innerPropMap.TryWrite(value, ref writer))
            //        return false;
            //    count++;
            //}

            //// Go back and write the count
            //BinaryPrimitives.WriteUInt32BigEndian(writer.AsSpan().Slice(1), count);
            //writer.Advance(4);
            //return true;
        }
    }
}