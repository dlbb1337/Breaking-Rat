using System;

namespace BreakingRat.Application.Abstractions.IServices
{
    public interface IPauseService
    {
        event Action Pause;
        event Action Unpause;

        void PauseGame();

        void UnPauseGame();
    }
}
