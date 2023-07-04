using System;

namespace BreakingRat.Assets.Scripts.Core.Application.Abstractions.Services
{
    public interface IPauseService
    {
        event Action Pause;
        event Action Unpause;

        void PauseGame();

        void UnPauseGame();
    }
}
