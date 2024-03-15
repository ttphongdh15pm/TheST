using NAudio.Wave;

namespace Audio
{
    public delegate void DataAvailableEventHandler(object? sender, ReadOnlyMemory<byte> data);
    public interface IAudioCapture
    {
        WaveFormat? WaveFormat { get; set; }
        event DataAvailableEventHandler? DataAvailable;
        bool IsCapturing { get; }
        bool UpdateCaptureDevice(string deviceId);
        void StartCapturing();
        void StopCapturing();
    }
}
