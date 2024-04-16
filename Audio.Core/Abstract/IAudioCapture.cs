using NAudio.Wave;

namespace Audio
{
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
