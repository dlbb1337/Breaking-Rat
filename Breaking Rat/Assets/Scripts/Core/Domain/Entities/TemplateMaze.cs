using System;

namespace BreakingRat.Domain.Entities
{
    public class TemplateMaze
    {
        private readonly TemplateCell[,] _maze;

        public TemplateCell? exit { get; set; }
        public TemplateCell[] Bounds { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public TemplateMaze(int width, int height)
        {
            Width = width;
            Height = height;

            _maze = InitializeMaze(Width, Height);
            Bounds = InitializeBounds(Width, Height);
        }

        private TemplateCell[] InitializeBounds(int width, int height)
        {
            var bounds = new TemplateCell[width + height];

            for (int i = 0; i < width; i++)
            {
                bounds[i].X = i;
                bounds[i].Y = height;
                bounds[i].LeftWall = false;
                bounds[i].BottomWall = true;
            }

            for (int i = 0; i < height; i++)
            {
                bounds[i + width].X = width;
                bounds[i + width].Y = i;
                bounds[i + width].LeftWall = true;
                bounds[i + width].BottomWall = false;
            }

            return bounds;
        }

        public TemplateCell[,] InitializeMaze(int width, int height)
        {
            var cells = new TemplateCell[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    cells[x, y].X = x;
                    cells[x, y].Y = y;
                    cells[x, y].LeftWall = true;
                    cells[x, y].BottomWall = true;
                }
            }

            return cells;
        }

        public TemplateCell this[int x, int y]
        {
            get
            {
                if (x >= Width || x < 0 || y >= Height || y < 0)
                    throw new ArgumentException();

                return _maze[x, y];
            }
            set
            {
                if (x >= Width || x < 0 || y >= Height || y < 0)
                    throw new ArgumentException();

                _maze[x, y] = value;
            }
        }
    }
}
