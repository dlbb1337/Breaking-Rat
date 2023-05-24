using BreakingRat.Data.Obstacles;
using BreakingRat.GameLogic.Location.MazeLogic;

namespace BreakingRat.GameLogic.Obstacles
{
    public interface IObstacles
    {
        public int ObstacleId { get; }
        public void Add(Maze maze, ObstaclesStaticData data);
    }
}
