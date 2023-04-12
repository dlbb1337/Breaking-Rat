using System;

namespace BreakingRat.Infrastructure
{
    public interface IState
    {
        void OnEnter();
        void OnStay();
        void OnExit();
    }
}
