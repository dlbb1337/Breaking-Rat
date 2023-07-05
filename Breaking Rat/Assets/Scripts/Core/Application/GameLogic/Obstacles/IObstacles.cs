using BreakingRat.Application.GameLogic.Location.MazeLogic;
using BreakingRat.Domain.Data.Obstacles;
using System.Threading.Tasks;

namespace BreakingRat.Application.GameLogic.Obstacles 
{ 
    public interface IObstacles
    {
        public int ObstacleId { get; }
        Task AddAsync(Maze maze, ObstaclesStaticData data);
    }
}
