using BreakingRat.Infrastructure;
using BreakingRat.Infrastructure.States;
using System;

namespace BreakingRat.GameLogic.Services
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
