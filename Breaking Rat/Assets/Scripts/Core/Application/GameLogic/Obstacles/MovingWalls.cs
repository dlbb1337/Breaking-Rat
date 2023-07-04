using BreakingRat.Data.Obstacles;
using BreakingRat.GameLogic.Location.MazeLogic;
using BreakingRat.Infrastructure.Factory;
using System.Threading.Tasks;
using UnityEngine;

namespace BreakingRat.GameLogic.Obstacles
{
    public class MovingWalls : IObstacles
    {
        private MovingWallsStaticData _data;
        private IFactory _factory;

        public int ObstacleId => 2;

        public MovingWalls(IFactory factory)
        {
            _factory = factory;
        }

        public async Task AddAsync(Maze maze, ObstaclesStaticData data)
        {
            _data = data as MovingWallsStaticData;

            var countPerMaze = _data.CountPerMaze;

            for (int i = 0; i < countPerMaze; i++)
            {
                await CreateMovingWallAsync(maze, _data);
            }
        }

        private async Task CreateMovingWallAsync(Maze maze, MovingWallsStaticData data)
        {
            var rndY = Random.Range(0, maze.Height);
            var rndX = Random.Range(0, maze.Width);

            var wall = await _factory.CreateMovingWall(maze[rndX,rndY].transform.position, Quaternion.identity, maze.transform);

            wall.Construct
                (Random.Range(data.MinMovementSpeed, data.MaxMovementSpeed),
                data.XCenter,
                Random.Range(data.MinDistance, data.MaxDistance));
        }
    }
}
