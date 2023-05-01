using System;

namespace BreakingRat.Infrastructure
{
    public interface IState:IExitableState
    {
        void Enter();
    }
}
