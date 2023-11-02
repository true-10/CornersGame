using System;

namespace Corners
{
    public sealed class StateMachine
    {
        public Action<IState> OnStateEnter { get; set; }
        public Action<IState> OnStateExit { get; set; }

        private IState _currentState;
        private StateManager stateManager;

        public StateMachine(StateManager stateManager)
        {
            this.stateManager = stateManager;
        }

        public void SetState(int stateType, Action OnError = null)
        {
            var newState = stateManager.GetStateByType(stateType);
            if (newState == null)
            {
                OnError?.Invoke();
                return;
            }

            if (_currentState != null)
            {
                _currentState.OnExit();
                OnStateExit?.Invoke(_currentState);
            }
            _currentState = newState;
            _currentState.OnEnter();
            OnStateEnter?.Invoke(_currentState);
        }

        public void Tick()
        {
            if (_currentState == null)
            {
                //OnError?.Invoke();
                return;
            }
            _currentState.OnUpdate();
        }
    }
}
