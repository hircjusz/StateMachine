using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace ApplicationServices
{
    public abstract class Device
    {
        private Action<string, string, string> _devEventMethod;  
        
        public string DeviceName { get; private set; }

        protected Device(Action<string, string, string> devEventMethod, string deviceName)
        {
            _devEventMethod = devEventMethod;
            DeviceName = deviceName;
        }

        public abstract void OnInit();


        public void RegisterEventCallback(Action<string, string, string> _devEventMethod)
        {
            this._devEventMethod = _devEventMethod;
        }

        public void DoNotificationCallback(string name, string eventInfo, string source)
        {
            _devEventMethod.Invoke(name,eventInfo,source);
        }






    }
}
