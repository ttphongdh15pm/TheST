using Audio;
using TheST.Sockets;

namespace TheST.App.AudioProcessing
{
    internal class AudioGateway : IAudioBufferDataHandler
    {
        private readonly IAudioBufferDataHandler _audioBufferDataHandler;

        private readonly IAudioPipeline _pythonPipeline;

        private UdpCommunicator? _udpCommunicator;

        public AudioGateway(IAudioBufferDataHandler audioBufferDataHandler)
        {
            _pythonPipeline = new PythonProcessAudioPipeline(this);
            _audioBufferDataHandler = audioBufferDataHandler;
        }

        //Receive from python
        public void ReceiveBuffer(ReadOnlySpan<byte> buffer)
        {
            _udpCommunicator?.SendBytes(buffer);
        }

        public void Start(string remoteAddress)
        {
            _udpCommunicator?.Dispose();
            _udpCommunicator = new UdpCommunicator(remoteAddress, 9999);
            _udpCommunicator.MessageReceived += UdpListener_MessageReceived;
            _udpCommunicator.StartListening();
            _pythonPipeline.Start();
        }

        public void Send(ReadOnlySpan<byte> data)
        {
            _pythonPipeline.Put(data);
        }

        public void Stop()
        {
            _pythonPipeline.Stop();
        }

        //Receive from partner
        private void UdpListener_MessageReceived(object? sender, ReadOnlyMemory<byte> e)
        {
            _audioBufferDataHandler?.ReceiveBuffer(e.Span);
        }
    }
}