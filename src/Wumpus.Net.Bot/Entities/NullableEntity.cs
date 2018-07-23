using Voltaic;

namespace Wumpus.Entities
{
    public struct NullableEntity<T>
        where T : class
    {
        /// <summary> Returns true if this entity's value has been populated. </summary>
        public bool HasValue { get; }
        /// <summary> Returns the id of this entity, regardless if the value is specified. </summary>
        public Snowflake Id { get; }
        /// <summary> Gets the value for this parameter. </summary>
        public T Value { get; }

        /// <summary> Creates a new Parameter with the provided id. </summary>
        public NullableEntity(Snowflake id)
        {
            Id = id;
            Value = null;
            HasValue = false;
        }
        /// <summary> Creates a new Parameter with the provided id and value. </summary>
        public NullableEntity(Snowflake id, T value)
        {
            Id = id;
            Value = value;
            HasValue = value != null;
        }

        public T GetValueOrDefault() => Value;
        public T GetValueOrDefault(T defaultValue) => HasValue ? Value : defaultValue;

        public override bool Equals(object other)
        {
            if (other is NullableEntity<T> nullableOther)
            {
                if (!nullableOther.HasValue) return !HasValue;
                return Value?.Equals(nullableOther.Value) ?? false;
            }

            if (!HasValue) return false;
            return Value?.Equals(other) ?? false;
        }
        public override int GetHashCode() => HasValue ? Value.GetHashCode() : 0;

        public override string ToString() => HasValue ? Value?.ToString() : null;
        private string DebuggerDisplay => Value?.ToString() ?? $"<{Id}>";

        public static explicit operator T(NullableEntity<T> value) => value.Value;

        public static bool operator ==(NullableEntity<T> a, NullableEntity<T> b)
        {
            if (!b.HasValue) return !a.HasValue;
            return a.Value?.Equals(b.Value) ?? false;
        }
        public static bool operator !=(NullableEntity<T> a, NullableEntity<T> b) => !(a == b);

        public static bool operator ==(NullableEntity<T> a, T b)
        {
            if (!a.HasValue) return false;
            return a.Value?.Equals(b) ?? false;
        }
        public static bool operator !=(NullableEntity<T> a, T b) => !(a == b);

        public Optional<T> ToOptional()
            => HasValue ? new Optional<T>(Value) : Optional<T>.Unspecified;
    }

    public static class NullableEntity
    {
        public static NullableEntity<T> Create<T>(Snowflake id)
            where T : class
            => new NullableEntity<T>(id);
        public static NullableEntity<T> Create<T>(Snowflake id, T value)
            where T : class
            => new NullableEntity<T>(id, value);
    }
}
