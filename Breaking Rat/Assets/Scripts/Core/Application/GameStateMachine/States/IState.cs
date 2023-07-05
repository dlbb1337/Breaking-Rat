using System;

namespace BreakingRat.Application.StateMachine.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}
