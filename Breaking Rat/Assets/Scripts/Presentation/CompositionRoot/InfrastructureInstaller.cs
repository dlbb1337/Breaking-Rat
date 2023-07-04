using Zenject;
using UnityEngine;
using BreakingRat.Assets.Scripts.Core.Application.StateMachine.States;
using BreakingRat.Assets.Scripts.Core.Application.StateMachine;
using BreakingRat.Assets.Scripts.Core.Application.UI;
using BreakingRat.Assets.Scripts.Core.Application.Abstractions.Services;
using BreakingRat.Assets.Scripts.Infrastructure.Persistence.SceneManagment;
using BreakingRat.Assets.Scripts.Infrastructure.Persistence.Services;

namespace BreakingRat.Assets.Scripts.Presentation.CompositionRoot
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