using BreakingRat.Application.Abstractions.IServices;
using BreakingRat.Application.UI;

namespace BreakingRat.Application.StateMachine.States
{
    public class LoadSceneState : IValueState<string>
    {
        private readonly GameCurtain _gameCurtain;
        private readonly ISceneLoaderService _sceneLoader;

        public LoadSceneState(ISceneLoaderService sceneLoader, GameCurtain gameCurtain)
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
