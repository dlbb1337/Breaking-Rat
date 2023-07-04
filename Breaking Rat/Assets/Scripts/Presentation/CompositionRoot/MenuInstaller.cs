using BreakingRat.Assets.Scripts.Core.Application.Abstractions.Services;
using BreakingRat.Assets.Scripts.Core.Application.Services;
using BreakingRat.Assets.Scripts.Infrastructure.Persistence.Services.AssetManagement;
using Zenject;
using Factory = BreakingRat.Assets.Scripts.Core.Application.Services.Factory;
using IFactory = BreakingRat.Assets.Scripts.Core.Application.Abstractions.Services.IFactory;

namespace BreakingRat.Assets.Scripts.Presentation.CompositionRoot
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