using System;

namespace BreakingRat.GameLogic.Services
{
    public interface IPauseService
    {
        event Action Pause;
        event Action Unpause;

        void PauseGame();

        void UnPauseGame();
    }
}
