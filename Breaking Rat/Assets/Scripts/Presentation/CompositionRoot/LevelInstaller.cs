using BreakingRat.Application.Abstractions.Services.InputSystem;
using BreakingRat.Application.CompositionRoot;
using BreakingRat.Infrastructure.CompositionRoot;
using UnityEngine;
using Zenject;

namespace BreakingRat.Presentation.CompositionRoot
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private InputService _inputService;

        public override void InstallBindings()
        {
            Container
                .BindPauseService()
                .BindGameServices()
                .BindAds()
                .BindAssetProvider()
                .BindFactories()
                .BindObstacles()
                .BindInputServices(_inputService)
                .BindMazes();

            Container.Bind<InitializeLevel>().FromNew().AsSingle().NonLazy();
        }
    }
}