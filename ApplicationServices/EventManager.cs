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



    }
}
