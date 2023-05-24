using BreakingRat.Data.Services;
using BreakingRat.Infrastructure.Services.AssetManagement;
using BreakingRat.UI.Factory;
using Zenject;
using Factory = BreakingRat.Infrastructure.Factory.Factory;
using IFactory = BreakingRat.Infrastructure.Factory.IFactory;

public class MenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        Container.Bind<IFactory>().To<Factory>().AsSingle();
        Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
    }
}