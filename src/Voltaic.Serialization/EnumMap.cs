using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Voltaic.Serialization
{
    public static class EnumMap
    {
        public static EnumMap<T> For<T>() where T : struct => EnumMap<T>.Instance;
    }

    public class EnumMap<T>
        where T : struct
    {
        public static readonly EnumMap<T> Instance = new EnumMap<T>();

        public bool IsStringEnum { get; } = typeof(T).GetTypeInfo().GetCustomAttribute<ModelStringEnumAttribute>() != null;
        public bool IsFlagsEnum { get; } = typeof(T).GetTypeInfo().GetCustomAttribute<FlagsAttribute>() != null;
        public ulong MaxValue { get; }

        private readonly Dictionary<string, T> _keyToValue;
        private readonly MemoryDictionary<T> _utf8KeyToValue;
        private readonly Dictionary<long, T> _intToValue;

        private readonly Dictionary<T, string> _valueToKey;
        private readonly Dictionary<T, Utf8String> _valueToUtf8Key;
        private readonly Dictionary<T, long> _valueToInt;

        public EnumMap()
        {
            var typeInfo = typeof(T).GetTypeInfo();
            if (!typeInfo.IsEnum)
                throw new InvalidOperationException($"{typeInfo.Name} is not an Enum");
            if (IsStringEnum && IsFlagsEnum)
                throw new NotSupportedException("ModelStringEnum cannot be used on a Flags enum");

            _keyToValue = new Dictionary<string, T>();
            _utf8KeyToValue = new MemoryDictionary<T>();
            _intToValue = new Dictionary<long, T>();

            _valueToKey = new Dictionary<T, string>();
            _valueToUtf8Key = new Dictionary<T, Utf8String>();
            _valueToInt = new Dictionary<T, long>();

            foreach (T val in Enum.GetValues(typeof(T)).OfType<T>())
            {
                var fieldInfo = typeInfo.GetDeclaredField(Enum.GetName(typeof(T), val));
                var attr = fieldInfo.GetCustomAttribute<ModelEnumValueAttribute>();
                if (attr != null)
                {
                    string utf16Key = attr.Key;

                    var utf16Bytes = MemoryMarshal.AsBytes(utf16Key.AsSpan());
                    if (Encodings.Utf16.ToUtf8Length(utf16Bytes, out var length) != OperationStatus.Done)
                        throw new ArgumentException("Failed to serialize enum key to UTF8");
                    var utf8Key = new byte[length].AsSpan();
                    if (Encodings.Utf16.ToUtf8(utf16Bytes, utf8Key, out _, out _) != OperationStatus.Done)
                        throw new ArgumentException("Failed to serialize enum key to UTF8");

                    if (attr.Type != EnumValueType.WriteOnly)
                    {
                        _keyToValue.Add(utf16Key, val);
                        _utf8KeyToValue.Add(utf8Key, val);
                    }
                    if (attr.Type != EnumValueType.ReadOnly)
                    {
                        _valueToKey.Add(val, utf16Key);
                        _valueToUtf8Key.Add(val, new Utf8String(utf8Key));
                    }
                }

                var underlyingType = Enum.GetUnderlyingType(typeof(T));
                long baseVal;
                if (underlyingType == typeof(sbyte))
                    baseVal = (sbyte)(ValueType)val;
                else if (underlyingType == typeof(short))
                    baseVal = (short)(ValueType)val;
                else if (underlyingType == typeof(int))
                    baseVal = (int)(ValueType)val;
                else if (underlyingType == typeof(long))
                    baseVal = (long)(ValueType)val;
                else if (underlyingType == typeof(byte))
                    baseVal = (byte)(ValueType)val;
                else if (underlyingType == typeof(ushort))
                    baseVal = (ushort)(ValueType)val;
                else if (underlyingType == typeof(uint))
                    baseVal = (uint)(ValueType)val;
                else if (underlyingType == typeof(ulong))
                    baseVal = (long)(ulong)(ValueType)val;
                else
                    throw new SerializationException($"Unsupported underlying enum type: {underlyingType.Name}");

                _intToValue.Add(baseVal, val);
                _valueToInt.Add(val, baseVal);
                if (baseVal > 0 && (ulong)baseVal > MaxValue)
                    MaxValue = (ulong)baseVal;
            }
        }

        public bool TryFromKey(ReadOnlyMemory<byte> key, out T value)
            => TryFromKey(key.Span, out value);
        public bool TryFromKey(ReadOnlySpan<byte> key, out T value)
        {
            if (!IsFlagsEnum)
                return _utf8KeyToValue.TryGetValue(key, out value);
            else
                throw new NotSupportedException("TryFromKey is not support on a Flags enum");
        }
        public Utf8String ToUtf8Key(T value)
        {
            if (!IsFlagsEnum)
            {
                if (_valueToUtf8Key.TryGetValue(value, out var key))
                    return key;
                throw new SerializationException($"Unknown enum value: {value}");
            }
            else
                throw new NotSupportedException("ToUtf8Key is not support on a Flags enum");
        }
        public string ToUtf16Key(T value)
        {
            if (!IsFlagsEnum)
            {
                if (_valueToKey.TryGetValue(value, out var key))
                return key;
                throw new SerializationException($"Unknown enum value: {value}");
            }
            else
                throw new NotSupportedException("ToUtf16Key is not support on a Flags enum");
        }

        public bool TryFromValue(ulong intValue, out T enumValue)
        {
            if (!IsFlagsEnum)
                return _intToValue.TryGetValue((long)intValue, out enumValue);
            else
            {
                enumValue = (T)(ValueType)intValue;
                return true;
            }
        }
        public bool TryFromValue(long intValue, out T enumValue)
        {
            if (!IsFlagsEnum)
                return _intToValue.TryGetValue(intValue, out enumValue);
            else
            {
                enumValue = (T)(ValueType)intValue;
                return true;
            }
        }

        public ulong ToUInt64(T value)
        {
            if (!IsFlagsEnum)
            {
                if (_valueToInt.TryGetValue(value, out var intValue))
                    return (ulong)intValue;
                throw new SerializationException($"Unknown enum value: {value}");
            }
            else
                return (ulong)(ValueType)value;
        }
        public long ToInt64(T value)
        {
            if (!IsFlagsEnum)
            {
                if (_valueToInt.TryGetValue(value, out var intValue))
                    return intValue;
                throw new SerializationException($"Unknown enum value: {value}");
            }
            else
                return (long)(ValueType)value;
        }
    }
}
