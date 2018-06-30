using System;
using System.Buffers;
using System.Collections.Generic;
using System.Reflection;

namespace Voltaic.Serialization.Etf
{
    public class EtfSerializer : Serializer
    {
        public EtfSerializer(ConverterCollection converters = null, ArrayPool<byte> bytePool = null)
          : base(converters, bytePool)
        {
            // Integers
            _converters.SetDefault<sbyte, SByteEtfConverter>(
                (s, t, p) => new SByteEtfConverter(GetStandardFormat(p)));
            _converters.SetDefault<byte, ByteEtfConverter>(
                (s, t, p) => new ByteEtfConverter(GetStandardFormat(p)));
            _converters.SetDefault<short, Int16EtfConverter>(
                (s, t, p) => new Int16EtfConverter(GetStandardFormat(p)));
            _converters.SetDefault<ushort, UInt16EtfConverter>(
                (s, t, p) => new UInt16EtfConverter(GetStandardFormat(p)));
            _converters.SetDefault<int, Int32EtfConverter>(
                (s, t, p) => new Int32EtfConverter(GetStandardFormat(p)));
            _converters.SetDefault<uint, UInt32EtfConverter>(
                (s, t, p) => new UInt32EtfConverter(GetStandardFormat(p)));
            _converters.SetDefault<long, Int64EtfConverter>(
                (s, t, p) => new Int64EtfConverter(GetStandardFormat(p)));
            _converters.SetDefault<ulong, UInt64EtfConverter>(
                (s, t, p) => new UInt64EtfConverter(GetStandardFormat(p)));

            // Floats
            _converters.SetDefault<float, SingleEtfConverter>(
                (s, t, p) => new SingleEtfConverter(GetStandardFormat(p)));
            _converters.SetDefault<double, DoubleEtfConverter>(
                (s, t, p) => new DoubleEtfConverter(GetStandardFormat(p)));
            _converters.SetDefault<decimal, DecimalEtfConverter>(
                (s, t, p) => new DecimalEtfConverter(GetStandardFormat(p)));

            // Dates/TimeSpans
            _converters.SetDefault<DateTime, DateTimeEtfConverter>(
                (s, t, p) => new DateTimeEtfConverter(GetStandardFormat(p)));
            _converters.SetDefault<DateTimeOffset, DateTimeOffsetEtfConverter>(
                (s, t, p) => new DateTimeOffsetEtfConverter(GetStandardFormat(p)));
            _converters.SetDefault<TimeSpan, TimeSpanEtfConverter>(
                (s, t, p) => new TimeSpanEtfConverter(GetStandardFormat(p)));

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
            _converters.SetDefault<bool, BooleanEtfConverter>(
                (s, t, p) => new BooleanEtfConverter(GetStandardFormat(p)));
            _converters.SetDefault<Guid, GuidEtfConverter>(
                (s, t, p) => new GuidEtfConverter(GetStandardFormat(p)));
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
            // Strip version
            if (data.Length == 0)
                throw new SerializationException("No version byte found");
            data = data.Slice(1);

            if (data.Length != 0 && EtfReader.GetTokenType(ref data) == EtfTokenType.DistributionHeader)
                throw new SerializationException("Distribution header is unsupported");

            return base.Read(data, converter);
        }

        public override void Write<T>(T value, ref ResizableMemory<byte> writer, ValueConverter<T> converter = null)
        {
            writer.Push(131); // Version
            base.Write(value, ref writer, converter);
        }

        private StandardFormat GetStandardFormat(PropertyInfo propInfo)
        {
            var attr = propInfo?.GetCustomAttribute<StandardFormatAttribute>();
            if (attr == null)
                return default;
            return attr.Format;
        }
    }
}
