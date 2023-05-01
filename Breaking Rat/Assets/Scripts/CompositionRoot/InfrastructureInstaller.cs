using BreakingRat.Infrastructure;
using BreakingRat.Infrastructure.States;
using Zenject;

namespace BreakingRat.CompositionRoot
{
    public class InfrastructureInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().To<SceneLoader>().AsSingle();
            Container.BindInterfacesTo<GameLoopState>().AsSingle();
            Container.BindInterfacesTo<LoadLevelState>().AsSingle();
            Container.Bind<GameStateMachine>().FromNew().AsSingle();
            Container.Bind<Game>().FromNew().AsSingle();
        }
    }
}