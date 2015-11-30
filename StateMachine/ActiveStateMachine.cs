using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StateMachine
{

    public enum EngineState
    {
        Running,
        Stopped,
        Paused,
        Initialized
    }

    public class ActiveStateMachine
    {

        public Dictionary<string, State> StateList { get; private set; }

        public BlockingCollection<string> TriggerQueue { get; private set; }

        public State CurrenState { get; private set; }

        public State PreviouState { get; private set; }

        public EngineState StateMachineEngine { get; private set; }

        public event EventHandler<StateMachineEventArgs> StateMachineEvent;


        private Task _queueWorkerTask;
        private readonly State _initialState;
        private ManualResetEvent _resumer;
        private CancellationTokenSource _tokenSource;


        public ActiveStateMachine(Dictionary<string, State> stateList, int queueCapacity)
        {
            StateList = stateList;
            _initialState = new State("InitialState", null, null, false, null);

            TriggerQueue = new BlockingCollection<string>(queueCapacity);

            InitStateMachine();
            RaiseStateMachineSystemEvent("State Machine Initialized", "System Ready to Start");

            StateMachineEngine = EngineState.Initialized;
        }

        private void RaiseStateMachineSystemEvent(string eventName, string eventInfo)
        {
            if (StateMachineEvent != null)
            {
                StateMachineEvent(this, new StateMachineEventArgs(eventName, eventInfo, "", "State Machine", StateMachineEventType.Command));
            }
        }

        private void RaiseStateMachineSystemCommand(string eventName, string eventInfo)
        {
            if (StateMachineEvent != null)
            {
                StateMachineEvent(this, new StateMachineEventArgs(eventName, eventInfo, "", "State Machine", StateMachineEventType.System));
            }
        }

        private void InitStateMachine()
        {
            PreviouState = _initialState;

            var state = StateList.FirstOrDefault(t => t.Value.IsDefaultState);
            CurrenState = state.Value;
            RaiseStateMachineSystemCommand("OnInit", "StateMachineInitialized");
        }

        public void Start()
        {
            _tokenSource = new CancellationTokenSource();
            _queueWorkerTask = Task.Factory.StartNew(QueueWorkerMethod, _tokenSource, TaskCreationOptions.LongRunning);
            StateMachineEngine = EngineState.Running;
            RaiseStateMachineSystemEvent("State Machine: Started", "System runnin");
        }

        private void QueueWorkerMethod(object arg)
        {
            _resumer.WaitOne();

            try
            {
                foreach (var trigger in TriggerQueue.GetConsumingEnumerable())
                {
                    if (_tokenSource.IsCancellationRequested)
                    {
                        RaiseStateMachineSystemEvent("State machine: Queue Worker", "Processing canceled");
                        return;
                    }

                    //Compare trigger

                    foreach (var transistion in CurrenState.StateTransistionList.Where(transistion => trigger == transistion.Value.Trigger))
                    {
                        ExecuteTransistion(transistion.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                RaiseStateMachineSystemEvent("State Machine:Queue Worker", "Processing canceled");
                Start();
            }


        }

        public void ExecuteTransistion(Transistion transition)
        {
            if (CurrenState.StateName != transition.SourceStateName)
            {
                string message = String.Format("Transisition has wrong source state {0}, when system is in {1}",
                    transition.SourceStateName, CurrenState.StateName);
                RaiseStateMachineSystemEvent("State machine: default guard execute transistion", message);
                return;
            }

            if (!StateList.ContainsKey(transition.TargetStateName))
            {
                string message = String.Format("Transisition has wrong target state {0},when system is in {1}. State not in global", transition.SourceStateName, CurrenState.StateName);
                RaiseStateMachineSystemEvent("State machine: Default guard execute transition ", message);
                return;
            }


            CurrenState.ExitActions.ForEach(a => a.Execute());

            transition.GuardList.ForEach(g => g.Execute());
            string info = transition.GuardList.Count + " guard actions executed";
            RaiseStateMachineSystemEvent("State machine: ExecuteTransistion", info);

            //Run all transistion action list
            transition.TransistionActionList.ForEach(t => t.Execute());

            //State change
            info = transition.TransistionActionList.Count + " transistion actions executed";
            RaiseStateMachineSystemEvent("State machine: Begin state change", info);

            var targetState = GetStateFromStateList(transition.TargetStateName);

            PreviouState = CurrenState;
            CurrenState = targetState;

            foreach (var entryActions in CurrenState.EntryActions)
            {
                entryActions.Execute();
            }

            RaiseStateMachineSystemEvent("State machine: State change completed succesfully", "Previous State" + PreviouState.StateName + " New State= " + CurrenState.StateName);



        }

        private State GetStateFromStateList(string p)
        {
            return StateList[p];
        }

        public void Pause()
        {
            StateMachineEngine = EngineState.Paused;

            _resumer.Reset();

            RaiseStateMachineSystemEvent("State Machine P:Pausesd", "System Waiting");


        }

        public void Resume()
        {
            _resumer.Reset();
            StateMachineEngine = EngineState.Running;
            RaiseStateMachineSystemEvent("state machine: resumed", "system running");

        }

        public void Stop()
        {
            _tokenSource.Cancel();
            _queueWorkerTask.Wait();
            _queueWorkerTask.Dispose();
        }

        //private void RaiseStateMachineSystemCommand(string p1, string p2)
        //{
        //    throw new NotImplementedException();
        //}


    }



}
