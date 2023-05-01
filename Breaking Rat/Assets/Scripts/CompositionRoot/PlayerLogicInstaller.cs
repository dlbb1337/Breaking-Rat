using BreakingRat.GameLogic.Location.MazeLogic;
using BreakingRat.Services.AssetManagement;
using BreakingRat.Services.Input;
using BreakingRat.Services.Input.InputSystem;
using UnityEngine;
using Zenject;

namespace BreakingRat.CompositionRoot
{
    public class PlayerLogicInstaller : MonoInstaller
    {
        [SerializeField] private InputService _inputService;

        public override void InstallBindings()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<MazeGenerator>().FromNew().AsSingle();
            Container.Bind<MazeSpawner>().FromNew().AsSingle();

            Container.Bind<InputService>().FromInstance(_inputService).AsSingle();
            Container.Bind<ITouchService>().To<TouchService>().AsSingle();
        }
    }
}