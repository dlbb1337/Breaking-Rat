using BreakingRat.Application.Abstractions.IServices;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace BreakingRat.Application.Services.Factories
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;

        public UIFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async Task<Button> CreateLevelButtonAsync(Transform parent)
        {
            var button = await _assetProvider
            .Instantiate(AssetPaths.LevelButtonPrefabPath, parent);

            return button.GetComponent<Button>();
        }

        public async Task<CanvasGroup> InstantiateCurtainAsync()
        {
            var UIRoot = await CreateUIRootAsync();

            var gameObject = await _assetProvider.Instantiate
                (AssetPaths.CurtainPrefabPath, UIRoot.transform);
            return gameObject.GetComponent<CanvasGroup>();
        }

        public async Task<Canvas> CreateUIRootAsync()
        {
            var gameObject = await _assetProvider
            .Instantiate(AssetPaths.UIRootPrefabPath);

            return gameObject.GetComponent<Canvas>();
        }
    }
}