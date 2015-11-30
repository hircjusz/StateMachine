using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachine;

namespace ApplicationServices
{
    public class ViewManager
    {
        private string[] _viewStates;
        private string DefaultViewState;
        private IUserInterface _UI;


        //public
        public event EventHandler<StateMachineEventArgs> ViewManagerEvent;

        public string CurrentView { get; private set; }

        public IViewStateConfiguration ViewStateConfiguration { get; set; }







    }
}
