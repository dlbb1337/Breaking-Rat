using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace BreakingRat.UI.Factory
{
    public interface IUIFactory
    {
        Task<Canvas> CreateUIRootAsync();
        Task<Button> CreateLevelButtonAsync(Transform parent);
        public Task<CanvasGroup> InstantiateCurtainAsync();
    }
}
