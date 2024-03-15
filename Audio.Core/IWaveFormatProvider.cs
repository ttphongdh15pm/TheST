using NAudio.Wave;

namespace Audio
{
    public interface IWaveFormatProvider
    {
        bool IsSupported(string deviceId, WaveFormat waveFormat);
        List<WaveFormat> GetWaveFormats(string deviceId);
        List<WaveFormat> WaveFormats { get; }
    }
}
