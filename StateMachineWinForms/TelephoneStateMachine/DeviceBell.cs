using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices;

namespace TelephoneStateMachine
{
    public class DeviceBell:Device
    {

        public bool Ringing { get; set; }

   
        public DeviceBell(Action<string, string, string> devEventMethod, string deviceName) : base(devEventMethod, deviceName)
        {
        }

        public void Silent()
        {
            Ringing = false;
        }

        public void Rings()
        {
            Ringing = true;
            System.Media.SystemSounds.Hand.Play();
        }


        public override void OnInit()
        {
            Ringing = false;
        }
    }
}
