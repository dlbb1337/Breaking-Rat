using System;

namespace BreakingRat.Infrastructure.States
{
    public interface IState:IExitableState
    {
        void Enter();
    }
}
