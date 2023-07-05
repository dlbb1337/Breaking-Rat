using BreakingRat.Application.Abstractions.IServices;
using UnityEngine;
using Zenject;

namespace BreakingRat.Application.GameLogic.Obstacles.GameObjects
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Trigger _trigger;
        private bool _isPaused = true;

        public Trigger Trigger => _trigger;
        public float MovementSpeed { get; set; }
        public Vector3 Direction { get; set; }

        private IPauseService _pauseService;

        [Inject]
        private void Construct(IPauseService pauseService)
        {
            _pauseService = pauseService;

            _pauseService.Pause += OnPause;
            _pauseService.Unpause += OnUnpause;
        }

        private void OnPause()
        {
            _isPaused = true;
        }

        private void OnUnpause()
        {
            _isPaused = false;
        }

        private void OnDisable()
        {
            _pauseService.Pause -= OnPause;
            _pauseService.Unpause -= OnUnpause;
        }

        private void OnEnable()
        {
            if (_pauseService != null)
            {

                _pauseService.Pause += OnPause;
                _pauseService.Unpause += OnUnpause;
            }
        }
        private void FixedUpdate()
        {
            if (_isPaused) return;

            Move();
        }

        private void Move()
        {
            transform.position = transform.position + Direction * Time.fixedDeltaTime * MovementSpeed;
        }

    }
}
