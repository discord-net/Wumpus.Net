using System;
using System.Buffers;
using System.Collections.Generic;

namespace Voltaic.Serialization.Json
{
    public class DictionaryJsonConverter<T> : ValueConverter<Dictionary<string, T>>
    {
        private readonly ValueConverter<T> _innerConverter;
        private readonly ArrayPool<T> _pool;

        public DictionaryJsonConverter(ValueConverter<T> innerConverter, ArrayPool<T> pool = null)
        {
            _innerConverter = innerConverter;
            _pool = pool;
        }

        public override bool CanWrite(Dictionary<string, T> value, PropertyMap propMap = null)
            => propMap == null || (!propMap.ExcludeNull && !propMap.ExcludeDefault) || value != null;

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out Dictionary<string, T> result, PropertyMap propMap = null)
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

            if (JsonReader.GetTokenType(ref remaining) == JsonTokenType.EndObject)
            {
                result = new Dictionary<string, T>(0); // EmptyDictionary?
                remaining = remaining.Slice(1);
                return true;
            }
            result = new Dictionary<string, T>(); // TODO: We need a resizable dictionary w/ pooling

            for (int i = 0; ; i++)
            {
                switch (JsonReader.GetTokenType(ref remaining))
                {
                    case JsonTokenType.None:
                        return false;
                    case JsonTokenType.EndObject:
                        remaining = remaining.Slice(1);
                        return true;
                    case JsonTokenType.ListSeparator:
                        if (i == 0)
                            return false;
                        remaining = remaining.Slice(1);
                        break;
                    default:
                        if (i != 0)
                            return false;
                        break;
                }
                if (!JsonReader.TryReadString(ref remaining, out var key))
                    return false;
                if (JsonReader.GetTokenType(ref remaining) != JsonTokenType.KeyValueSeparator)
                    return false;
                remaining = remaining.Slice(1);
                if (!_innerConverter.TryRead(ref remaining, out var value, propMap))
                    return false;
                if (result.ContainsKey(key))
                    return false;
                result[key] = value;
            }
        }

        public override bool TryWrite(ref ResizableMemory<byte> writer, Dictionary<string, T> value, PropertyMap propMap = null)
        {
            if (value == null)
                return JsonWriter.TryWriteNull(ref writer);

            writer.Push((byte)'{');
            bool isFirst = true;
            foreach (var pair in value)
            {
                if (!isFirst)
                    writer.Push((byte)',');
                else
                    isFirst = false;
                if (!JsonWriter.TryWrite(ref writer, pair.Key))
                    return false;
                writer.Push((byte)':');
                if (!_innerConverter.TryWrite(ref writer, pair.Value, propMap))
                    return false;
            }
            writer.Push((byte)'}');
            return true;
        }
    }
}
