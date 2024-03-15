using NAudio.CoreAudioApi;

namespace Audio.Devices
{
    internal sealed class CaptureDeviceManager : DefaultDeviceManager
    {
        public CaptureDeviceManager() : base(DataFlow.Capture)
        {
        }
    }
}
