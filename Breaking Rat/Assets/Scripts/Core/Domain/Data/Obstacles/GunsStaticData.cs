using UnityEngine;

namespace BreakingRat.Domain.Data.Obstacles
{
    [CreateAssetMenu(fileName = "GunsStaticData", menuName = "StaticData/GunsStaticData")]
    public class GunsStaticData : ObstaclesStaticData
    {
        [SerializeField] private int _countPerMaze;
        [SerializeField] private float _leftPositionX;
        [SerializeField] private float _rightPositionX;
        [SerializeField] private float _maxFireCooldown;
        [SerializeField] private float _minFireCooldown;
        [SerializeField] private float _maxProjectileSpeed;
        [SerializeField] private float _minProjectileSpeed;
        [SerializeField] private float _projectileDistance;
        [SerializeField] private int _bulletsCapacity;

        public float ProjectileDistance => _projectileDistance;
        public float LeftPositionX => _leftPositionX;
        public float RightPositionX => _rightPositionX;
        public int BulletsCapacity => _bulletsCapacity;
        public int CountPerMaze => _countPerMaze;
        public float MaxProjectileSpeed => _maxProjectileSpeed;
        public float MinProjectileSpeed => _minProjectileSpeed;
        public float MaxFireCooldown => _maxFireCooldown;
        public float MinFireCooldown => _minFireCooldown;
        public override int ObstacleId => 3;
    }
}
