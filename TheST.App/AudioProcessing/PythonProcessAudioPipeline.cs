﻿using System.Diagnostics;
using TheST.Sockets;

namespace TheST.App.AudioProcessing
{
    internal sealed class PythonProcessAudioPipeline : AudioPipelineBase
    {
        private readonly object _startStopLock = new object();
        private readonly UdpCommunicator _udpListener;
        private readonly UdpMemorySender _udpSender;
        private volatile bool _isRunning;
        public PythonProcessAudioPipeline(IAudioBufferDataHandler handler) : base(handler)
        {
            _udpSender = new UdpMemorySender("127.0.0.3", 6666);
            _udpListener = new UdpCommunicator("127.0.0.3", 7777);
        }

        public override void Put(ReadOnlySpan<byte> input)
        {
            lock (_startStopLock)
            {
                if (_isRunning == false)
                {
                    return;
                }
                _udpSender.Send(input);
            }
        }

        public override void Start()
        {
            lock (_startStopLock)
            {
                if (_isRunning)
                {
                    return;
                }
                _udpListener.MessageReceived -= HandleMessageReceived;
                _udpListener.MessageReceived += HandleMessageReceived;
                _udpListener.StartListening();
                _isRunning = true;
            }
        }

        protected override void InternalDispose()
        {
            if(_udpListener == null)
            {
                return;
            }

            lock (_startStopLock)
            {
                if (_isRunning == false)
                {
                    return;
                }
                _udpListener.MessageReceived -= HandleMessageReceived;
                _udpListener.Dispose();
                _isRunning = false;
            }
        }

        private void HandleMessageReceived(object? sender, ReadOnlyMemory<byte> receivedBuffer)
        {
            HandleOutputData(receivedBuffer.Span);
        }
    }
}