using System;

namespace Voltaic.Serialization
{
    public class DateTimeEpochConverter : ValueConverter<DateTime>
    {
        private readonly EpochType _type;
        private readonly ValueConverter<long> _innerConverter;

        public DateTimeEpochConverter(Serializer serializer, EpochType type)
        {
            _type = type;
            _innerConverter = serializer.GetConverter<long>();
        }
        public override bool CanWrite(DateTime value, PropertyMap propMap = null)
        { 
            switch (_type)
            {
                case EpochType.UnixNanos:
                    return _innerConverter.CanWrite(value.Ticks * 100, propMap);
                case EpochType.UnixMillis:
                    return _innerConverter.CanWrite(value.Ticks / TimeSpan.TicksPerMillisecond, propMap);
                case EpochType.UnixSeconds:
                    return _innerConverter.CanWrite(value.Ticks / TimeSpan.TicksPerSecond, propMap);
            }
            return false;
        }
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out DateTime result, PropertyMap propMap = null)
        {
            result = default;
            if (!_innerConverter.TryRead(ref remaining, out var units, propMap))
                return false;
            switch (_type)
            {
                case EpochType.UnixNanos:
                    result = new DateTime(units / 100, DateTimeKind.Utc);
                    return true;
                case EpochType.UnixMillis:
                    result = new DateTime(units * TimeSpan.TicksPerMillisecond, DateTimeKind.Utc);
                    return true;
                case EpochType.UnixSeconds:
                    result = new DateTime(units * TimeSpan.TicksPerSecond, DateTimeKind.Utc);
                    return true;
            }
            return false;
        }
        public override bool TryWrite(ref ResizableMemory<byte> writer, DateTime value, PropertyMap propMap = null)
        {
            switch (_type)
            {
                case EpochType.UnixNanos:
                    return _innerConverter.TryWrite(ref writer, value.Ticks * 100, propMap);
                case EpochType.UnixMillis:
                    return _innerConverter.TryWrite(ref writer, value.Ticks / TimeSpan.TicksPerMillisecond, propMap);
                case EpochType.UnixSeconds:
                    return _innerConverter.TryWrite(ref writer, value.Ticks / TimeSpan.TicksPerSecond, propMap);
            }
            return false;
        }
    }

    public class DateTimeOffsetEpochConverter : ValueConverter<DateTimeOffset>
    {
        private readonly ValueConverter<long> _innerConverter;
        private readonly EpochType _type;

        public DateTimeOffsetEpochConverter(Serializer serializer, EpochType type)
        {
            _type = type;
            _innerConverter = serializer.GetConverter<long>();
        }
        public override bool CanWrite(DateTimeOffset value, PropertyMap propMap = null)
        {
            switch (_type)
            {
                case EpochType.UnixNanos:
                    return _innerConverter.CanWrite(value.Ticks * 100, propMap);
                case EpochType.UnixMillis:
                    return _innerConverter.CanWrite(value.Ticks / TimeSpan.TicksPerMillisecond, propMap);
                case EpochType.UnixSeconds:
                    return _innerConverter.CanWrite(value.Ticks / TimeSpan.TicksPerSecond, propMap);
            }
            return false;
        }
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out DateTimeOffset result, PropertyMap propMap = null)
        {
            result = default;
            if (!_innerConverter.TryRead(ref remaining, out var units, propMap))
                return false;
            switch (_type)
            {
                case EpochType.UnixNanos:
                    result = new DateTimeOffset(units / 100, TimeSpan.Zero);
                    return true;
                case EpochType.UnixMillis:
                    result = new DateTimeOffset(units * TimeSpan.TicksPerMillisecond, TimeSpan.Zero);
                    return true;
                case EpochType.UnixSeconds:
                    result = new DateTimeOffset(units * TimeSpan.TicksPerSecond, TimeSpan.Zero);
                    return true;
            }
            return false;
        }
        public override bool TryWrite(ref ResizableMemory<byte> writer, DateTimeOffset value, PropertyMap propMap = null)
        {
            switch (_type)
            {
                case EpochType.UnixNanos:
                    return _innerConverter.TryWrite(ref writer, value.Ticks * 100, propMap);
                case EpochType.UnixMillis:
                    return _innerConverter.TryWrite(ref writer, value.Ticks / TimeSpan.TicksPerMillisecond, propMap);
                case EpochType.UnixSeconds:
                    return _innerConverter.TryWrite(ref writer, value.Ticks / TimeSpan.TicksPerSecond, propMap);
            }
            return false;
        }
    }

    public class TimeSpanEpochConverter : ValueConverter<TimeSpan>
    {
        private readonly ValueConverter<long> _innerConverter;
        private readonly EpochType _type;

        public TimeSpanEpochConverter(Serializer serializer, EpochType type)
        {
            _type = type;
            _innerConverter = serializer.GetConverter<long>();
        }
        public override bool CanWrite(TimeSpan value, PropertyMap propMap = null)
        {
            switch (_type)
            {
                case EpochType.UnixNanos:
                    return _innerConverter.CanWrite(value.Ticks * 100, propMap);
                case EpochType.UnixMillis:
                    return _innerConverter.CanWrite(value.Ticks / TimeSpan.TicksPerMillisecond, propMap);
                case EpochType.UnixSeconds:
                    return _innerConverter.CanWrite(value.Ticks / TimeSpan.TicksPerSecond, propMap);
            }
            return false;
        }
        public override bool TryRead(ref ReadOnlySpan<byte> remaining, out TimeSpan result, PropertyMap propMap = null)
        {
            result = default;
            if (!_innerConverter.TryRead(ref remaining, out var units, propMap))
                return false;
            switch (_type)
            {
                case EpochType.UnixNanos:
                    result = new TimeSpan(units / 100);
                    return true;
                case EpochType.UnixMillis:
                    result = new TimeSpan(units * TimeSpan.TicksPerMillisecond);
                    return true;
                case EpochType.UnixSeconds:
                    result = new TimeSpan(units * TimeSpan.TicksPerSecond);
                    return true;
            }
            return false;
        }
        public override bool TryWrite(ref ResizableMemory<byte> writer, TimeSpan value, PropertyMap propMap = null)
        {
            switch (_type)
            {
                case EpochType.UnixNanos:
                    return _innerConverter.TryWrite(ref writer, value.Ticks * 100, propMap);
                case EpochType.UnixMillis:
                    return _innerConverter.TryWrite(ref writer, value.Ticks / TimeSpan.TicksPerMillisecond, propMap);
                case EpochType.UnixSeconds:
                    return _innerConverter.TryWrite(ref writer, value.Ticks / TimeSpan.TicksPerSecond, propMap);
            }
            return false;
        }
    }
}
