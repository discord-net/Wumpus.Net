using System;
using System.Buffers;

namespace Voltaic.Serialization.Json
{
    public class BooleanJsonConverter : ValueConverter<bool>
    {
        private readonly StandardFormat _format;
        public BooleanJsonConverter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool CanWrite(bool value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out bool result, PropertyMap propMap = null)
            => JsonReader.TryReadBoolean(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, bool value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, _format);
    }

    public class DateTimeJsonConverter : ValueConverter<DateTime>
    {
        private readonly StandardFormat _format;
        public DateTimeJsonConverter(StandardFormat format = default)
        {
            // Offsets using the default parser has unpredictable behavior with trailing data. Since it's ignored anyway, default to no offsets (G).
            _format = !format.IsDefault ? format : 'G';
        }
        public override bool CanWrite(DateTime value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out DateTime result, PropertyMap propMap = null)
            => JsonReader.TryReadDateTime(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, DateTime value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, _format);
    }

    public class DateTimeOffsetJsonConverter : ValueConverter<DateTimeOffset>
    {
        private readonly StandardFormat _format;
        public DateTimeOffsetJsonConverter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool CanWrite(DateTimeOffset value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out DateTimeOffset result, PropertyMap propMap = null)
            => JsonReader.TryReadDateTimeOffset(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, DateTimeOffset value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, _format);
    }

    public class TimeSpanJsonConverter : ValueConverter<TimeSpan>
    {
        private readonly StandardFormat _format;
        public TimeSpanJsonConverter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool CanWrite(TimeSpan value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out TimeSpan result, PropertyMap propMap = null)
            => JsonReader.TryReadTimeSpan(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, TimeSpan value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, _format);
    }

    public class SingleJsonConverter : ValueConverter<float>
    {
        private readonly StandardFormat _format;
        public SingleJsonConverter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool CanWrite(float value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out float result, PropertyMap propMap = null)
            => JsonReader.TryReadSingle(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, float value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, _format);
    }

    public class DoubleJsonConverter : ValueConverter<double>
    {
        private readonly StandardFormat _format;
        public DoubleJsonConverter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool CanWrite(double value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out double result, PropertyMap propMap = null)
            => JsonReader.TryReadDouble(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, double value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, _format);
    }

    public class DecimalJsonConverter : ValueConverter<decimal>
    {
        private readonly StandardFormat _format;
        public DecimalJsonConverter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool CanWrite(decimal value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out decimal result, PropertyMap propMap = null)
            => JsonReader.TryReadDecimal(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, decimal value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, _format);
    }

    public class GuidJsonConverter : ValueConverter<Guid>
    {
        private readonly StandardFormat _format;
        public GuidJsonConverter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool CanWrite(Guid value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out Guid result, PropertyMap propMap = null)
            => JsonReader.TryReadGuid(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, Guid value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, _format);
    }

    public class SByteJsonConverter : ValueConverter<sbyte>
    {
        private readonly StandardFormat _format;
        public SByteJsonConverter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool CanWrite(sbyte value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out sbyte result, PropertyMap propMap = null)
            => JsonReader.TryReadInt8(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, sbyte value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, _format);
    }

    public class Int16JsonConverter : ValueConverter<short>
    {
        private readonly StandardFormat _format;
        public Int16JsonConverter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool CanWrite(short value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out short result, PropertyMap propMap = null)
            => JsonReader.TryReadInt16(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, short value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, _format);
    }

    public class Int32JsonConverter : ValueConverter<int>
    {
        private readonly StandardFormat _format;
        public Int32JsonConverter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool CanWrite(int value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out int result, PropertyMap propMap = null)
            => JsonReader.TryReadInt32(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, int value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, _format);
    }

    public class Int53JsonConverter : ValueConverter<long>
    {
        private readonly StandardFormat _format;
        public Int53JsonConverter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool CanWrite(long value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out long result, PropertyMap propMap = null)
            => JsonReader.TryReadInt64(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, long value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, _format);
    }

    public class Int64JsonConverter : ValueConverter<long>
    {
        private readonly StandardFormat _format;
        public Int64JsonConverter(StandardFormat format = default)
        {
            _format = !format.IsDefault ? format : JsonSerializer.IntFormat; // Adds quotes
        }
        public override bool CanWrite(long value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out long result, PropertyMap propMap = null)
            => JsonReader.TryReadInt64(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, long value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, _format);
    }

    public class ByteJsonConverter : ValueConverter<byte>
    {
        private readonly StandardFormat _format;
        public ByteJsonConverter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool CanWrite(byte value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out byte result, PropertyMap propMap = null)
            => JsonReader.TryReadUInt8(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, byte value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, _format);
    }

    public class UInt16JsonConverter : ValueConverter<ushort>
    {
        private readonly StandardFormat _format;
        public UInt16JsonConverter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool CanWrite(ushort value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out ushort result, PropertyMap propMap = null)
            => JsonReader.TryReadUInt16(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, ushort value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, _format);
    }

    public class UInt32JsonConverter : ValueConverter<uint>
    {
        private readonly StandardFormat _format;
        public UInt32JsonConverter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool CanWrite(uint value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out uint result, PropertyMap propMap = null)
            => JsonReader.TryReadUInt32(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, uint value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, _format);
    }

    public class UInt53JsonConverter : ValueConverter<ulong>
    {
        private readonly StandardFormat _format;
        public UInt53JsonConverter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool CanWrite(ulong value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out ulong result, PropertyMap propMap = null)
            => JsonReader.TryReadUInt64(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, ulong value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, _format);
    }

    public class UInt64JsonConverter : ValueConverter<ulong>
    {
        private readonly StandardFormat _format;
        public UInt64JsonConverter(StandardFormat format = default)
        {
            _format = !format.IsDefault ? format : JsonSerializer.IntFormat; // Adds quotes
        }
        public override bool CanWrite(ulong value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out ulong result, PropertyMap propMap = null)
            => JsonReader.TryReadUInt64(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, ulong value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, _format);
    }

    public class CharJsonConverter : ValueConverter<char>
    {
        public override bool CanWrite(char value, PropertyMap propMap = null)
            => propMap == null || !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out char result, PropertyMap propMap = null)
            => JsonReader.TryReadChar(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, char value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value);
    }

    public class StringJsonConverter : ValueConverter<string>
    {
        public override bool CanWrite(string value, PropertyMap propMap = null)
            => propMap == null || (!propMap.ExcludeDefault && !propMap.ExcludeNull) || !(value is null);
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out string result, PropertyMap propMap = null)
        {
            switch (JsonReader.GetTokenType(ref remaining))
            {
                case JsonTokenType.Null:
                    remaining = remaining.Slice(4);
                    result = null;
                    return true;
                default:
                    return JsonReader.TryReadString(ref remaining, out result);
            }
        }
        public override bool TryWrite(ref ResizableMemory<byte> writer, string value, PropertyMap propMap = null)
        {
            if (value is null)
                return JsonWriter.TryWriteNull(ref writer);
            else
                return JsonWriter.TryWrite(ref writer, value);
        }
    }

    public class Utf8StringJsonConverter : ValueConverter<Utf8String>
    {
        public override bool CanWrite(Utf8String value, PropertyMap propMap = null)
            => propMap == null || (!propMap.ExcludeDefault && !propMap.ExcludeNull) || !(value is null);
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out Utf8String result, PropertyMap propMap = null)
        {
            switch (JsonReader.GetTokenType(ref remaining))
            {
                case JsonTokenType.Null:
                    remaining = remaining.Slice(4);
                    result = null;
                    return true;
                default:
                    return JsonReader.TryReadUtf8String(ref remaining, out result);
            }
        }
        public override bool TryWrite(ref ResizableMemory<byte> writer, Utf8String value, PropertyMap propMap = null)
        {
            if (value is null)
                return JsonWriter.TryWriteNull(ref writer);
            else
                return JsonWriter.TryWrite(ref writer, value);
        }
    }
}
