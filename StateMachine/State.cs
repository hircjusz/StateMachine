using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    public class State
    {
        public String StateName { get; private set; }

        public Dictionary<string,Transistion> StateTransistionList { get; private set; }

        public List<StateMachineAction > EntryActions { get; private set; }

        public List<StateMachineAction> ExitActions { get; private set; }

        public bool IsDefaultState { get; private set; }

        public State(string stateName, Dictionary<string, Transistion> stateTransistionList, List<StateMachineAction> entryActions, bool isDefaultState, List<StateMachineAction> exitActions)
        {
            StateName = stateName;
            StateTransistionList = stateTransistionList;
            EntryActions = entryActions;
            IsDefaultState = isDefaultState;
            ExitActions = exitActions;
        }
    }
}
