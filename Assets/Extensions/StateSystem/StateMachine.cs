using System;
using System.Collections.Generic;
using System.Linq;

namespace StateSystem
{
    public class StateMachine<T>
        where T : Enum
    {
        private readonly Dictionary<T, StateBase<T>> _states;
        private StateBase<T> _currentState;
        public T CurrentStateType;
        
        public StateMachine(IEnumerable<StateBase<T>> states, T startState)
        {
            _states = states.ToDictionary(s => s.State, s => s);
            _currentState = _states[startState];
            _currentState.Enter();
        }

        public void HandleUpdate(float time) => _currentState.HandleUpdate(time);

        public void SwitchState(T newState, bool switchIfEqualCurrentState = false)
        {
            if(_currentState.State.Equals(newState) || !switchIfEqualCurrentState) return;

            _currentState.Exit();
            _currentState = _states[newState];
            _currentState.Enter();
        }
    }
}