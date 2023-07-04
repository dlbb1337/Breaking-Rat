using UnityEngine;

namespace BreakingRat.Assets.Scripts.Core.Application.GameLogic
{
    public class Follow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Vector3 _defaultPosition;
        [SerializeField] private float _interpolatePercentage;
        [SerializeField] private bool _y;
        [SerializeField] private bool _x;
        [SerializeField] private bool _z;

        public bool Y { get => _y; set => _y = value; }
        public bool X { get => _x; set => _x = value; }
        public bool Z { get => _x; set => _x = value; }
        public Vector3 OffSet { get => _offset; set => _offset = value; }

        public void Construct
            (Transform target,
            Vector3 offset, Vector3
            defaultPosition,
            float interpolatePercentage,
            bool x,
            bool y,
            bool z)
        {
            _target = target;
            _offset = offset;
            _interpolatePercentage = interpolatePercentage;
            _y = y;
            _x = x;
            _z = z;
            _defaultPosition = defaultPosition;
        }

        private void LateUpdate()
        {
            var targetPosition = new Vector3
                (_x ? _target.position.x : _defaultPosition.x,
                _y ? _target.position.y : _defaultPosition.y,
                _z ? _target.position.z : _defaultPosition.z) + _offset;

            Move(targetPosition);
        }

        private void Move(Vector3 targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, _interpolatePercentage * 0.01f);
        }
    }
}
