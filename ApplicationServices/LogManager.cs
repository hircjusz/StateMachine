using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;

namespace ApplicationServices
{
    public interface ILogManager
    {
        void Log(object sender, StateMachineEventArgs args);
    }

    public class LogManager : ILogManager
    {
         public void Log(object sender, StateMachineEventArgs args)
         {
             Debug.Print(args.EventName);
         }


    }
}
