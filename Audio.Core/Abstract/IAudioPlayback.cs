using NAudio.Wave;

namespace Audio
{
    public interface IAudioPlayback
    {
        WaveFormat WaveFormat { get; set; }
        bool IsPlaying { get; }
        bool UpdatePlaybackDevice(string deviceId);
        void Play();
        void Pause();
        void Stop();
        void AddSample(ReadOnlySpan<byte> sample);
    }
}
