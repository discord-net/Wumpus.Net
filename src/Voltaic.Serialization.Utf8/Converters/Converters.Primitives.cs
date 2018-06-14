using System;

namespace Voltaic.Serialization.Utf8
{
    public class BooleanUtf8Converter : ValueConverter<bool>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out bool result, PropertyMap propMap = null)
            => Utf8Reader.TryReadBoolean(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, bool value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }

    public class DateTimeUtf8Converter : ValueConverter<DateTime>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out DateTime result, PropertyMap propMap = null)
            => Utf8Reader.TryReadDateTime(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, DateTime value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }

    public class DateTimeOffsetUtf8Converter : ValueConverter<DateTimeOffset>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out DateTimeOffset result, PropertyMap propMap = null)
            => Utf8Reader.TryReadDateTimeOffset(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, DateTimeOffset value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }

    public class TimeSpanUtf8Converter : ValueConverter<TimeSpan>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out TimeSpan result, PropertyMap propMap = null)
            => Utf8Reader.TryReadTimeSpan(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, TimeSpan value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }

    public class SingleUtf8Converter : ValueConverter<float>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out float result, PropertyMap propMap = null)
            => Utf8Reader.TryReadSingle(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, float value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }

    public class DoubleUtf8Converter : ValueConverter<double>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out double result, PropertyMap propMap = null)
            => Utf8Reader.TryReadDouble(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, double value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }

    public class DecimalUtf8Converter : ValueConverter<decimal>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out decimal result, PropertyMap propMap = null)
            => Utf8Reader.TryReadDecimal(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, decimal value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }

    public class GuidUtf8Converter : ValueConverter<Guid>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out Guid result, PropertyMap propMap = null)
            => Utf8Reader.TryReadGuid(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, Guid value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }

    public class SByteUtf8Converter : ValueConverter<sbyte>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out sbyte result, PropertyMap propMap = null)
            => Utf8Reader.TryReadInt8(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, sbyte value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }

    public class Int16Utf8Converter : ValueConverter<short>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out short result, PropertyMap propMap = null)
            => Utf8Reader.TryReadInt16(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, short value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }

    public class Int32Utf8Converter : ValueConverter<int>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out int result, PropertyMap propMap = null)
            => Utf8Reader.TryReadInt32(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, int value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }

    public class Int64Utf8Converter : ValueConverter<long>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out long result, PropertyMap propMap = null)
            => Utf8Reader.TryReadInt64(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, long value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }

    public class ByteUtf8Converter : ValueConverter<byte>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out byte result, PropertyMap propMap = null)
            => Utf8Reader.TryReadUInt8(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, byte value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }

    public class UInt16Utf8Converter : ValueConverter<ushort>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out ushort result, PropertyMap propMap = null)
            => Utf8Reader.TryReadUInt16(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, ushort value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }

    public class UInt32Utf8Converter : ValueConverter<uint>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out uint result, PropertyMap propMap = null)
            => Utf8Reader.TryReadUInt32(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, uint value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }

    public class UInt64Utf8Converter : ValueConverter<ulong>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out ulong result, PropertyMap propMap = null)
            => Utf8Reader.TryReadUInt64(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, ulong value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }

    public class CharUtf8Converter : ValueConverter<char>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out char result, PropertyMap propMap = null)
            => Utf8Reader.TryReadChar(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, char value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }

    public class StringUtf8Converter : ValueConverter<string>
    {
        public override bool TryRead(Serializer serializer, ref ReadOnlySpan<byte> remaining, out string result, PropertyMap propMap = null)
            => Utf8Reader.TryReadString(ref remaining, out result);
        public override bool TryWrite(Serializer serializer, ref MemoryBufferWriter<byte> writer, string value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }
}
