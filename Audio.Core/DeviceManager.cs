using NAudio.CoreAudioApi;

namespace Audio
{
    internal static class DeviceManager
    {
        private static readonly MMDeviceEnumerator enumerator = new MMDeviceEnumerator();

        public static MMDevice? GetDevice(string deviceId)
        {
            try
            {
                return enumerator.GetDevice(deviceId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IEnumerable<MMDevice> GetDevices(DataFlow dataFlow)
        {
            var devices = enumerator.EnumerateAudioEndPoints(dataFlow, DeviceState.Active).ToList();
            foreach (var device in devices)
            {
                if (device is MMDevice mmDevice)
                {
                    yield return mmDevice;
                }
            }
        }
    }
}