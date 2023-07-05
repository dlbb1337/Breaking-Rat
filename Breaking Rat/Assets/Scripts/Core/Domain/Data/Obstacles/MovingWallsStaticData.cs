using UnityEngine;

namespace BreakingRat.Domain.Data.Obstacles
{
    [CreateAssetMenu(fileName = "MovingWallsStaticData", menuName = "StaticData/MovingWallsStaticData")]
    public class MovingWallsStaticData : ObstaclesStaticData
    {
        [SerializeField] private int _countPerMaze;
        [SerializeField] private float _minMovementSpeed;
        [SerializeField] private float _maxMovementSpeed;
        [SerializeField] private float _xCenter;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _minDistance;

        public int CountPerMaze => _countPerMaze;
        public float MinMovementSpeed => _minMovementSpeed;
        public float MaxMovementSpeed => _maxMovementSpeed;
        public float XCenter => _xCenter;
        public float MaxDistance => _maxDistance;
        public float MinDistance => _minDistance;
        public override int ObstacleId => 2;
    }
}
