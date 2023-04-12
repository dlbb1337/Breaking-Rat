using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BreakingRat.Location.MazeLogic
{
    public class MazeGenerator
    {
        private int _width;
        private int _height;
        private TemplateMaze _maze;

        public TemplateMaze Generate(int width, int height)
        {
            _width = width;
            _height = height;

            _maze = new TemplateMaze(width, height);

            Algorythm();

            return _maze;
        }

        private void Algorythm()
        {
            var track = new Stack<TemplateCell>();
            var visitedCells = new List<TemplateCell>();
            var adjacentCells = new TemplateCell?[4];

            var currentCell = _maze[GetRandomInt(0, _width), GetRandomInt(0, _height)];

            track.Push(currentCell);
            while(true)
            {
                visitedCells.Add(currentCell);
                if(track.Count <= 0)
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

                Move(_maze[currentCell.X,currentCell.Y], _maze[chosenCell.Value.X, chosenCell.Value.Y]);

                track.Push(currentCell);
                currentCell = (TemplateCell)chosenCell;
            }
        }

        private void FindAdjacentCells(TemplateCell from, TemplateCell?[] buffer)
        {
            SetCellFromMaze(from.X + 1, from.Y, out buffer[0]);

            SetCellFromMaze(from.X - 1, from.Y, out buffer[1]);

            SetCellFromMaze(from.X, from.Y - 1, out buffer[2]);

            SetCellFromMaze(from.X, from.Y + 1, out buffer[3]);
        }

        private void SetCellFromMaze(int x , int y, out TemplateCell? cell)
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
            else if(from.Y!= to.Y)
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
