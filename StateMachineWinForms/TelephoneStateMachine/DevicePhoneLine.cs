using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices;

namespace TelephoneStateMachine
{
    public class DevicePhoneLine:Device
    {
        public bool LineActiveInternal{get;set;}
        public bool LineActiveExternal { get; set; }

        public DevicePhoneLine(Action<string, string, string> devEventMethod, string deviceName) : base(devEventMethod, deviceName)
        {
        }

        public void ActiveExternal()
        {
            LineActiveExternal = true;
            DoNotificationCallback("OnLineExternalActive","Phone line set to active",DeviceName);
        }

        public void ActiveInernal()
        {
            LineActiveInternal = true;
        }


        public void OffInternal()
        {
            LineActiveInternal = false;
            System.Media.SystemSounds.Hand.Play();
        }

        public override void OnInit()
        {

            LineActiveInternal =false;
            LineActiveExternal = false;

        }
    }
}
