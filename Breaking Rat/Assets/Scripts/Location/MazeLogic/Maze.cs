using System;
using UnityEngine;

namespace BreakingRat.Location.MazeLogic
{
    public class Maze : MonoBehaviour
    {
        private Cell[,] _maze;

        public Cell[] Bounds { get; private set; }
        public int Width => TemplateMaze.Width;
        public int Height => TemplateMaze.Height;
        public float DistanceBetweenCells { get; private set; }
        public TemplateMaze TemplateMaze { get; private set; }
        public bool Initialized { get; private set; } = false;

        public void Construct(TemplateMaze templateMaze, Cell[,] maze, Cell[] bounds)
        {
            TemplateMaze = templateMaze;
            _maze = maze;
            Bounds = bounds;
            Initialized = true;
        }

        public Cell this[int x, int y]
        {
            get
            {
                if (x >= Width || x < 0 || y >= Height || y < 0)
                    throw new ArgumentException();

                return _maze[x, y];
            }
        }
    }
}
