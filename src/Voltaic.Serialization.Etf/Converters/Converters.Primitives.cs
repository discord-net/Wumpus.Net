using System;
using System.Buffers;

namespace Voltaic.Serialization.Etf
{
    public class BooleanEtfConverter : ValueConverter<bool>
    {
        public override bool CanWrite(bool value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out bool result, PropertyMap propMap = null)
            => EtfReader.TryReadBoolean(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, bool value, PropertyMap propMap = null)
            => EtfWriter.TryWrite(ref writer, value);
    }

    public class DateTimeEtfConverter : ValueConverter<DateTime>
    {
        public override bool CanWrite(DateTime value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out DateTime result, PropertyMap propMap = null)
            => EtfReader.TryReadDateTime(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, DateTime value, PropertyMap propMap = null)
            => EtfWriter.TryWrite(ref writer, value);
    }

    public class DateTimeOffsetEtfConverter : ValueConverter<DateTimeOffset>
    {
        public override bool CanWrite(DateTimeOffset value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out DateTimeOffset result, PropertyMap propMap = null)
            => EtfReader.TryReadDateTimeOffset(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, DateTimeOffset value, PropertyMap propMap = null)
            => EtfWriter.TryWrite(ref writer, value);
    }

    public class TimeSpanEtfConverter : ValueConverter<TimeSpan>
    {
        public override bool CanWrite(TimeSpan value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out TimeSpan result, PropertyMap propMap = null)
            => EtfReader.TryReadTimeSpan(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, TimeSpan value, PropertyMap propMap = null)
            => EtfWriter.TryWrite(ref writer, value);
    }

    public class SingleEtfConverter : ValueConverter<float>
    {
        public override bool CanWrite(float value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out float result, PropertyMap propMap = null)
            => EtfReader.TryReadSingle(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, float value, PropertyMap propMap = null)
            => EtfWriter.TryWrite(ref writer, value);
    }

    public class DoubleEtfConverter : ValueConverter<double>
    {
        public override bool CanWrite(double value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out double result, PropertyMap propMap = null)
            => EtfReader.TryReadDouble(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, double value, PropertyMap propMap = null)
            => EtfWriter.TryWrite(ref writer, value);
    }

    public class DecimalEtfConverter : ValueConverter<decimal>
    {
        public override bool CanWrite(decimal value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out decimal result, PropertyMap propMap = null)
            => EtfReader.TryReadDecimal(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, decimal value, PropertyMap propMap = null)
            => EtfWriter.TryWrite(ref writer, value);
    }

    public class GuidEtfConverter : ValueConverter<Guid>
    {
        public override bool CanWrite(Guid value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out Guid result, PropertyMap propMap = null)
            => EtfReader.TryReadGuid(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, Guid value, PropertyMap propMap = null)
            => EtfWriter.TryWrite(ref writer, value);
    }

    public class SByteEtfConverter : ValueConverter<sbyte>
    {
        public override bool CanWrite(sbyte value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out sbyte result, PropertyMap propMap = null)
            => EtfReader.TryReadInt8(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, sbyte value, PropertyMap propMap = null)
            => EtfWriter.TryWrite(ref writer, value);
    }

    public class Int16EtfConverter : ValueConverter<short>
    {
        public override bool CanWrite(short value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out short result, PropertyMap propMap = null)
            => EtfReader.TryReadInt16(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, short value, PropertyMap propMap = null)
            => EtfWriter.TryWrite(ref writer, value);
    }

    public class Int32EtfConverter : ValueConverter<int>
    {
        public override bool CanWrite(int value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out int result, PropertyMap propMap = null)
            => EtfReader.TryReadInt32(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, int value, PropertyMap propMap = null)
            => EtfWriter.TryWrite(ref writer, value);
    }

    public class Int64EtfConverter : ValueConverter<long>
    {
        public override bool CanWrite(long value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out long result, PropertyMap propMap = null)
            => EtfReader.TryReadInt64(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, long value, PropertyMap propMap = null)
            => EtfWriter.TryWrite(ref writer, value);
    }

    public class ByteEtfConverter : ValueConverter<byte>
    {
        public override bool CanWrite(byte value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out byte result, PropertyMap propMap = null)
            => EtfReader.TryReadUInt8(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, byte value, PropertyMap propMap = null)
            => EtfWriter.TryWrite(ref writer, value);
    }

    public class UInt16EtfConverter : ValueConverter<ushort>
    {
        public override bool CanWrite(ushort value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out ushort result, PropertyMap propMap = null)
            => EtfReader.TryReadUInt16(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, ushort value, PropertyMap propMap = null)
            => EtfWriter.TryWrite(ref writer, value);
    }

    public class UInt32EtfConverter : ValueConverter<uint>
    {
        public override bool CanWrite(uint value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out uint result, PropertyMap propMap = null)
            => EtfReader.TryReadUInt32(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, uint value, PropertyMap propMap = null)
            => EtfWriter.TryWrite(ref writer, value);
    }

    public class UInt64EtfConverter : ValueConverter<ulong>
    {
        public override bool CanWrite(ulong value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out ulong result, PropertyMap propMap = null)
            => EtfReader.TryReadUInt64(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, ulong value, PropertyMap propMap = null)
            => EtfWriter.TryWrite(ref writer, value);
    }

    public class CharEtfConverter : ValueConverter<char>
    {
        public override bool CanWrite(char value, PropertyMap propMap)
            => !propMap.ExcludeDefault || value != default;
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out char result, PropertyMap propMap = null)
            => EtfReader.TryReadChar(ref remaining, out result);
        public override bool TryWrite(ref ResizableMemory<byte> writer, char value, PropertyMap propMap = null)
            => EtfWriter.TryWrite(ref writer, value);
    }

    public class StringEtfConverter : ValueConverter<string>
    {
        public override bool CanWrite(string value, PropertyMap propMap)
            => (!propMap.ExcludeDefault && !propMap.ExcludeNull) || !(value is null);
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out string result, PropertyMap propMap = null)
        {
            if (EtfReader.TryReadNullSafe(ref remaining))
            {
                result = null;
                return true;
            }
            return EtfReader.TryReadString(ref remaining, out result);
        }
        public override bool TryWrite(ref ResizableMemory<byte> writer, string value, PropertyMap propMap = null)
        {
            if (value is null)
                return EtfWriter.TryWriteNull(ref writer);
            else
                return EtfWriter.TryWrite(ref writer, value);
        }
    }

    public class Utf8StringEtfConverter : ValueConverter<Utf8String>
    {
        public override bool CanWrite(Utf8String value, PropertyMap propMap)
            => (!propMap.ExcludeDefault && !propMap.ExcludeNull) || !(value is null);
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out Utf8String result, PropertyMap propMap = null)
        {
            if (EtfReader.TryReadNullSafe(ref remaining))
            {
                result = null;
                return true;
            }
            return EtfReader.TryReadUtf8String(ref remaining, out result);
        }
        public override bool TryWrite(ref ResizableMemory<byte> writer, Utf8String value, PropertyMap propMap = null)
        {
            if (value is null)
                return EtfWriter.TryWriteNull(ref writer);
            else
                return EtfWriter.TryWrite(ref writer, value);
        }
    }
}
