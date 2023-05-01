using System.Collections.Generic;
using System;
using UnityEngine;

namespace BreakingRat.Infrastructure
{
    public class GameStateMachine 
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(List<IExitableState> states)
        {
            var dictionaryStates = new Dictionary<Type, IExitableState>();

            foreach (var state in states)
            {
                dictionaryStates.Add(state.GetType(), state);
            }

            _states = dictionaryStates;
        }

        public void EnterState<TState>() where TState : class,IState
        {
            _activeState?.Exit();
            var state = GetState<TState>();
            _activeState = state;
            state.Enter();
        }

        public void EnterState<TState, TPay>(TPay pay) where TState : class, IPayLoadedState<TPay>
        {
            _activeState?.Exit();
            var state = GetState<TState>();
            _activeState = state;
            state.Enter(pay);
        }

        public void ExitState<TState>() where TState : class,IState
        {
            var state = GetState<TState>();
            state.Exit();
        }

        private TState GetState<TState>() where TState : class,IExitableState => 
            _states[typeof(TState)] as TState;
    }
}
