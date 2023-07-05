namespace BreakingRat.Application.StateMachine.States
{
    public interface IValueState<TValue> : IExitableState
    {
        void Enter(TValue value);
    }
}
