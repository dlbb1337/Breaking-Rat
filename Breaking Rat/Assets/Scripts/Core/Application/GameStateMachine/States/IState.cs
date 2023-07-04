using System;

namespace BreakingRat.Assets.Scripts.Core.Application.StateMachine.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}
