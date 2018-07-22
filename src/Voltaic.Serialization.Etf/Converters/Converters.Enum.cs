using System;
using System.Reflection;

namespace Voltaic.Serialization.Etf
{
    public class Int64EnumEtfConverter<T> : ValueConverter<T>
        where T : struct // enum
    {
        private readonly ValueConverter<Utf8String> _keyConverter;
        private readonly ValueConverter<long> _valueConverter;
        private readonly EnumMap<T> _map;

        public Int64EnumEtfConverter(Serializer serializer, PropertyInfo propInfo)
        {
            _map = EnumMap.For<T>();
            _keyConverter = serializer.GetConverter<Utf8String>(propInfo, true);
            _valueConverter = serializer.GetConverter<long>(propInfo, true);
        }

        public override bool CanWrite(T value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || _map.ToInt64(value) != default;

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out T result, PropertyMap propMap = null)
        {
            result = default;

            if (_map.IsStringEnum)
            {
                if (!_keyConverter.TryRead(ref remaining, out var keyValue, propMap))
                    return false;
                return _map.TryFromKey(keyValue, out result);
            }
            else
            {
                if (!_valueConverter.TryRead(ref remaining, out var longValue, propMap))
                    return false;
                return _map.TryFromValue(longValue, out result);
            }
        }

        public override bool TryWrite(ref ResizableMemory<byte> writer, T value, PropertyMap propMap = null)
        {
            if (_map.IsStringEnum)
                return _keyConverter.TryWrite(ref writer, _map.ToUtf8Key(value), propMap);
            else
                return _valueConverter.TryWrite(ref writer, _map.ToInt64(value), propMap);
        }
    }

    public class UInt64EnumEtfConverter<T> : ValueConverter<T>
        where T : struct // enum
    {
        private readonly ValueConverter<Utf8String> _keyConverter;
        private readonly ValueConverter<ulong> _valueConverter;
        private readonly EnumMap<T> _map;

        public UInt64EnumEtfConverter(Serializer serializer, PropertyInfo propInfo)
        {
            _map = EnumMap.For<T>();
            _keyConverter = serializer.GetConverter<Utf8String>(propInfo, true);
            _valueConverter = serializer.GetConverter<ulong>(propInfo, true);
        }

        public override bool CanWrite(T value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || _map.ToInt64(value) != default;

        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out T result, PropertyMap propMap = null)
        {
            result = default;

            if (_map.IsStringEnum)
            {
                if (!_keyConverter.TryRead(ref remaining, out var keyValue, propMap))
                    return false;
                return _map.TryFromKey(keyValue, out result);
            }
            else
            {
                if (!_valueConverter.TryRead(ref remaining, out var longValue, propMap))
                    return false;
                return _map.TryFromValue(longValue, out result);
            }
        }

        public override bool TryWrite(ref ResizableMemory<byte> writer, T value, PropertyMap propMap = null)
        {
            if (_map.IsStringEnum)
                return _keyConverter.TryWrite(ref writer, _map.ToUtf8Key(value), propMap);
            else
                return _valueConverter.TryWrite(ref writer, _map.ToUInt64(value), propMap);
        }
    }
}
