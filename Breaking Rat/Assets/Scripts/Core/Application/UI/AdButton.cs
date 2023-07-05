using BreakingRat.Application.Abstractions.IServices;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BreakingRat.Application.UI
{
    public class AdButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private IAdsService _adsService;

        [Inject]
        private void Construct(IAdsService adsService)
        {
            _adsService = adsService;
        }

        private void Start()
        {
            _button.onClick.AddListener(_adsService.ShowAd);
        }
    }
}
