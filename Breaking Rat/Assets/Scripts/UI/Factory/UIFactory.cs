using BreakingRat.Infrastructure.Services.AssetManagement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace BreakingRat.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;

        public UIFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public Button CreateLevelButton(Transform parent) =>
            _assetProvider
            .Instantiate(AssetPaths.LevelButtonPrefabPath, parent)
            .GetComponent<Button>();

        public CanvasGroup InstantiateCurtain()
        {
            var UIRoot = CreateUIRoot();

            var curtain = _assetProvider.Instantiate
                (AssetPaths.CurtainPrefabPath, UIRoot.transform).GetComponent<CanvasGroup>();

            return curtain;
        }

        public Canvas CreateUIRoot() =>
            _assetProvider
            .Instantiate(AssetPaths.UIRootPrefabPath)
            .GetComponent<Canvas>();
    }
}