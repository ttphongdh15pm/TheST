using Audio.Devices;
using TheST.App.EventArguments;
using TheST.Models;

namespace TheST.App.Controls
{
    public partial class DeviceConfiguration : UserControl
    {
        private readonly List<Device> _captureDevices = new List<Device>();
        private readonly List<Device> _playbackDevices = new List<Device>();

        public Device? SelectedCaptureDevice => _cbbCaptureDevice?.SelectedItem as Device;

        public Device? SelectedPlaybackDevice => _cbbPlaybackDevice?.SelectedItem as Device;

        public DeviceConfiguration()
        {
            InitializeComponent();
        }

        public event EventHandler<DeviceChangedArgs>? CaptureDeviceChanged;

        public event EventHandler<DeviceChangedArgs>? PlaybackDeviceChanged;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadDevices();
            PopulateDevices(_cbbCaptureDevice, _captureDevices);
            PopulateDevices(_cbbPlaybackDevice, _playbackDevices);
        }

        private void LoadDevices()
        {
            _playbackDevices.Clear();
            _captureDevices.Clear();
            _playbackDevices.AddRange(DevicesFactory.PlaybackDeviceManager.GetDevices().Select(device => new Device(device.ID, device.FriendlyName)));
            _captureDevices.AddRange(DevicesFactory.CaptureDeviceManager.GetDevices().Select(device => new Device(device.ID, device.FriendlyName)));
        }

        private void OnCaptureDeviceSelectedIndexChanged(object sender, EventArgs e)
        {
            CaptureDeviceChanged?.Invoke(this, new DeviceChangedArgs(SelectedCaptureDevice));
        }

        private void OnPlaybackDeviceSelectedIndexChanged(object sender, EventArgs e)
        {
            PlaybackDeviceChanged?.Invoke(this, new DeviceChangedArgs(SelectedPlaybackDevice));
        }

        private void PopulateDevices(ComboBox comboBox, IEnumerable<Device> devices)
        {
            comboBox.DataSource = devices.ToList();
            comboBox.DisplayMember = nameof(Device.Name);
            comboBox.ValueMember = nameof(Device.Id);
        }
    }
}