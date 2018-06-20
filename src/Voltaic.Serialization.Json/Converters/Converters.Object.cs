using System;

namespace Voltaic.Serialization.Json
{
    public class ObjectJsonConverter<T> : ValueConverter<T>
        where T : class, new()
    {
        private ref struct SpanList<SpanT>
        {
            private Span<SpanT> s1, s2, s3, s4, s5, s6, s7, s8;

            public int Count { get; private set; }

            public bool Add(Span<SpanT> span)
            {
                switch (Count++)
                {
                    case 0: s1 = span; return true;
                    case 1: s2 = span; return true;
                    case 2: s3 = span; return true;
                    case 3: s4 = span; return true;
                    case 4: s5 = span; return true;
                    case 5: s6 = span; return true;
                    case 6: s7 = span; return true;
                    case 7: s8 = span; return true;
                    default: return false;
                }
            }
            public Span<SpanT> this[int i]
            {
                get
                {
                    switch (i)
                    {
                        case 0: return s1;
                        case 1: return s2;
                        case 2: return s3;
                        case 3: return s4;
                        case 4: return s5;
                        case 5: return s6;
                        case 6: return s7;
                        case 7: return s8;
                        default: return Span<SpanT>.Empty;
                    }
                }
            }
        }

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
            Span<ref int> queued = stackalloc Span<int>[8];
            int queuedCount = 0;

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

                if (!JsonReader.TryReadUtf8String(ref remaining, out var key))
                    return false;
                if (JsonReader.GetTokenType(ref remaining) != JsonTokenType.KeyValueSeparator)
                    return false;
                remaining = remaining.Slice(1);

                if (!map.TryGetProperty(key, out var innerPropMap))
                {
                    JsonReader.Skip(ref remaining);
                    continue;
                }
                if (innerPropMap.DependencyIndex != null && !dependencies[innerPropMap.DependencyIndex.Value])
                {
                    queued[queuedCount++] = 0;
                    JsonReader.Skip(ref remaining);
                    continue;
                }

                if (!innerPropMap.CanRead)
                    return false;
                if (!innerPropMap.TryRead(result, ref remaining))
                    return false;

                if (innerPropMap.SelectorIndex != null)
                    dependencies[innerPropMap.SelectorIndex.Value] = true;
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
