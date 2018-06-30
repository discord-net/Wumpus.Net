using System;
using System.Buffers;
using System.Collections.Generic;

namespace Voltaic.Serialization.Etf
{
    public class EtfSerializer : Serializer
    {
        public EtfSerializer(ConverterCollection converters = null, ArrayPool<byte> bytePool = null)
          : base(converters, bytePool)
        {
            // Integers
            _converters.SetDefault<sbyte, SByteEtfConverter>();
            _converters.SetDefault<byte, ByteEtfConverter>();
            _converters.SetDefault<short, Int16EtfConverter>();
            _converters.SetDefault<ushort, UInt16EtfConverter>();
            _converters.SetDefault<int, Int32EtfConverter>();
            _converters.SetDefault<uint, UInt32EtfConverter>();
            _converters.SetDefault<long, Int64EtfConverter>();
            _converters.SetDefault<ulong, UInt64EtfConverter>();

            // Floats
            _converters.SetDefault<float, SingleEtfConverter>();
            _converters.SetDefault<double, DoubleEtfConverter>();
            _converters.SetDefault<decimal, DecimalEtfConverter>();

            // Dates/TimeSpans
            _converters.SetDefault<DateTime, DateTimeEtfConverter>();
            _converters.SetDefault<DateTimeOffset, DateTimeOffsetEtfConverter>();
            _converters.SetDefault<TimeSpan, TimeSpanEtfConverter>();

            // Collections
            _converters.AddGlobalConditional(typeof(ArrayEtfConverter<>),
                (t, p) => t.IsArray,
                (t) => t.GetElementType());
            _converters.SetGenericDefault(typeof(List<>), typeof(ListEtfConverter<>),
                (t) => t.GenericTypeArguments[0]);
            _converters.AddGenericConditional(typeof(Dictionary<,>), typeof(DictionaryEtfConverter<>),
                (t, p) => t.GenericTypeArguments[0] == typeof(string),
                (t) => t.GenericTypeArguments[1]);

            // Strings
            _converters.SetDefault<char, CharEtfConverter>();
            _converters.SetDefault<string, StringEtfConverter>();
            _converters.SetDefault<Utf8String, Utf8StringEtfConverter>();

            // Enums
            _converters.AddGlobalConditional(typeof(Int64EnumEtfConverter<>),
                (t, p) => t.IsEnum && (
                    Enum.GetUnderlyingType(t.AsType()) == typeof(sbyte) ||
                    Enum.GetUnderlyingType(t.AsType()) == typeof(short) ||
                    Enum.GetUnderlyingType(t.AsType()) == typeof(int) ||
                    Enum.GetUnderlyingType(t.AsType()) == typeof(long)),
                (t) => t.AsType());
            _converters.AddGlobalConditional(typeof(UInt64EnumEtfConverter<>),
                (t, p) => t.IsEnum && (
                    Enum.GetUnderlyingType(t.AsType()) == typeof(byte) ||
                    Enum.GetUnderlyingType(t.AsType()) == typeof(ushort) ||
                    Enum.GetUnderlyingType(t.AsType()) == typeof(uint) ||
                    Enum.GetUnderlyingType(t.AsType()) == typeof(ulong)),
                (t) => t.AsType());

            // Others
            _converters.SetDefault<bool, BooleanEtfConverter>();
            _converters.SetDefault<Guid, GuidEtfConverter>();
            _converters.SetGenericDefault(typeof(Nullable<>), typeof(NullableEtfConverter<>),
                (t) => t.GenericTypeArguments[0]);
            _converters.SetGenericDefault(typeof(Optional<>), typeof(OptionalEtfConverter<>),
                (t) => t.GenericTypeArguments[0]);
            _converters.AddGlobalConditional(typeof(ObjectEtfConverter<>),
                (t, p) => t.IsClass,
                (t) => t.AsType());
        }

        public override T Read<T>(ReadOnlySpan<byte> data, ValueConverter<T> converter = null)
        {
            // Strip header
            if (data.Length > 0 && data[0] == 131)
                data = data.Slice(1);

            return base.Read(data, converter);
        }
    }
}
