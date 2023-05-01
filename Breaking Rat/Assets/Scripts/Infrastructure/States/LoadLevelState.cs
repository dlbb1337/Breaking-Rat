using System;

namespace BreakingRat.Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<Action>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly string _sceneName="SampleScene";

        public LoadLevelState(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Enter(Action OnLoaded)
        {
            _sceneLoader.LoadScene(_sceneName, OnLoaded);
        }

        public void Exit()
        {
        }
    }
}
