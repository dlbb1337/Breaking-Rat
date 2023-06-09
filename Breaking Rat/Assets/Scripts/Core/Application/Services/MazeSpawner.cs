using BreakingRat.Application.Abstractions.IServices;
using BreakingRat.Application.GameLogic.Location.MazeLogic;
using BreakingRat.Application.GameLogic.Obstacles;
using BreakingRat.Application.Services.Factories;
using BreakingRat.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace BreakingRat.Application.Services
{
    public class MazeSpawner
    {
        private readonly float _distanceBetweenCells = 1;
        private readonly MazeGenerator _mazeGenerator;
        private readonly IFactory _factory;
        private readonly List<IObstacles> _obstacles = new();
        private readonly List<Maze> _mazes = new();
        private readonly IStaticDataService _staticDataService;
        private IScoreService _scoreService;

        public int LevelId { get; set; }
        public int Capacity { get; set; } = 10;
        public List<Maze> Mazes => _mazes;

        public MazeSpawner
            (MazeGenerator mazeGenerator,
            IFactory factory,
            List<IObstacles> obstacles,
            IStaticDataService staticDataService,
            IScoreService scoreService)
        {
            _mazeGenerator = mazeGenerator;
            _factory = factory;
            _obstacles = obstacles;
            _staticDataService = staticDataService;
            _scoreService = scoreService;
        }

        public async Task<Maze> SpawnMazeAsync
            (int width,
             int height,
             Vector3 mazePosition,
             TemplateCell? entry = null)
        {
            var maze = await _factory.InstantiateMaze
                (width,
                 height,
                 mazePosition,
                 _distanceBetweenCells,
                 entry);

            await AddObstaclesAsync(maze);

            return HandleMaze(maze);
        }

        private async Task AddObstaclesAsync(Maze maze)
        {
            foreach (var obstacles in _obstacles)
            {
                var level = _staticDataService.CurrentLevelStaticData;
                var obstacle = level.ObStacles.Where(x => x.ObstacleId == obstacles.ObstacleId).FirstOrDefault();

                if (obstacle is null == false)
                    await obstacles.AddAsync(maze, obstacle);
            }
        }

        public async Task<Maze> SpawnMazeAsync(TemplateMaze templateMaze, Vector3 mazePosition, TemplateCell? entry = null)
        {
            var maze = await _factory.InstantiateMaze(templateMaze, mazePosition, _distanceBetweenCells, entry);

            return HandleMaze(maze);
        }

        private Maze HandleMaze(Maze maze)
        {
            DestroyExcessMazes();

            SetExitTrigger(maze);

            _mazes.Add(maze);

            return maze;
        }

        private void SetExitTrigger(Maze maze)
        {
            maze.ExitTrigger.Enter.AddListener
                            (collider =>
                            {
                                _scoreService++;
                                _factory.Remove(maze.ExitTrigger.gameObject);
                            });
        }

        private void DestroyExcessMazes()
        {
            if (_mazes.Count > Capacity)
            {
                _factory.Remove(_mazes[0].gameObject);
                _mazes.RemoveAt(0);
            }
        }

    }
}
