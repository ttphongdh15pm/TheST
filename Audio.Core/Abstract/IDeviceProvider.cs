using NAudio.CoreAudioApi;

namespace Audio
{
    public interface IDeviceProvider
    {
        IEnumerable<MMDevice> GetDevices();
        bool TryGetDevice(string deviceId, out MMDevice? outDevice);
    }
}
