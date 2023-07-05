using BreakingRat.Application.Abstractions.IServices;
using BreakingRat.Application.Services.Factories;
using BreakingRat.Infrastructure.Persistence.Services.AssetManagement;
using Zenject;
using Factory = BreakingRat.Application.Services.Factories.Factory;
using IFactory = BreakingRat.Application.Services.Factories.IFactory;

namespace BreakingRat.Presentation.CompositionRoot
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IFactory>().To<Factory>().AsSingle();
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
        }
    }
}