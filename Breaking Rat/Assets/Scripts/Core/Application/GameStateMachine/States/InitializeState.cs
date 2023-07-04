using BreakingRat.Assets.Scripts.Core.Application.UI;

namespace BreakingRat.Assets.Scripts.Core.Application.StateMachine.States
{
    public class InitializeState : IState
    {
        private readonly GameCurtain _gameCurtain;

        public InitializeState(GameCurtain gameCurtain)
        {
            _gameCurtain = gameCurtain;
        }

        public void Enter()
        {
            _gameCurtain.Text.text = "Initializing...";
        }

        public void Exit()
        {
            _gameCurtain.Text.text = "";
        }
    }
}
