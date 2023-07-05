using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace BreakingRat.Application.Services.Factories
{
    public interface IUIFactory
    {
        Task<Canvas> CreateUIRootAsync();
        Task<Button> CreateLevelButtonAsync(Transform parent);
        public Task<CanvasGroup> InstantiateCurtainAsync();
    }
}
