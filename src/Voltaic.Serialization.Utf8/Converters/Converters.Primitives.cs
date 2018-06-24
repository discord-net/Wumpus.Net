using System;
using System.Buffers;

namespace Voltaic.Serialization.Utf8
{
    public class BooleanUtf8Converter : ValueConverter<bool>
    {
        private readonly StandardFormat _format;
        public BooleanUtf8Converter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out bool result, PropertyMap propMap = null)
            => Utf8Reader.TryReadBoolean(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, bool value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value, _format);
    }

    public class DateTimeUtf8Converter : ValueConverter<DateTime>
    {
        private readonly StandardFormat _format;
        public DateTimeUtf8Converter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out DateTime result, PropertyMap propMap = null)
            => Utf8Reader.TryReadDateTime(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, DateTime value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value, _format);
    }

    public class DateTimeOffsetUtf8Converter : ValueConverter<DateTimeOffset>
    {
        private readonly StandardFormat _format;
        public DateTimeOffsetUtf8Converter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out DateTimeOffset result, PropertyMap propMap = null)
            => Utf8Reader.TryReadDateTimeOffset(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, DateTimeOffset value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value, _format);
    }

    public class TimeSpanUtf8Converter : ValueConverter<TimeSpan>
    {
        private readonly StandardFormat _format;
        public TimeSpanUtf8Converter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out TimeSpan result, PropertyMap propMap = null)
            => Utf8Reader.TryReadTimeSpan(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, TimeSpan value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value, _format);
    }

    public class SingleUtf8Converter : ValueConverter<float>
    {
        private readonly StandardFormat _format;
        public SingleUtf8Converter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out float result, PropertyMap propMap = null)
            => Utf8Reader.TryReadSingle(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, float value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value, _format);
    }

    public class DoubleUtf8Converter : ValueConverter<double>
    {
        private readonly StandardFormat _format;
        public DoubleUtf8Converter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out double result, PropertyMap propMap = null)
            => Utf8Reader.TryReadDouble(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, double value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value, _format);
    }

    public class DecimalUtf8Converter : ValueConverter<decimal>
    {
        private readonly StandardFormat _format;
        public DecimalUtf8Converter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out decimal result, PropertyMap propMap = null)
            => Utf8Reader.TryReadDecimal(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, decimal value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value, _format);
    }

    public class GuidUtf8Converter : ValueConverter<Guid>
    {
        private readonly StandardFormat _format;
        public GuidUtf8Converter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out Guid result, PropertyMap propMap = null)
            => Utf8Reader.TryReadGuid(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, Guid value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value, _format);
    }

    public class SByteUtf8Converter : ValueConverter<sbyte>
    {
        private readonly StandardFormat _format;
        public SByteUtf8Converter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out sbyte result, PropertyMap propMap = null)
            => Utf8Reader.TryReadInt8(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, sbyte value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value, _format);
    }

    public class Int16Utf8Converter : ValueConverter<short>
    {
        private readonly StandardFormat _format;
        public Int16Utf8Converter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out short result, PropertyMap propMap = null)
            => Utf8Reader.TryReadInt16(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, short value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value, _format);
    }

    public class Int32Utf8Converter : ValueConverter<int>
    {
        private readonly StandardFormat _format;
        public Int32Utf8Converter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out int result, PropertyMap propMap = null)
            => Utf8Reader.TryReadInt32(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, int value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value, _format);
    }

    public class Int64Utf8Converter : ValueConverter<long>
    {
        private readonly StandardFormat _format;
        public Int64Utf8Converter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out long result, PropertyMap propMap = null)
            => Utf8Reader.TryReadInt64(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, long value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value, _format);
    }

    public class ByteUtf8Converter : ValueConverter<byte>
    {
        private readonly StandardFormat _format;
        public ByteUtf8Converter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out byte result, PropertyMap propMap = null)
            => Utf8Reader.TryReadUInt8(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, byte value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value, _format);
    }

    public class UInt16Utf8Converter : ValueConverter<ushort>
    {
        private readonly StandardFormat _format;
        public UInt16Utf8Converter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out ushort result, PropertyMap propMap = null)
            => Utf8Reader.TryReadUInt16(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, ushort value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value, _format);
    }

    public class UInt32Utf8Converter : ValueConverter<uint>
    {
        private readonly StandardFormat _format;
        public UInt32Utf8Converter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out uint result, PropertyMap propMap = null)
            => Utf8Reader.TryReadUInt32(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, uint value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value, _format);
    }

    public class UInt64Utf8Converter : ValueConverter<ulong>
    {
        private readonly StandardFormat _format;
        public UInt64Utf8Converter(StandardFormat format = default)
        {
            _format = format;
        }
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out ulong result, PropertyMap propMap = null)
            => Utf8Reader.TryReadUInt64(ref remaining, out result, _format.Symbol);
        public override bool TryWrite(ref ResizableMemory<byte> writer, ulong value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value, _format);
    }

    public class CharUtf8Converter : ValueConverter<char>
    {
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out char result, PropertyMap propMap = null)
            => Utf8Reader.TryReadChar(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, char value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }

    public class StringUtf8Converter : ValueConverter<string>
    {
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out string result, PropertyMap propMap = null)
            => Utf8Reader.TryReadString(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, string value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }

    public class Utf8StringUtf8Converter : ValueConverter<Utf8String>
    {
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out Utf8String result, PropertyMap propMap = null)
            => Utf8Reader.TryReadUtf8String(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, Utf8String value, PropertyMap propMap = null)
            => Utf8Writer.TryWrite(ref writer, value);
    }
}
