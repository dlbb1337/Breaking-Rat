using BreakingRat.Assets.Scripts.Core.Application.GameLogic.Location.MazeLogic;
using BreakingRat.Assets.Scripts.Core.Domain.Data.Obstacles;
using System.Threading.Tasks;

namespace BreakingRat.Assets.Scripts.Core.Application.GameLogic.Obstacles
{
    public interface IObstacles
    {
        public int ObstacleId { get; }
        Task AddAsync(Maze maze, ObstaclesStaticData data);
    }
}
