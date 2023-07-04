using BreakingRat.Assets.Scripts.Core.Application.UI;

namespace BreakingRat.Assets.Scripts.Core.Application.StateMachine.States
{
    public class PauseState : IState
    {
        private readonly GameCurtain _gameCurtain;

        public PauseState(GameCurtain gameCurtain)
        {
            _gameCurtain = gameCurtain;
        }

        public void Enter()
        {
            _gameCurtain.Text.text = "Game is paused";
        }

        public void Exit()
        {
            _gameCurtain.Text.text = "";
        }
    }
}
