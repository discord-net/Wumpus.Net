using System;
using System.Buffers;

namespace Voltaic.Serialization
{
    public struct ResizableMemory<T>
    {
        private readonly ArrayPool<T> _pool;

        public ResizableMemory(int initalCapacity = 16, ArrayPool<T> pool = null)
        {
            _pool = pool ?? ArrayPool<T>.Shared;
            Array = _pool.Rent(initalCapacity);
            Length = 0;
        }

        public T[] Array { get; private set; }
        public int Length { get; private set; }

        public void Add(T item)
        {
            RequestLength(1);
            Length++;
        }

        public Span<T> CreateBuffer(int minimumLength)
        {
            RequestLength(minimumLength);
            return new Span<T>(Array, Length, Array.Length - Length);
        }
        public void Write(Span<T> span)
        {
            Length += span.Length;
        }
        public void Write(Span<T> span, int count)
        {
            Length += count;
        }

        public void Clear()
        {
            Length = 0;
        }

        public ArraySegment<T> AsSegment() => new ArraySegment<T>(Array);
        public Memory<T> AsMemory() => new Memory<T>(Array, 0, Length);
        public Span<T> AsSpan() => new Span<T>(Array, 0, Length);
        
        public T[] ToArray()
        {
            if (Length == Array.Length)
                return Array;
            var result = new T[Length];
            Array.AsSpan().CopyTo(result);
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
                Array = _pool.Rent(newSize);
                oldArray.AsSpan(0, Length).CopyTo(Array);
                _pool.Return(oldArray);
            }
        }
    }
}
