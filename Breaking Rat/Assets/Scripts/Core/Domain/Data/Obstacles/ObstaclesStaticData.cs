using UnityEngine;

namespace BreakingRat.Data.Obstacles
{
    public abstract class ObstaclesStaticData : ScriptableObject
    {
        public abstract int ObstacleId { get; }
    }
}
