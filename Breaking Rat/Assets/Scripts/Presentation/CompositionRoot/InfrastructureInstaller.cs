using BreakingRat.Application.Abstractions.IServices;
using BreakingRat.Application.StateMachine;
using BreakingRat.Application.StateMachine.States;
using BreakingRat.Application.UI;
using BreakingRat.Infrastructure.Persistence.SceneManagment;
using BreakingRat.Infrastructure.Persistence.Services;
using UnityEngine;
using Zenject;

namespace BreakingRat.Presentation.CompositionRoot
{
    public class InfrastructureInstaller : MonoInstaller
    {
        [SerializeField] private GameCurtain _gameCurtain;

        public override void InstallBindings()
        {
            Container.Bind<GameCurtain>().FromInstance(_gameCurtain).AsSingle();
            Container.Bind<IProgressService>().To<ProgressService>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<ISceneLoaderService>().To<SceneLoader>().AsSingle();
            Container.BindInterfacesTo<GameLoopState>().AsSingle();
            Container.BindInterfacesTo<LoadSceneState>().AsSingle();
            Container.BindInterfacesTo<InitializeState>().AsSingle();
            Container.BindInterfacesTo<PauseState>().AsSingle();
            Container.Bind<GameStateMachine>().FromNew().AsSingle();
        }
    }
}