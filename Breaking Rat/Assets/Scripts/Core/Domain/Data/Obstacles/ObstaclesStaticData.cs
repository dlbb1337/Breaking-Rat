using UnityEngine;

namespace BreakingRat.Domain.Data.Obstacles
{
    public abstract class ObstaclesStaticData : ScriptableObject
    {
        public abstract int ObstacleId { get; }
    }
}
