using System;
using System.Buffers;

namespace Voltaic.Serialization
{
    public struct ResizableMemory<T>
    {
        public ResizableMemory(int initalCapacity, ArrayPool<T> pool = null)
        {
            Pool = pool ?? ArrayPool<T>.Shared;
            Array = Pool.Rent(initalCapacity);
            Length = 0;
        }

        public ArrayPool<T> Pool { get; private set; }
        public T[] Array { get; private set; }
        public int Length { get; private set; }

        public void Push(T item)
        {
            RequestLength(1);
            Array[Length++] = item;
        }
        public T Pop()
        {
            return Array[--Length];
        }

        public ArraySegment<T> GetSegment(int minimumLength)
        {
            RequestLength(minimumLength);
            return new ArraySegment<T>(Array, Length, Array.Length - Length);
        }
        public Memory<T> GetMemory(int minimumLength)
        {
            RequestLength(minimumLength);
            return new Memory<T>(Array, Length, Array.Length - Length);
        }
        public Span<T> GetSpan(int minimumLength)
        {
            RequestLength(minimumLength);
            return new Span<T>(Array, Length, Array.Length - Length);
        }
        public void Advance(int count)
        {
            Length += count;
        }

        public void Clear()
        {
            Length = 0;
        }

        public ArraySegment<T> AsSegment() => new ArraySegment<T>(Array, 0, Length);
        public Memory<T> AsMemory() => new Memory<T>(Array, 0, Length);
        public ReadOnlyMemory<T> AsReadOnlyMemory() => new ReadOnlyMemory<T>(Array, 0, Length);
        public Span<T> AsSpan() => new Span<T>(Array, 0, Length);
        public ReadOnlySpan<T> AsReadOnlySpan() => new ReadOnlySpan<T>(Array, 0, Length);

        public T[] ToArray()
        {
            if (Length == Array.Length)
                return Array;
            var result = new T[Length];
            Array.AsSpan(0, Length).CopyTo(result);
            return result;
        }

        private void RequestLength(int length)
        {
            length += Length;
            if (length > Array.Length)
            {
                var newSize = Array.Length;
                while (newSize < length)
                    newSize *= 2;
                var oldArray = Array;
                Array = Pool.Rent(newSize);
                oldArray.AsSpan(0, Length).CopyTo(Array);
                Pool.Return(oldArray);
            }
        }

        public void Return()
        {
            if (Array != null)
            {
                Pool.Return(Array);
                Array = null;
            }
        }
    }
}
