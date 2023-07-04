using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BreakingRat.GameLogic.Location.MazeLogic
{
    public class MazeGenerator
    {
        private int _width;
        private int _height;
        private TemplateMaze _maze;

        public TemplateMaze GenerateEmptyMaze
            (int width, int height, TemplateCell? entry = null, TemplateCell? exit = null)
        {
            _width = width;
            _height = height;

            _maze = new TemplateMaze(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    _maze[x, y] = new TemplateCell()
                    {
                        LeftWall = false,
                        BottomWall = false,
                        X = x,
                        Y = y,
                    };
                }
            }

            for (int y = 0; y < height; y++)
            {
                _maze[0, y] = new TemplateCell()
                {
                    LeftWall = true,
                    BottomWall = _maze[0,y].BottomWall,
                    X = 0,
                    Y = y,
                };
            }

            for (int x = 0; x < width; x++)
            {
                _maze[x, 0] = new TemplateCell()
                {
                    LeftWall = _maze[x,0].LeftWall,
                    BottomWall = true,
                    X = x,
                    Y = 0,
                };
            }

            if (entry.HasValue)
            {
                _maze[entry.Value.X, 0] = new TemplateCell()
                {
                    LeftWall = _maze[entry.Value.X, 0].LeftWall,
                    BottomWall = false,
                    X = entry.Value.X,
                    Y = 0,
                };
            }

            if (exit.HasValue)
            {
                _maze.Bounds[exit.Value.X].BottomWall = false;
            }

            return _maze; 
        }

        public TemplateMaze Generate(int width, int height, TemplateCell? entry = null)
        {
            _width = width;
            _height = height;

            _maze = new TemplateMaze(width, height);

            if (entry.HasValue)
            {
                TemplateCell newCell = new()
                {
                    BottomWall = false,
                    LeftWall = true,
                    X = entry.Value.X,
                    Y = 0
                };

                _maze[entry.Value.X, 0] = newCell;
            }

            BackTrackingAlgorythm();

            _maze.exit = GenerateExit();

            return _maze;
        }

        private void BackTrackingAlgorythm()
        {
            var track = new Stack<TemplateCell>();
            var visitedCells = new List<TemplateCell>();
            var adjacentCells = new TemplateCell?[4];

            var currentCell = _maze[GetRandomInt(0, _width), GetRandomInt(0, _height)];

            track.Push(currentCell);
            while (true)
            {
                visitedCells.Add(currentCell);
                if (track.Count <= 0)
                    break;

                FindAdjacentCells(currentCell, adjacentCells);
                if (adjacentCells.All(cell => cell is null || visitedCells.Contains((TemplateCell)cell)))
                {
                    currentCell = track.Pop();
                    continue;
                }

                TemplateCell? chosenCell = null;

                while (chosenCell is null || visitedCells.Contains((TemplateCell)chosenCell))
                {
                    var rndNum = GetRandomInt(0, adjacentCells.Length);
                    chosenCell = adjacentCells[rndNum];
                }

                Move(_maze[currentCell.X, currentCell.Y], _maze[chosenCell.Value.X, chosenCell.Value.Y]);

                track.Push(currentCell);
                currentCell = (TemplateCell)chosenCell;
            }

        }

        private TemplateCell GenerateExit()
        {
            int rndInt = GetRandomInt(0, _width);

            _maze.Bounds[rndInt].BottomWall = false;

            return _maze.Bounds[rndInt];
        }

        private void FindAdjacentCells(TemplateCell from, TemplateCell?[] buffer)
        {
            SetCellFromMaze(from.X + 1, from.Y, out buffer[0]);

            SetCellFromMaze(from.X - 1, from.Y, out buffer[1]);

            SetCellFromMaze(from.X, from.Y - 1, out buffer[2]);

            SetCellFromMaze(from.X, from.Y + 1, out buffer[3]);
        }

        private void SetCellFromMaze(int x, int y, out TemplateCell? cell)
        {
            var isAvailable = CheckIsCellAvailable(x, y);

            if (isAvailable)
                cell = _maze[x, y];
            else
                cell = null;
        }

        private bool CheckIsCellAvailable(int x, int y)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height)
                return false;

            return true;
        }

        private bool Move(TemplateCell from, TemplateCell to)
        {
            if (from.X != to.X)
            {
                if (from.X < to.X)
                {
                    to.LeftWall = false;
                }
                else
                {
                    from.LeftWall = false;
                }
            }
            else if (from.Y != to.Y)
            {
                if (from.Y < to.Y)
                {
                    to.BottomWall = false;
                }
                else
                {
                    from.BottomWall = false;
                }
            }
            else
            {
                return false;
            }

            _maze[to.X, to.Y] = to;
            _maze[from.X, from.Y] = from;

            return true;
        }

        private int GetRandomInt(int minInclusive, int maxExclusive) =>
            Random.Range(minInclusive, maxExclusive);
    }
}
