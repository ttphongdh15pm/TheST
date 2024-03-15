using TheST.Models;

namespace TheST.App.EventArguments
{
    public sealed class DeviceChangedArgs : EventArgs
    {
        public Device? Device { get; }
        public DeviceChangedArgs(Device? device)
        {
            Device = device;
        }
    }
}
