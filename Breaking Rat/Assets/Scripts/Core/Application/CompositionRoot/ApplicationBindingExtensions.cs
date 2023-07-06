using BreakingRat.Application.Abstractions.IServices;
using BreakingRat.Application.Abstractions.Services.InputSystem;
using BreakingRat.Application.Services;
using BreakingRat.Application.Services.Factories;
using BreakingRat.Application.StateMachine;
using BreakingRat.Application.StateMachine.States;
using BreakingRat.Application.UI;
using System.ComponentModel;
using Zenject;
using Factory = BreakingRat.Application.Services.Factories.Factory;
using IFactory = BreakingRat.Application.Services.Factories.IFactory;

namespace BreakingRat.Application.CompositionRoot
{
    public static class ApplicationBindingExtensions
    {
        public static DiContainer BindGameStateMachine(this DiContainer container, GameCurtain gameCurtain)
        {
            container.Bind<GameCurtain>().FromInstance(gameCurtain).AsSingle();
            container.BindInterfacesTo<GameLoopState>().AsSingle();
            container.BindInterfacesTo<LoadSceneState>().AsSingle();
            container.BindInterfacesTo<InitializeState>().AsSingle();
            container.BindInterfacesTo<PauseState>().AsSingle();
            container.Bind<GameStateMachine>().FromNew().AsSingle();

            return container;
        }

        public static DiContainer BindFactories(this DiContainer container)
        {
            container.Bind<IFactory>().To<Factory>().AsSingle();
            container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            return container;
        }

        public static DiContainer BindGameServices(this DiContainer container)
        {
            container.Bind<IScoreService>().To<ScoreService>().AsSingle();
            container.Bind<IRecordService>().To<RecordService>().AsSingle();
            container.Bind<IDeathService>().To<DeathService>().AsSingle();

            return container;
        }

        public static DiContainer BindInputServices(this DiContainer container, InputService inputService)
        {
            container.Bind<InputService>().FromInstance(inputService).AsSingle();
            container.Bind<ITouchService>().To<TouchService>().AsSingle();

            return container;
        }

        public static DiContainer BindMazes(this DiContainer container)
        {
            container.Bind<MazeGenerator>().FromNew().AsSingle();
            container.Bind<MazeSpawner>().FromNew().AsSingle();

            return container;
        }
    }
}
