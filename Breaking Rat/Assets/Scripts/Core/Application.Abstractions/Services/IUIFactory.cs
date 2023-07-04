using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace BreakingRat.Assets.Scripts.Core.Application.Abstractions.Services
{
    public interface IUIFactory
    {
        Task<Canvas> CreateUIRootAsync();
        Task<Button> CreateLevelButtonAsync(Transform parent);
        public Task<CanvasGroup> InstantiateCurtainAsync();
    }
}
