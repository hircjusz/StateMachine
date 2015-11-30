using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices;

namespace TelephoneStateMachine
{
    public class DeviceReceiver:Device
    {

        public bool ReceiverLifted { get; set; }

        public DeviceReceiver(Action<string, string, string> devEventMethod, string deviceName) : base(devEventMethod, deviceName)
        {
        }

        public void OnReceiverUp()
        {
            ReceiverLifted = true;
            DoNotificationCallback("OnReceiverUp","Receiver Lifted","Receiver");
        }

        public void OnReceiverDown()
        {
            ReceiverLifted = true;
            DoNotificationCallback("OnReceiverDown", "Receiver Down", "Receiver");
        }

        public override void OnInit()
        {
            ReceiverLifted = false;
        }
    }
}
