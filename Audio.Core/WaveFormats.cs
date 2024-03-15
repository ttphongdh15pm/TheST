using Audio.Devices;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace Audio
{
    public static class WaveFormats
    {
        private static Lazy<IWaveFormatProvider> _waveFormatsProvider = new Lazy<IWaveFormatProvider>(() => new DefaultWaveFormatsProvider());
        public static IWaveFormatProvider WaveFormatsProvider => _waveFormatsProvider.Value;

        private class DefaultWaveFormatsProvider : IWaveFormatProvider
        {
            public List<WaveFormat> GetWaveFormats(string deviceId)
            {
                List<WaveFormat> result = new List<WaveFormat>();
                var waveFormats = GetCommonWaveFormats();
                foreach (var waveFormat in waveFormats)
                {
                    if (IsSupported(deviceId, waveFormat))
                    {
                        result.Add(waveFormat);
                    }
                }
                return result;
            }

            public List<WaveFormat> WaveFormats
            {
                get
                {
                    List<WaveFormat> result = new List<WaveFormat>();
                    var waveFormats = GetCommonWaveFormats();
                    foreach (var waveFormat in waveFormats)
                    {
                        result.Add(waveFormat);
                    }
                    return result;
                }
            }

            public bool IsSupported(string deviceId, WaveFormat waveFormat)
            {
                try
                {
                    using (var device = DevicesFactory.GetDevice(deviceId))
                    using (var player = new WasapiOut(device, AudioClientShareMode.Shared, false, 200))
                    {
                        player.Init(new BufferedWaveProvider(waveFormat));
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

            private static IEnumerable<WaveFormat> GetCommonWaveFormats()
            {
                // Expanded set of sample rates
                var sampleRates = new int[] { 8000, 11025, 22050, 44100, 48000, 96000, 192000 };

                // Expanded set of bit depths (only for PCM)
                var bitDepths = new int[] { 8, 16, 24, 32 };

                // Expanded set of channels
                var channels = new int[] { 1, 2 };

                foreach (var sampleRate in sampleRates)
                {
                    foreach (var channel in channels)
                    {
                        // IEEE Float
                        yield return WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channel);
                        // Mu-Law (only supports 8 bit depth and mono)
                        if (channel == 1)
                        {
                            yield return WaveFormat.CreateMuLawFormat(sampleRate, 1);
                        }

                        // PCM
                        foreach (var bitDepth in bitDepths)
                        {
                            WaveFormat? waveFormat = null;
                            try
                            {
                                waveFormat = new WaveFormat(sampleRate, bitDepth, channel);
                            }
                            catch (ArgumentException)
                            {
                                // Ignore invalid combinations
                            }

                            if (waveFormat != null)
                            {
                                yield return waveFormat;
                            }
                        }
                    }
                }
            }
        }
    }
}
