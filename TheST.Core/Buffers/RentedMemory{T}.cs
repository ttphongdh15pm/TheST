using System.Buffers;
using System.Collections;

namespace TheST.Core.Buffers
{
    public struct RentedMemory<T> : IDisposable, IEnumerable<T>
    {
        private readonly T[] _inlineArray;
        private readonly ArrayPool<T> _pool;
        private readonly int _requestedCapacity;
        private Memory<T> _memory;

        public RentedMemory(ReadOnlySpan<T> spanToCopy, ArrayPool<T>? pool = null)
        {
            _pool = pool ?? ArrayPool<T>.Shared;
            _requestedCapacity = spanToCopy.Length;
            _inlineArray = _pool.Rent(_requestedCapacity);
            spanToCopy.CopyTo(_inlineArray);
        }

        public RentedMemory(IEnumerable<T> values, int capacity, ArrayPool<T>? pool = null)
        {
            _pool = pool ?? ArrayPool<T>.Shared;
            _requestedCapacity = capacity;
            _inlineArray = _pool.Rent(capacity);
            int index = 0;
            foreach (var value in values)
            {
                _inlineArray[index++] = value;
                if (index >= capacity)
                {
                    break;
                }
            }
        }

        public RentedMemory(int capacity, ArrayPool<T>? pool = null)
        {
            _pool = pool ?? ArrayPool<T>.Shared;
            _requestedCapacity = capacity;
            _inlineArray = _pool.Rent(capacity);
        }

        public int Length => _requestedCapacity;

        public Memory<T> Memory
        {
            get
            {
                if (_memory.IsEmpty)
                {
                    _memory = _inlineArray.AsMemory(0, _requestedCapacity);
                }
                return _memory;
            }
        }

        public Span<T> Span => Memory.Span;

        public static implicit operator T[](RentedMemory<T> rentedMemory) => rentedMemory._inlineArray;

        public void Clear()
        {
            Array.Clear(_inlineArray, 0, _requestedCapacity);
        }

        public void Dispose()
        {
            if (_inlineArray != null)
            {
                _pool.Return(_inlineArray);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _requestedCapacity; i++)
            {
                yield return _inlineArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < _requestedCapacity; i++)
            {
                yield return _inlineArray[i];
            }
        }
    }
}
