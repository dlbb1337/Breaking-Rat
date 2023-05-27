using BreakingRat.Commands;
using BreakingRat.GameLogic;
using BreakingRat.GameLogic.DeathLogic.Services;
using BreakingRat.GameLogic.Location.MazeLogic;
using BreakingRat.GameLogic.Obstacles;
using BreakingRat.GameLogic.Services;
using BreakingRat.Infrastructure.Services.Ads;
using BreakingRat.Infrastructure.Services.AssetManagement;
using BreakingRat.Infrastructure.Services.Input;
using BreakingRat.Infrastructure.Services.Input.InputSystem;
using BreakingRat.UI.Factory;
using GameConsole.CommandTools;
using UnityEngine;
using Zenject;
using Factory = BreakingRat.Infrastructure.Factory.Factory;
using IFactory = BreakingRat.Infrastructure.Factory.IFactory;

namespace BreakingRat.CompositionRoot
{
    public class GameLogicInstaller : MonoInstaller
    {
        [SerializeField] private InputService _inputService;

        public override void InstallBindings()
        {
            Container.Bind<IPauseService>().To<PauseService>().AsSingle();

            Container.Bind<IScoreService>().To<ScoreService>().AsSingle();

            Container.Bind<IAdsService>().To<AdsService>().AsSingle();

            Container.Bind<IRecordService>().To<RecordService>().AsSingle();

            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();

            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();

            Container.Bind<MazeGenerator>().FromNew().AsSingle();

            Container.Bind<IFactory>().To<Factory>().AsSingle();

            Container.Bind<IDeathService>().To<DeathService>().AsSingle();

            Container.Bind<IObstacles>().To<CameraChangerObstacles>().AsSingle();

            Container.Bind<IObstacles>().To<MovingWalls>().AsSingle();

            Container.Bind<IObstacles>().To<GunsObstacle>().AsSingle();

            Container.Bind<MazeSpawner>().FromNew().AsSingle();

            Container.Bind<InputService>().FromInstance(_inputService).AsSingle();

            Container.Bind<ITouchService>().To<TouchService>().AsSingle();

            Container.Bind<ICommandContainer>().To<UIFactoryCommandContainer>().AsSingle();

            Container.Bind<ICommandContainer>().To<ProgressCommands>().AsSingle();

            Container.Bind<InitializeLevel>().FromNew().AsSingle().NonLazy();
        }
    }
}