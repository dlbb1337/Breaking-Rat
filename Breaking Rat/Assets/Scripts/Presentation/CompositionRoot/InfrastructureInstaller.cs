using BreakingRat.Data.Services;
using BreakingRat.Infrastructure;
using BreakingRat.Infrastructure.States;
using BreakingRat.UI;
using Zenject;
using UnityEngine;
using BreakingRat.Infrastructure.Services.Ads;

namespace BreakingRat.CompositionRoot
{
    public class InfrastructureInstaller : MonoInstaller
    {
        [SerializeField] private GameCurtain _gameCurtain;

        public override void InstallBindings()
        {
            Container.Bind<GameCurtain>().FromInstance(_gameCurtain).AsSingle();
            Container.Bind<IProgressService>().To<ProgressService>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<SceneLoader>().To<SceneLoader>().AsSingle();
            Container.BindInterfacesTo<GameLoopState>().AsSingle();
            Container.BindInterfacesTo<LoadSceneState>().AsSingle();
            Container.BindInterfacesTo<InitializeState>().AsSingle();
            Container.BindInterfacesTo<PauseState>().AsSingle();
            Container.Bind<GameStateMachine>().FromNew().AsSingle();
        }
    }
}