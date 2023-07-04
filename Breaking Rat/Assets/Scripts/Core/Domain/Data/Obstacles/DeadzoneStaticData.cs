using UnityEngine;

namespace BreakingRat.Assets.Scripts.Core.Domain.Data.Obstacles
{
    [CreateAssetMenu(fileName = "DeadzoneStaticData", menuName = "StaticData/DeadzoneStaticData")]
    public class DeadzoneStaticData : ScriptableObject
    {
        [SerializeField] private Vector3 _instantiateDeadzonePosition;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _speedMultiplier;

        public Vector3 InstantiateDeadzonePosition => _instantiateDeadzonePosition;
        public float MovementSpeed => _movementSpeed;
        public float SpeedMultiplier => _speedMultiplier;
    }
}
