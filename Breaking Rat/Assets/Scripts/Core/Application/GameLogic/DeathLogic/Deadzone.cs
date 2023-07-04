using BreakingRat.Assets.Scripts.Core.Application.Abstractions.Services;
using UnityEngine;
using Zenject;

namespace BreakingRat.Assets.Scripts.Core.Application.GameLogic.DeathLogic
{
    public class Deadzone : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _speedMultiplier;
        [SerializeField] private Trigger _trigger;
        private IPauseService _pauseService;
        private bool _paused = true;
        private IScoreService _scoreService;

        public float MovementSpeed { get => _moveSpeed; set => _moveSpeed = value; }
        public float SpeedMultiplier { get => _speedMultiplier; set => _speedMultiplier = value; }
        public Trigger Trigger => _trigger;

        [Inject]
        private void Construct
            (IScoreService scoreService, IPauseService pauseService)
        {
            _scoreService = scoreService;

            _pauseService = pauseService;

            _pauseService.Pause += OnPause;
            _pauseService.Unpause += OnUnPause;
        }

        private void OnUnPause()
        {
            _paused = false;
        }

        private void OnPause()
        {
            _paused = true;
        }

        private void Update()
        {
            if (_paused) return;

            Move();
        }

        private void Move()
        {
            transform.position += Vector3.up * (_moveSpeed + _speedMultiplier * _scoreService) * Time.deltaTime;
        }

        private void OnDisable()
        {
            _pauseService.Pause -= OnPause;
            _pauseService.Unpause -= OnUnPause;
        }
    }
}
