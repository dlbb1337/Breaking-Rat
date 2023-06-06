using UnityEngine;
using UnityEngine.UI;

namespace BreakingRat.UI.Factory
{
    public interface IUIFactory
    {
        Canvas CreateUIRoot();
        Button CreateLevelButton(Transform parent);
        public CanvasGroup InstantiateCurtain();
    }
}
