using BreakingRat.Services;
using System.Collections.Generic;
using UnityEngine;

namespace BreakingRat.Location.MazeLogic
{
    public class MazeSpawner
    {
        private readonly float _distanceBetweenCells = 1;
        private readonly MazeGenerator _mazeGenerator;
        private readonly IAssetProvider _assetProvider;
        private readonly List<Maze> _mazes = new();

        public MazeSpawner(MazeGenerator mazeGenerator, IAssetProvider assetProvider)
        {
            _mazeGenerator = mazeGenerator;
            _assetProvider = assetProvider;
        }

        public Maze SpawnMaze(int width, int height, Vector3 mazePosition)
        {
            var templateMaze = _mazeGenerator.Generate(width, height);

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

            _mazes.Add(maze);

            return maze;
        }

        private Cell SpawnCell(TemplateCell templateCell, Transform parent)
        {
            float positionX = templateCell.X * _distanceBetweenCells + parent.position.x;
            float positionY = templateCell.Y * _distanceBetweenCells+parent.position.y;
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

            foreach(var templateCell in templateMaze.Bounds)
                bounds.Add(SpawnCell(templateCell, maze.transform));

            return bounds.ToArray();
        }

        private void Remove(GameObject obj) => GameObject.Destroy(obj);
    }
}
