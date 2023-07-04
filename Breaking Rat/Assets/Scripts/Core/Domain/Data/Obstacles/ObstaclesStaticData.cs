using UnityEngine;

namespace BreakingRat.Assets.Scripts.Core.Domain.Data.Obstacles
{
    public abstract class ObstaclesStaticData : ScriptableObject
    {
        public abstract int ObstacleId { get; }
    }
}
