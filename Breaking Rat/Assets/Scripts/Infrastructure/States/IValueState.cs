namespace BreakingRat.Infrastructure.States
{
    public interface IValueState<TValue> : IExitableState
    {
        void Enter(TValue value);
    }
}
