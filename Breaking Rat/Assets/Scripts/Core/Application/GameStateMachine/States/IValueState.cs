namespace BreakingRat.Assets.Scripts.Core.Application.StateMachine.States
{
    public interface IValueState<TValue> : IExitableState
    {
        void Enter(TValue value);
    }
}
