using BreakingRat.Assets.Scripts.Core.Application.Abstractions.Services;
using UnityEngine;
using Zenject;

namespace BreakingRat.Assets.Scripts.Core.Application.GameLogic.PlayerLogic
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _turnPercentage;
        [SerializeField] private Rigidbody2D _rb2D;

        private bool _paused = true;
        private float _degrees;
        private ITouchService _touchService;
        private IPauseService _pauseService;

        public float MovementSpeed { get => _moveSpeed; set => _moveSpeed = value; }
        public float TurnPercentage { get => _turnPercentage; set => _turnPercentage = value; }

        [Inject]
        private void Construct(ITouchService touchService, IPauseService pauseService)
        {
            _touchService = touchService;
            _touchService.TouchBegun += HandleTouch;

            _pauseService = pauseService;

            pauseService.Pause += OnPause;
            pauseService.Unpause += OnUnPause;
        }

        private void OnUnPause()
        {
            _paused = false;
        }

        private void OnPause()
        {
            _paused = true;
        }

        private void FixedUpdate()
        {
            if (_paused)
                return;

            Move(transform.up * Time.fixedDeltaTime * _moveSpeed);
            Rotate(FloatLerp(_rb2D.rotation, _degrees, _turnPercentage * 0.01f));
        }

        private void HandleTouch(Vector2 position)
        {
            if (_paused)
                return;

            if (IsLeftPartOfScreen(position))
                TurnLeft();
            else
                TurnRight();
        }

        private void TurnRight() =>
            _degrees -= 90;

        private void TurnLeft() =>
            _degrees += 90;

        private void Move(Vector3 towards) =>
            _rb2D.MovePosition(transform.position + towards);

        private void Rotate(float degrees) =>
            _rb2D.MoveRotation(degrees);

        private bool IsLeftPartOfScreen(Vector2 position)
        {
            if (Screen.width / 2 < position.x)
                return false;
            else
                return true;
        }

        private float FloatLerp(float a, float b, float t)
        {
            t = Mathf.Clamp01(t);
            return a + (b - a) * t;
        }

        private void OnDisable()
        {
            _pauseService.Pause -= OnPause;
            _pauseService.Unpause -= OnUnPause;
            _touchService.TouchBegun -= HandleTouch;
        }
    }
}
