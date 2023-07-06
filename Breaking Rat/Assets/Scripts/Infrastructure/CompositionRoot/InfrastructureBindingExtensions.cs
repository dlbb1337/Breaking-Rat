using BreakingRat.Application.Abstractions.IServices;
using BreakingRat.Application.GameLogic.Obstacles;
using BreakingRat.Infrastructure.Persistence.SceneManagment;
using BreakingRat.Infrastructure.Persistence.Services;
using BreakingRat.Infrastructure.Persistence.Services.Ads;
using BreakingRat.Infrastructure.Persistence.Services.AssetManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace BreakingRat.Infrastructure.CompositionRoot
{
    public static class InfrastructureBindingExtensions
    {
        public static DiContainer BindDataServices(this DiContainer container)
        {
            container.Bind<IProgressService>().To<ProgressService>().AsSingle();
            container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();

            return container;
        }

        public static DiContainer BindObstacles(this DiContainer container)
        {
            container.Bind<IObstacles>().To<CameraChangerObstacles>().AsSingle();
            container.Bind<IObstacles>().To<MovingWalls>().AsSingle();
            container.Bind<IObstacles>().To<GunsObstacle>().AsSingle();

            return container;
        }

        public static DiContainer BindSceneLoader(this DiContainer container)
        {
            container.Bind<ISceneLoaderService>().To<SceneLoader>().AsSingle();

            return container;
        }

        public static DiContainer BindAssetProvider(this DiContainer container)
        {
            container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();

            return container;
        }

        public static DiContainer BindPauseService(this DiContainer container)
        {
            container.Bind<IPauseService>().To<PauseService>().AsSingle();

            return container;
        }

        public static DiContainer BindAds(this DiContainer container)
        {
            container.Bind<IAdsService>().To<AdsService>().AsSingle();

            return container;
        }
    }
}
