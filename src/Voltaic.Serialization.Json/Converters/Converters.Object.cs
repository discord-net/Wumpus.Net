using System;

namespace Voltaic.Serialization.Json
{
    public class ObjectJsonConverter<T> : ValueConverter<T>
        where T : class, new()
    {
        public override bool CanWrite(T value, PropertyMap propMap)
            => (!propMap.ExcludeNull && !propMap.ExcludeDefault) || value != null;

        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out T result, PropertyMap propMap = null)
        {
            result = default;

            switch (JsonReader.GetTokenType(ref remaining))
            {
                case JsonTokenType.StartObject:
                    break;
                case JsonTokenType.Null:
                    result = null;
                    return true;
                default:
                    return false;
            }
            remaining = remaining.Slice(1);

            result = new T();
            if (JsonReader.GetTokenType(ref remaining) == JsonTokenType.EndObject)
            {
                remaining = remaining.Slice(1);
                return true;
            }

            Span<bool> dependencies = stackalloc bool[8];
            var deferred = new DeferredSpanList<byte>();

            var map = serializer.GetMap<T>();
            bool isFirst = true;
            while (true)
            {
                switch (JsonReader.GetTokenType(ref remaining))
                {
                    case JsonTokenType.None:
                        return false;
                    case JsonTokenType.EndObject:
                        remaining = remaining.Slice(1);
                        return true;
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

                var start = remaining;
                if (!JsonReader.TryReadUtf8String(ref remaining, out var key))
                    return false;
                if (JsonReader.GetTokenType(ref remaining) != JsonTokenType.KeyValueSeparator)
                    return false;
                remaining = remaining.Slice(1);

                // Unknown Property
                if (!map.TryGetProperty(key, out var innerPropMap))
                {
                    JsonReader.Skip(ref remaining);
                    continue;
                }

                if (!innerPropMap.CanRead)
                    return false;

                // Property depends on another that hasn't been deserialized yet
                if (!dependencies[innerPropMap.Dependency.Index.Value])
                {
                    if (!deferred.Add(start))
                        return false;
                    JsonReader.Skip(ref remaining);
                    continue;
                }

                if (!innerPropMap.TryRead(result, ref remaining))
                    return false;

                if (innerPropMap.Index != null)
                    dependencies[innerPropMap.Index.Value] = true;
            }
        }

        public override bool TryWrite(Serializer serializer, ref ResizableMemory<byte> writer, T value, PropertyMap propMap = null)
        {
            if (value == null)
                return JsonWriter.TryWriteNull(ref writer);

            writer.Append((byte)'{');
            bool isFirst = true;
            var map = serializer.GetMap(typeof(T));

            var properties = map.Properties;
            for (int i = 0; i < properties.Count; i++)
            {
                var key = properties[i].Key;
                var innerPropMap = properties[i].Value as PropertyMap<T>;
                if (!innerPropMap.CanWrite(value))
                    continue;

                if (!isFirst)
                    writer.Append((byte)',');
                else
                    isFirst = false;

                writer.Append((byte)'"');
                if (!JsonWriter.TryWriteUtf8String(ref writer, key.Span))
                    return false;
                writer.Append((byte)'"');

                writer.Append((byte)':');
                if (!innerPropMap.TryWrite(value, ref writer))
                    return false;
            }
            writer.Append((byte)'}');
            return true;
        }
    }
}
