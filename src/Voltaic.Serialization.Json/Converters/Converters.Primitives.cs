using System;

namespace Voltaic.Serialization.Json
{
    public class BooleanJsonConverter : ValueConverter<bool>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out bool result, PropertyMap propMap = null)
            => JsonReader.TryReadBoolean(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, bool value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value);
    }

    public class DateTimeJsonConverter : ValueConverter<DateTime>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out DateTime result, PropertyMap propMap = null)
            => JsonReader.TryReadDateTime(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, DateTime value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value);
    }

    public class DateTimeOffsetJsonConverter : ValueConverter<DateTimeOffset>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out DateTimeOffset result, PropertyMap propMap = null)
            => JsonReader.TryReadDateTimeOffset(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, DateTimeOffset value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value);
    }

    public class TimeSpanJsonConverter : ValueConverter<TimeSpan>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out TimeSpan result, PropertyMap propMap = null)
            => JsonReader.TryReadTimeSpan(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, TimeSpan value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value);
    }

    public class SingleJsonConverter : ValueConverter<float>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out float result, PropertyMap propMap = null)
            => JsonReader.TryReadSingle(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, float value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value);
    }

    public class DoubleJsonConverter : ValueConverter<double>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out double result, PropertyMap propMap = null)
            => JsonReader.TryReadDouble(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, double value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value);
    }

    public class DecimalJsonConverter : ValueConverter<decimal>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out decimal result, PropertyMap propMap = null)
            => JsonReader.TryReadDecimal(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, decimal value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value);
    }

    public class GuidJsonConverter : ValueConverter<Guid>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out Guid result, PropertyMap propMap = null)
            => JsonReader.TryReadGuid(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, Guid value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value);
    }

    public class SByteJsonConverter : ValueConverter<sbyte>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out sbyte result, PropertyMap propMap = null)
            => JsonReader.TryReadInt8(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, sbyte value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value);
    }

    public class Int16JsonConverter : ValueConverter<short>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out short result, PropertyMap propMap = null)
            => JsonReader.TryReadInt16(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, short value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value);
    }

    public class Int32JsonConverter : ValueConverter<int>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out int result, PropertyMap propMap = null)
            => JsonReader.TryReadInt32(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, int value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value);
    }

    public class Int53JsonConverter : ValueConverter<long>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out long result, PropertyMap propMap = null)
            => JsonReader.TryReadInt64(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, long value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, useQuotes: false);
    }

    public class Int64JsonConverter : ValueConverter<long>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out long result, PropertyMap propMap = null)
            => JsonReader.TryReadInt64(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, long value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value);
    }

    public class ByteJsonConverter : ValueConverter<byte>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out byte result, PropertyMap propMap = null)
            => JsonReader.TryReadUInt8(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, byte value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value);
    }

    public class UInt16JsonConverter : ValueConverter<ushort>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out ushort result, PropertyMap propMap = null)
            => JsonReader.TryReadUInt16(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, ushort value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value);
    }

    public class UInt32JsonConverter : ValueConverter<uint>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out uint result, PropertyMap propMap = null)
            => JsonReader.TryReadUInt32(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, uint value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value);
    }

    public class UInt53JsonConverter : ValueConverter<ulong>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out ulong result, PropertyMap propMap = null)
            => JsonReader.TryReadUInt64(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, ulong value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value, useQuotes: false);
    }

    public class UInt64JsonConverter : ValueConverter<ulong>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out ulong result, PropertyMap propMap = null)
            => JsonReader.TryReadUInt64(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, ulong value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value);
    }

    public class CharJsonConverter : ValueConverter<char>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out char result, PropertyMap propMap = null)
            => JsonReader.TryReadChar(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, char value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value);
    }

    public class StringJsonConverter : ValueConverter<string>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out string result, PropertyMap propMap = null)
            => JsonReader.TryReadString(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, string value, PropertyMap propMap = null)
            => JsonWriter.TryWrite(ref writer, value);
    }
}
