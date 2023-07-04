using UnityEngine;

namespace BreakingRat.Assets.Scripts.Core.Domain.Data
{
    [CreateAssetMenu(fileName = "PlayerStaticData", menuName = "StaticData/PlayerStaticData")]
    public class PlayerStaticData : ScriptableObject
    {
        [SerializeField] private Vector3 _instantiatePlayerPosition;
        [SerializeField] private float _playerMovementSpeed;
        [SerializeField] private float _turnPercentage;
        [SerializeField] private Vector3 _cameraOffset;
        [SerializeField] private float _cameraSize;
        [SerializeField] private Vector3 _cameraDefaultPosition;
        [SerializeField] private float _interpolatePercentage;
        [SerializeField] private bool _y;
        [SerializeField] private bool _x;
        [SerializeField] private bool _z;

        public float PlayerMovementSpeed => _playerMovementSpeed;
        public Vector3 CameraOffset => _cameraOffset;
        public Vector3 CameraDefaultPosition => _cameraDefaultPosition;
        public float InterpolatePercentage => _interpolatePercentage;
        public bool Y => _y;
        public bool X => _x;
        public bool Z => _z;
        public Vector3 InstantiatePlayerPosition => _instantiatePlayerPosition;
        public float CameraSize => _cameraSize;
        public float TurnPercentage => _turnPercentage;
    }
}
