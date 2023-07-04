using BreakingRat.Assets.Scripts.Core.Application.UI;
using BreakingRat.Assets.Scripts.Infrastructure.Persistence.SceneManagment;

namespace BreakingRat.Assets.Scripts.Core.Application.StateMachine.States
{
    public class LoadSceneState : IValueState<string>
    {
        private readonly GameCurtain _gameCurtain;
        private readonly SceneLoader _sceneLoader;

        public LoadSceneState(SceneLoader sceneLoader, GameCurtain gameCurtain)
        {
            _gameCurtain = gameCurtain;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.LoadScene(sceneName, OnLoaded);
            _gameCurtain.Text.text = "Loading...";
        }

        private void OnLoaded()
        {

        }

        public void Exit()
        {
            _gameCurtain.Text.text = "";
        }
    }
}
