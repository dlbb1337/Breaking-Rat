using BreakingRat.Application.Abstractions.IServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BreakingRat.Application.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Slider _deadzoneBar;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _unPauseButton;
        private IScoreService _scoreService;
        private IPauseService _pauseService;

        public Transform Player { get; set; }
        public Transform Deadzone { get; set; }
        public float Multiplier { get; set; } = .01f;

        [Inject]
        private void Construct(IScoreService scoreService, IPauseService pauseService)
        {
            _scoreService = scoreService;
            _pauseService = pauseService;

            _pauseButton.onClick.AddListener(_pauseService.PauseGame);
            _unPauseButton.onClick.AddListener(_pauseService.UnPauseGame);
        }

        private void Awake()
        {
            _deadzoneBar.maxValue = 0;
            _deadzoneBar.minValue = -1;
        }

        private void FixedUpdate()
        {
            if (Player is null == false || Deadzone is null == false)
                UpdateDeadzoneBar();

            if (_scoreService is null == false)
                UpdateScore(_scoreService);
        }

        private void UpdateDeadzoneBar()
        {
            var distance = Vector3.Distance(Player.position, Deadzone.position);

            _deadzoneBar.value = -distance * Multiplier;
        }

        private void UpdateScore(int score)
        {
            _scoreText.text = $"{score}";
        }
    }
}
