using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    public class StateMachineAction
    {


        public string Name { get; private set; }

        private Action _method;


        public StateMachineAction(string name,Action method)
        {
            Name = name;
            _method = method;
        }


        public void Execute()
        {
            _method();
        }


    }
}
