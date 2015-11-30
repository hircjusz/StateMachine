using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    public class StateMachineEventArgs
    {
        public string EventName { get; set; }

        public string EventInfo { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Source { get; set; }

        public string Target { get; set; }

        public StateMachineEventType EventType { get; set; }
        
        public StateMachineEventArgs(string eventName, string eventInfo, string source, string target, StateMachineEventType eventType)
        {
            EventName = eventName;
            EventInfo = eventInfo;
            Source = source;
            Target = target;
            EventType = eventType;
            TimeStamp = DateTime.Now;
        }

    }
}
