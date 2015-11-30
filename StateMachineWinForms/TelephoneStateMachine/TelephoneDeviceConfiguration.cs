using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices;

namespace TelephoneStateMachine
{
    public class TelephoneDeviceConfiguration : IDeviceConfiguration
    {

        private DeviceManager _devManager;

        public TelephoneDeviceConfiguration()
        {
            _devManager = DeviceManager.Instance;
            InitDevices();
        }

        public Dictionary<string, object> Devices { get; set; }

        private void InitDevices()
        {
            var bell = new DeviceBell(_devManager.RaiseDeviceManagerNotification, "Bell");
            var phoneLine = new DevicePhoneLine(_devManager.RaiseDeviceManagerNotification, "PhoneLine");
            var receiver = new DeviceReceiver(_devManager.RaiseDeviceManagerNotification, "Receiver");

            Devices = new Dictionary<string, object>()
            {
                {"Bell",bell},
                {"PhoneLine",phoneLine},
                {"Receiver",receiver}

            };
        }
    }
}
