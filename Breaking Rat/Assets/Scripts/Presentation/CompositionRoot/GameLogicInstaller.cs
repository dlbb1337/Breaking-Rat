using BreakingRat.Application.Abstractions.IServices;
using BreakingRat.Application.Abstractions.Services.InputSystem;
using BreakingRat.Application.GameLogic.Obstacles;
using BreakingRat.Application.Services;
using BreakingRat.Application.Services.Factories;
using BreakingRat.Assets.Scripts.Presentation;
using BreakingRat.Infrastructure.Persistence.Services;
using BreakingRat.Infrastructure.Persistence.Services.Ads;
using BreakingRat.Infrastructure.Persistence.Services.AssetManagement;
using UnityEngine;
using Zenject;
using Factory = BreakingRat.Application.Services.Factories.Factory;
using IFactory = BreakingRat.Application.Services.Factories.IFactory;

namespace BreakingRat.Presentation.CompositionRoot
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

            Container.Bind<InitializeLevel>().FromNew().AsSingle().NonLazy();
        }
    }
}