using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    public class Transistion
    {
        public string Name { get; private set; }

        public string SourceStateName { get; private set; }

        public string TargetStateName { get; private set; }

        public List<StateMachineAction> GuardList { get; private set; }

        public List<StateMachineAction> TransistionActionList { get; private set; }

        public string Trigger { get; private set; }

        public Transistion(string trigger, List<StateMachineAction> transistionActionList, List<StateMachineAction> guardList, string targetStateName, string sourceStateName, string name)
        {
            Trigger = trigger;
            TransistionActionList = transistionActionList;
            GuardList = guardList;
            TargetStateName = targetStateName;
            SourceStateName = sourceStateName;
            Name = name;
        }
    }
}
