using BreakingRat.Data.Obstacles;
using BreakingRat.GameLogic.Location.MazeLogic;
using System.Threading.Tasks;

namespace BreakingRat.GameLogic.Obstacles
{
    public interface IObstacles
    {
        public int ObstacleId { get; }
        Task AddAsync(Maze maze, ObstaclesStaticData data);
    }
}
