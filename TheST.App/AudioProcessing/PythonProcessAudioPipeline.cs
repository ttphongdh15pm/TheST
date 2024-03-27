using System.Diagnostics;
using TheST.Sockets;

namespace TheST.App.AudioProcessing
{
    internal sealed class PythonProcessAudioPipeline : AudioPipelineBase
    {
        private readonly object _startStopLock = new object();

        private readonly UdpCommunicator _udpCommunicator;

        private volatile bool _isRunning;

        private Process? pythonAudioProcess; 
        public PythonProcessAudioPipeline(IAudioBufferDataHandler handler) : base(handler)
        {
            _udpCommunicator = new UdpCommunicator("127.0.0.1", 7777);
        }

        public Process? ExecutePythonScript(string pythonFilePath, string arguments = "")
        {
            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"\"{pythonFilePath}\" {arguments}",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            return Process.Start(start);
        }

        public override void Put(ReadOnlySpan<byte> input)
        {
            lock (_startStopLock)
            {
                if (_isRunning == false)
                {
                    return;
                }
                _udpCommunicator.SendBytes(input, "127.0.0.1", 6666);
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
                _udpCommunicator.MessageReceived -= HandleMessageReceived;
                _udpCommunicator.MessageReceived += HandleMessageReceived;
                _udpCommunicator.StartListening();
                _isRunning = true;
                pythonAudioProcess = ExecutePythonScript("./audio-processing.py");
            }
        }

        public override void Stop()
        {
            lock (_startStopLock)
            {
                if (_isRunning == false)
                {
                    return;
                }
                pythonAudioProcess?.Kill();
                _udpCommunicator.MessageReceived -= HandleMessageReceived;
                _isRunning = false;
            }
        }

        protected override void InternalDispose()
        {
            if (_udpCommunicator == null)
            {
                return;
            }

            lock (_startStopLock)
            {
                if (_isRunning == false)
                {
                    return;
                }
                _udpCommunicator.MessageReceived -= HandleMessageReceived;
                _udpCommunicator.Dispose();
                _isRunning = false;
            }
        }

        private void HandleMessageReceived(object? sender, ReadOnlyMemory<byte> receivedBuffer)
        {
            HandleOutputData(receivedBuffer.Span);
        }
    }
}