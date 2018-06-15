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

        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out Dictionary<string, T> result, PropertyMap propMap = null)
        {
            result = default;

            switch (JsonReader.GetTokenType(ref remaining))
            {
                case TokenType.StartObject:
                    break;
                case TokenType.Null:
                    result = null;
                    return true;
                default:
                    return false;
            }
            remaining = remaining.Slice(1);

            if (JsonReader.GetTokenType(ref remaining) == TokenType.EndObject)
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
                    case TokenType.None:
                        return false;
                    case TokenType.EndObject:
                        remaining = remaining.Slice(1);
                        return true;
                    case TokenType.ListSeparator:
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
                if (JsonReader.GetTokenType(ref remaining) != TokenType.KeyValueSeparator)
                    return false;
                remaining = remaining.Slice(1);
                if (!_innerConverter.TryRead(serializer, ref remaining, out var value, propMap))
                    return false;
                result.Add(key, value);
            }
        }

        public override bool TryWrite(Serializer serializer, ref ResizableMemory<byte> writer, Dictionary<string, T> value, PropertyMap propMap = null)
        {
            if (value == null)
                return JsonWriter.TryWriteNull(ref writer);

            writer.Append((byte)'[');
            bool isFirst = true;
            foreach (var pair in value)
            {
                if (isFirst)
                {
                    writer.Append((byte)',');
                    isFirst = false;
                }
                if (!JsonWriter.TryWrite(ref writer, pair.Key))
                    return false;
                writer.Append((byte)':');
                if (!_innerConverter.TryWrite(serializer, ref writer, pair.Value, propMap))
                    return false;
            }
            writer.Append((byte)']');
            return true;
        }
    }
}
