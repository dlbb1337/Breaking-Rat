using System.Collections.Generic;
using System;
using UnityEngine;

namespace BreakingRat.Infrastructure
{
    public class GameStateMachine 
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine(Dictionary<Type, IState> states)
        {
            _states = states;
        }

        public void EnterState<TState>() where TState : IState
        {
            _activeState?.OnExit();
            var state = GetState<TState>();
            _activeState = state;
            state.OnEnter();
        }

        public void ExitState<TState>() where TState : IState
        {
            var state = GetState<TState>();
            state.OnExit();
        }

        private IState GetState<TState>() where TState : IState => 
            _states[typeof(TState)];
    }
}
