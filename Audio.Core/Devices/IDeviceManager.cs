using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio.Devices
{
    public interface IDeviceManager
    {
        IEnumerable<MMDevice> GetDevices();

        bool TryGetDevice(string deviceId, out MMDevice? outDevice);
    }
}