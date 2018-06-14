using System;
using System.Buffers;

namespace Voltaic.Serialization
{
    public struct MemoryBufferWriter<T>
    {
        private readonly ArrayPool<T> _pool;
        private ResizableArray<T> _buffer;

        public MemoryBufferWriter(int initalCapacity = 256, ArrayPool<T> pool = null)
        {
            _pool = pool ?? ArrayPool<T>.Shared;
            _buffer = new ResizableArray<T>(_pool.Rent(initalCapacity));
        }

        public int Count => _buffer.Count;
        public ArraySegment<T> Formatted => _buffer.Full;

        public Memory<T> GetMemory(int minimumLength = 0)
        {
            if (minimumLength < 1) minimumLength = 1;
            if (minimumLength > _buffer.Free.Count)
            {
                var doubleCount = _buffer.Free.Count * 2;
                int newSize = minimumLength > doubleCount ? minimumLength : doubleCount;
                var newArray = _pool.Rent(newSize + _buffer.Count);
                var oldArray = _buffer.Resize(newArray);
                _pool.Return(oldArray);
            }
            return _buffer.Free;
        }
        public Span<T> GetSpan(int minimumLength)
            => GetMemory(minimumLength).Span;

        public void Write(Span<T> span)
        { 
            _buffer.Count += span.Length;
        }
        public void Write(Span<T> span, int count)
        {
            _buffer.Count += count;
        }
        public void Clear()
        {
            _buffer.Count = 0;
        }
    }
}
