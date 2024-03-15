using NAudio.CoreAudioApi;

namespace Audio.Devices
{
    internal sealed class RenderDeviceManager : DefaultDeviceManager
    {
        public RenderDeviceManager() : base(DataFlow.Render)
        {
        }
    }
}