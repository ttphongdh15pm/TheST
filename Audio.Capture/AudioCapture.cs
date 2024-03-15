using Audio.Devices;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using TheST.Buffers;
using TheST.Core.Buffers;

namespace Audio.Capture
{
    public class AudioCapture : IAudioCapture
    {
        private readonly MemoryBufferQueue<byte> _buffer = new MemoryBufferQueue<byte>(1024);
        private string _deviceId = "";
        private WasapiCapture? _wasapiCapture;
        private WaveFormat? _waveFormat;
        public bool IsCapturing => _wasapiCapture != null && _wasapiCapture.CaptureState == CaptureState.Capturing;

        public WaveFormat? WaveFormat
        {
            get => _wasapiCapture?.WaveFormat;
            set
            {
                _waveFormat = value;
                UpdateCaptureDevice(_deviceId);
            }
        }

        public AudioCapture(WaveFormat waveFormat)
        {
            _waveFormat = waveFormat ?? throw new ArgumentException(nameof(waveFormat));
            UpdateCaptureDevice("");
        }

        public event DataAvailableEventHandler? DataAvailable;

        public void StartCapturing()
        {
            if (_wasapiCapture == null || _wasapiCapture.CaptureState != 0)
            {
                return;
            }
            _wasapiCapture?.StartRecording();
        }

        public void StopCapturing()
        {
            _wasapiCapture?.StopRecording();
        }

        public bool UpdateCaptureDevice(string deviceId)
        {
            if (DevicesFactory.CaptureDeviceManager.TryGetDevice(deviceId, out var device))
            {
                _deviceId = deviceId;
                var currentCaptureState = IsCapturing;
                StopCapturing();
                _wasapiCapture = new WasapiCapture(device, false, 100);
                _wasapiCapture.WaveFormat = _waveFormat;
                _wasapiCapture.DataAvailable += _wasapiCapture_DataAvailable;
                if (currentCaptureState)
                {
                    StartCapturing();
                }
                return true;
            }
            return false;
        }

        private void _wasapiCapture_DataAvailable(object? sender, WaveInEventArgs e)
        {
            using (var tempBuffer = new RentedMemory<byte>(e.BytesRecorded))
            {
                Array.Copy(e.Buffer, tempBuffer, e.BytesRecorded);
                _buffer.Enqueue(tempBuffer.Span);

                using (var tempInputBuffer = new RentedMemory<byte>(tempBuffer.Length))
                {
                    if (_buffer.TryDequeue(tempInputBuffer.Span) == false)
                    {
                        return;
                    }
                    DataAvailable?.Invoke(this, tempInputBuffer.Memory);
                }
            }
        }

        private int CalculateSampleInterval(int sampleRate)
        {
            double interval = 1.0 / sampleRate; // Interval in seconds
            double intervalMilliseconds = interval * 1000; // Interval in milliseconds
            return (int)Math.Round(intervalMilliseconds); // Round to nearest whole number
        }
    }
}