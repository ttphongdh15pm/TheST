using NAudio.CoreAudioApi;

namespace Audio.Devices
{
    public static class DevicesFactory
    {
        private static readonly MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
        private static Lazy<IDeviceManager> _captureDevices = new Lazy<IDeviceManager>(() => new CaptureDeviceManager());
        private static Lazy<IDeviceManager> _renderDevices = new Lazy<IDeviceManager>(() => new RenderDeviceManager());

        public static IDeviceManager CaptureDeviceManager => _captureDevices.Value;

        public static IDeviceManager PlaybackDeviceManager => _renderDevices.Value;

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

        internal static IEnumerable<MMDevice> GetDevices(DataFlow dataFlow)
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