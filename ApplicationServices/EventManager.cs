using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;

namespace ApplicationServices
{
    public class EventManager
    {

        private Dictionary<string, object> EventList;

        public EventHandler<StateMachineEventArgs> EventManagerEvent;

        private static readonly Lazy<EventManager> _eventManager= new Lazy<EventManager>(()=>new EventManager());

        public static EventManager Instance
        {
            get { return _eventManager.Value; }

        }

        private EventManager()
        {
            EventList= new Dictionary<string, object>();
        }

        public void RegisterEvent(string eventName,object source)
        {
            EventList.Add(eventName,source);
        }

        public bool SubscribeEvent(string eventName,string handlerMethodName,object sink)
        {
            try
            {
                var evt = EventList[eventName];

                var eventInfo = evt.GetType().GetEvent(eventName);
                var methodInfo = sink.GetType().GetMethod(handlerMethodName);

                var handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, sink, methodInfo);
                eventInfo.AddEventHandler(evt, handler);

                return true;
            }
            catch (Exception ex)
            {
                var message = "Exception while subscribing to handler. Event " + eventName;
                RaiseEventManagerEvent("EventManagerSystemEvent",message,StateMachineEventType.System);
                return false;
            }
        }

        private void RaiseEventManagerEvent(string eventName,string eventInfo, StateMachineEventType eventType)
        {
            var newArgs=new StateMachineEventArgs(eventName,eventInfo,String.Empty, String.Empty, eventType);
            if (EventManagerEvent != null)
            {
                EventManagerEvent(this, newArgs);
            }
        }



    }
}
