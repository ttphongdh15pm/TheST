﻿using NAudio.Wave;
using TheST.Common;

namespace Audio.WaveProvider
{
    public sealed class BufferedQueueWaveProvider : IWaveProvider
    {
        private readonly MemoryBufferQueue<byte> _bufferQueue;

        private readonly WaveFormat _waveFormat;

        public BufferedQueueWaveProvider(WaveFormat waveFormat)
        {
            _waveFormat = waveFormat;
            var defaultCapacity = waveFormat.AverageBytesPerSecond * 5;
            _bufferQueue = new MemoryBufferQueue<byte>(defaultCapacity);
        }

        public int BufferedBytes => _bufferQueue.Length;

        public WaveFormat WaveFormat => _waveFormat;

        public int Read(byte[] buffer, int offset, int count)
        {
            var outBufferSpan = buffer.AsSpan(offset, count);
            outBufferSpan.Clear();
            if (_bufferQueue.TryDequeue(outBufferSpan))
            {
                return count;
            }
            return count;
        }

        public void ClearBuffer()
        {
            _bufferQueue.Clear();
        }

        public void AddSamples(ReadOnlySpan<byte> buffer)
        {
            _bufferQueue.Enqueue(buffer);
        }
    }
}
