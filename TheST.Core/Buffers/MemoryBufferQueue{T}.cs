namespace TheST.Buffers
{
    public sealed class MemoryBufferQueue<T>
    {
        private readonly SemaphoreSlim _bufferLock = new SemaphoreSlim(1, 1);
        private volatile int _capacity = 0;
        private volatile int _endPosition = 0;
        private T[] _inlineBuffer;

        public MemoryBufferQueue(int capacity = 320)
        {
            _capacity = capacity;
            _endPosition = 0;
            _inlineBuffer = new T[_capacity];
        }

        public int Length => _endPosition;

        public void Enqueue(ReadOnlySpan<T> input)
        {
            try
            {
                _bufferLock.Wait();
                EnsureCapacity(input);
                input.CopyTo(_inlineBuffer.AsSpan(_endPosition));
                _endPosition += input.Length;
            }
            finally
            {
                _bufferLock.Release();
            }
        }

        public bool TryDequeue(Span<T> output)
        {
            try
            {
                _bufferLock.Wait();
                var inlineSpan = _inlineBuffer.AsSpan();
                var outputLength = output.Length;
                if (!EnoughToFillFull(outputLength))
                {
                    return false;
                }
                inlineSpan.Slice(0, outputLength).CopyTo(output);
                _endPosition -= outputLength;
                inlineSpan.Slice(outputLength, _endPosition).CopyTo(inlineSpan);
                return true;
            }
            finally
            {
                _bufferLock.Release();
            }
        }

        private bool EnoughToFillFull(int expectedLength)
        {
            return Length >= expectedLength;
        }

        private void EnsureCapacity(ReadOnlySpan<T> input)
        {
            while (_capacity - _endPosition <= input.Length)
            {
                _capacity = InscreaseCapacity();
            }
        }

        private int InscreaseCapacity(float ratio = 2f)
        {
            ratio = Math.Clamp(ratio, 1.1f, 10f);
            var newCapacity = (int)(_capacity * ratio);
            Array.Resize(ref _inlineBuffer, newCapacity);
            return newCapacity;
        }

        public void Clear()
        {
            Array.Clear(_inlineBuffer, 0, _inlineBuffer.Length);
            _endPosition = 0;
        }
    }
}
