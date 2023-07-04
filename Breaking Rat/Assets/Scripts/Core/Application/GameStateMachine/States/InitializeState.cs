using BreakingRat.UI;

namespace BreakingRat.Infrastructure.States
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
