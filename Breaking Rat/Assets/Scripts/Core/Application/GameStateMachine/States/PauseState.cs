using BreakingRat.Application.UI;

namespace BreakingRat.Application.StateMachine.States
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
