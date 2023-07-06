using BreakingRat.Application.CompositionRoot;
using BreakingRat.Infrastructure.CompositionRoot;
using Zenject;

namespace BreakingRat.Presentation.CompositionRoot
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindAssetProvider()
                .BindFactories();
        }
    }
}