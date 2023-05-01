using BreakingRat.Services.AssetManagement;
using System.Collections.Generic;
using UnityEngine;

namespace BreakingRat.GameLogic.Location.MazeLogic
{
    public class MazeSpawner
    {
        private readonly float _distanceBetweenCells = 1;
        private readonly MazeGenerator _mazeGenerator;
        private readonly IAssetProvider _assetProvider;
        private readonly  List<Maze> _mazes = new();
        private int _score = 0;

        public MazeGenerator MazeGenerator => _mazeGenerator;
        public int Capacity { get; set; } = 10;
        public int Score => _score;
        public List<Maze> Mazes => _mazes;

        public MazeSpawner(MazeGenerator mazeGenerator, IAssetProvider assetProvider)
        {
            _mazeGenerator = mazeGenerator;
            _assetProvider = assetProvider;
        }

        public Maze SpawnMaze(int width, int height, Vector3 mazePosition, TemplateCell? entry = null)
        {
            if (_mazes.Count > Capacity)
            {
                Remove(_mazes[0].gameObject);
                _mazes.RemoveAt(0);
            }

            var templateMaze = _mazeGenerator.Generate(width, height, entry);

            Maze maze = _assetProvider.Instantiate<Maze>(AssetPaths.MazePrefabPath, mazePosition, Quaternion.identity);

            var cells = new Cell[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    cells[x, y] = SpawnCell(templateMaze[x, y], maze.transform);
                }
            }

            var bounds = SpawnBounds(templateMaze, maze);

            maze.Construct(templateMaze, cells, bounds);

            maze.ExitTrigger.Enter.AddListener
                ( collider => 
                {
                    _score++;
                    GameObject.Destroy(maze.ExitTrigger);
                });

            maze.ExitTrigger.transform.position += Vector3.up * height * _distanceBetweenCells;

            _mazes.Add(maze);

            return maze;
        }

        public Maze SpawnMaze(int width, int height, Vector3 mazePosition,TemplateMaze templateMaze, TemplateCell? entry = null)
        {
            if (_mazes.Count > Capacity)
            {
                Remove(_mazes[0].gameObject);
                _mazes.RemoveAt(0);
            }

            Maze maze = _assetProvider.Instantiate<Maze>(AssetPaths.MazePrefabPath, mazePosition, Quaternion.identity);

            var cells = new Cell[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    cells[x, y] = SpawnCell(templateMaze[x, y], maze.transform);
                }
            }

            var bounds = SpawnBounds(templateMaze, maze);

            maze.Construct(templateMaze, cells, bounds);

            maze.ExitTrigger.Enter.AddListener
                (collider =>
                {
                    _score++;
                    GameObject.Destroy(maze.ExitTrigger);
                });

            maze.ExitTrigger.transform.position += Vector3.up * height * _distanceBetweenCells;

            _mazes.Add(maze);

            return maze;
        }

        private Cell SpawnCell(TemplateCell templateCell, Transform parent)
        {
            float positionX = templateCell.X * _distanceBetweenCells + parent.position.x;
            float positionY = templateCell.Y * _distanceBetweenCells + parent.position.y;
            float positionZ = 0 + parent.position.z;

            var position = new Vector3(positionX, positionY, positionZ);

            Cell cell = _assetProvider.Instantiate<Cell>(AssetPaths.CellPrefabPath, position, Quaternion.identity, parent);


            if (templateCell.LeftWall == false)
                Remove(cell.LeftWall);

            if (templateCell.BottomWall == false)
                Remove(cell.BottomWall);

            return cell;
        }

        private Cell[] SpawnBounds(TemplateMaze templateMaze, Maze maze)
        {
            var bounds = new List<Cell>();

            foreach (var templateCell in templateMaze.Bounds)
                bounds.Add(SpawnCell(templateCell, maze.transform));

            return bounds.ToArray();
        }

        private void Remove(GameObject obj) => GameObject.Destroy(obj);
    }
}
