using System;

namespace Voltaic.Serialization.Json
{
    public class ObjectJsonConverter<T> : ValueConverter<T>
        where T : class
    {
        private readonly JsonSerializer _serializer;
        private readonly ModelMap<T> _map;

        public ObjectJsonConverter(JsonSerializer serializer)
        {
            _serializer = serializer;
            _map = serializer.GetMap<T>();
        }

        public override bool CanWrite(T value, PropertyMap propMap = null)
            => propMap == null || (!propMap.ExcludeNull && !propMap.ExcludeDefault) || value != null;

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out T result, PropertyMap propMap = null)
        {
            result = default;

            switch (JsonReader.GetTokenType(ref remaining))
            {
                case JsonTokenType.StartObject:
                    break;
                case JsonTokenType.Null:
                    remaining = remaining.Slice(4);
                    result = null;
                    return true;
                default:
                    return false;
            }
            remaining = remaining.Slice(1);

            result = _map.CreateUninitialized();;
            if (JsonReader.GetTokenType(ref remaining) == JsonTokenType.EndObject)
            {
                remaining = remaining.Slice(1);
                return true;
            }

            uint dependencies = 0;
            var deferred = new DeferredPropertyList<byte, byte>();

            bool isFirst = true;
            bool incomplete = true;
            while (incomplete)
            {
                switch (JsonReader.GetTokenType(ref remaining))
                {
                    case JsonTokenType.None:
                        return false;
                    case JsonTokenType.EndObject:
                        remaining = remaining.Slice(1);
                        incomplete = false;
                        continue;
                    case JsonTokenType.ListSeparator:
                        if (isFirst)
                            return false;
                        remaining = remaining.Slice(1);
                        break;
                    default:
                        if (!isFirst)
                            return false;
                        isFirst = false;
                        break;
                }

                if (!JsonReader.TryReadUtf8String(ref remaining, out var key))
                    return false;
                if (JsonReader.GetTokenType(ref remaining) != JsonTokenType.KeyValueSeparator)
                    return false;
                remaining = remaining.Slice(1);

                // Unknown Property
                if (!_map.TryGetProperty(key, out var innerPropMap))
                {
                    _serializer.RaiseUnknownProperty(_map, key);
                    if (!JsonReader.Skip(ref remaining, out _))
                        return false;
                    continue;
                }

                if (!innerPropMap.CanRead)
                    return false;

                // Property depends on another that hasn't been deserialized yet
                if (!innerPropMap.HasReadConverter(result, dependencies))
                {
                    if (!JsonReader.Skip(ref remaining, out var skipped))
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
                        if (!JsonReader.Skip(ref remaining, out var skipped))
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
        }

        public override bool TryWrite(ref ResizableMemory<byte> writer, T value, PropertyMap propMap = null)
        {
            if (value == null)
                return JsonWriter.TryWriteNull(ref writer);

            writer.Push((byte)'{');
            bool isFirst = true;

            var properties = _map.Properties;
            for (int i = 0; i < properties.Count; i++)
            {
                var key = properties[i].Key;
                var innerPropMap = properties[i].Value as PropertyMap<T>;
                if (!innerPropMap.CanWrite(value))
                    continue;

                if (!isFirst)
                    writer.Push((byte)',');
                else
                    isFirst = false;

                writer.Push((byte)'"');
                if (!JsonWriter.TryWriteUtf8Bytes(ref writer, key.Span))
                    return false;
                writer.Push((byte)'"');

                writer.Push((byte)':');
                if (!innerPropMap.TryWrite(value, ref writer))
                    return false;
            }
            writer.Push((byte)'}');
            return true;
        }
    }
}
