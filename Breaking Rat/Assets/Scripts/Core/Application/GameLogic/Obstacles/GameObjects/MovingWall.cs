using BreakingRat.Assets.Scripts.Core.Application.Abstractions.Services;
using UnityEngine;
using Zenject;

namespace BreakingRat.Assets.Scripts.Core.Application.GameLogic.Obstacles.GameObjects
{
    public class MovingWall : MonoBehaviour
    {
        private float _movementSpeed;
        private float _xCenter;
        private float _distance;
        private bool _movingRight;
        private float _minDistance;
        private float _maxDistance;
        private IPauseService _pauseService;
        private bool _isPaused = true;

        public void Construct(float movementSpeed, float xCenter, float distance)
        {
            _movementSpeed = movementSpeed;
            _xCenter = xCenter;
            _distance = distance;
        }

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

        private void FixedUpdate()
        {
            if (_isPaused)
                return;

            Move();
        }

        private void Move()
        {
            _minDistance = _xCenter - _distance;
            _maxDistance = _xCenter + _distance;

            if (_movingRight && transform.position.x > _maxDistance)
                _movingRight = false;

            if (_movingRight == false && transform.position.x < _minDistance)
                _movingRight = true;

            var targetPosition = Vector3.right * _movementSpeed * Time.fixedDeltaTime;

            if (_movingRight == false)
                targetPosition = -targetPosition;

            transform.position = transform.position + targetPosition;
        }
    }
}
