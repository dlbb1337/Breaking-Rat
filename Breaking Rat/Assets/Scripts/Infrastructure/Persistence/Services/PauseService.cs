using BreakingRat.Application.Abstractions.IServices;
using BreakingRat.Application.StateMachine;
using BreakingRat.Application.StateMachine.States;
using System;

namespace BreakingRat.Infrastructure.Persistence.Services
{
    public class PauseService : IPauseService
    {
        private GameStateMachine _gameStateMachine;

        public event Action Pause;
        public event Action Unpause;

        public PauseService(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void PauseGame()
        {
            _gameStateMachine.EnterState<PauseState>();
            Pause?.Invoke();
        }

        public void UnPauseGame()
        {
            _gameStateMachine.EnterState<GameLoopState>();
            Unpause?.Invoke();
        }
    }
}
