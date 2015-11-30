using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;

namespace ApplicationServices
{
    public class DeviceManager
    {
        public Dictionary<string,object> DeviceList { get; set; }

        public event EventHandler<StateMachineEventArgs> DeviceManagerEvent;
        public event EventHandler<StateMachineEventArgs> DeviceManagerNotification;


        public void AddDevice(string name, object device)
        {
            DeviceList.Add(name,device);
            RaiseDeviceManagerEvent("Added device",name);
        }

        public void RaiseDeviceManagerEvent(string p, string name)
        {
            var newDMArgs= new StateMachineEventArgs(name,"Device Manager Event"+ name,"Device Manager","",StateMachineEventType.System);
            if (DeviceManagerEvent != null)
            {
                DeviceManagerEvent(this, newDMArgs);
            }
        }

        public void RaiseDeviceManagerNotification(string command, string info,string source)
        {
            var newDMArgs = new StateMachineEventArgs(command, "Device Manager Event" + info, source, "", StateMachineEventType.Notification);
            if (DeviceManagerNotification != null)
            {
                DeviceManagerNotification(this, newDMArgs);
            }
        }

        public void RemoveDevice(string name,object device)
        {
            DeviceList.Remove(name);

        }


        public void SystemEventHandler(object sender, StateMachineEventArgs args)
        {
            if (args.EventName == "OnInit" && args.EventType == StateMachineEventType.Command)
            {
                foreach (var dev in DeviceList)
                {

                    try
                    {
                        var initMethod = dev.Value.GetType().GetMethod("OnInit");
                        initMethod.Invoke(dev.Value, new object[] { });
                        RaiseDeviceManagerEvent("DeviceManager -Initialization device", dev.Key);
                    }
                    catch (Exception ex )
                    {
                        
                        RaiseDeviceManagerEvent("DeviceManager -Initialization device exception", dev.Key); 
                    }
                }

            }

            if (args.EventType == StateMachineEventType.Command)
            {

                if (args.EventName == "OnInit")
                {
                    return;
                }
                DeviceCommandHandler(this,args);

            }
        }


        public void DeviceCommandHandler(object sender, StateMachineEventArgs args)
        {
            if(args.EventType!=StateMachineEventType.Command) return;

            try
            {
                if(!DeviceList.ContainsKey(args.Target))return;
                var device = DeviceList[args.Target];
                var deviceMethod = device.GetType().GetMethod(args.EventName);
                deviceMethod.Invoke(device, new object[] {});

                RaiseDeviceManagerEvent("Device Command",args.Target);
            }
            catch (Exception ex)
            {
                RaiseDeviceManagerEvent("Device Command error", ex.ToString());
            }
        }

        public void LoadDeviceConfiguration(IDeviceConfiguration devManagerConfiguration)
        {
            DeviceList = devManagerConfiguration.Devices;
        }




        public static DeviceManager Instance { get; set; }
    }
}
