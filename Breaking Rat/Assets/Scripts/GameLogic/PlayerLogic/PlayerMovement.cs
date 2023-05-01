using BreakingRat.Services.Input;
using UnityEngine;
using Zenject;

namespace BreakingRat.GameLogic.PlayerLogic
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _turnPercentage;
        [SerializeField] private Rigidbody2D _rb2D;
        private float _degrees;
        private ITouchService _touchService;

        [Inject]
        private void Construct(ITouchService touchService)
        {
            _touchService = touchService;
            _touchService.TouchBegun += HandleTouch;
        }

        private void FixedUpdate()
        {
            Move(transform.up * Time.fixedDeltaTime * _moveSpeed);
            Rotate(FloatLerp(_rb2D.rotation, _degrees, _turnPercentage * 0.01f));
        }

        private void HandleTouch(Vector2 position)
        {
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
    }
}
