using BreakingRat.GameLogic.DeathLogic;
using BreakingRat.GameLogic.Location.MazeLogic;
using BreakingRat.GameLogic.Obstacles.GameObjects;
using BreakingRat.GameLogic.PlayerLogic;
using BreakingRat.Infrastructure.Services.AssetManagement;
using BreakingRat.UI;
using System.Collections.Generic;
using UnityEngine;

namespace BreakingRat.Infrastructure.Factory
{
    public class Factory : IFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly MazeGenerator _mazeGenerator;

        public Factory(MazeGenerator mazeGenerator, IAssetProvider assetProvider)
        {
            _mazeGenerator = mazeGenerator;
            _assetProvider = assetProvider;
        }

        public Maze InstantiateMaze
            (int width, int height, Vector3 mazePosition, float distanceBetweenCells, TemplateCell? entry = null)
        {
            var templateMaze = _mazeGenerator.Generate(width, height, entry);

            return InstantiateMaze(templateMaze, mazePosition, distanceBetweenCells, entry);
        }

        public Maze InstantiateMaze
            (TemplateMaze templateMaze, Vector3 mazePosition, float distanceBetweenCells, TemplateCell? entry = null)
        {
            var width = templateMaze.Width;
            var height = templateMaze.Height;

            Maze maze = _assetProvider.InstantiateWithDI<Maze>
                (AssetPaths.MazePrefabPath, mazePosition, Quaternion.identity);

            var cells = new Cell[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    cells[x, y] = SpawnCell(templateMaze[x, y], distanceBetweenCells, maze.transform);
                }
            }

            var bounds = SpawnBounds(templateMaze, distanceBetweenCells, maze);

            maze.Construct(templateMaze, cells, bounds);

            maze.ExitTrigger.transform.position += Vector3.up * height * distanceBetweenCells;

            return maze;
        }

        private Cell[] SpawnBounds(TemplateMaze templateMaze, float distanceBetweenCells, Maze maze)
        {
            var bounds = new List<Cell>();

            foreach (var templateCell in templateMaze.Bounds)
                bounds.Add(SpawnCell(templateCell, distanceBetweenCells, maze.transform));

            return bounds.ToArray();
        }

        private Cell SpawnCell(TemplateCell templateCell, float distanceBetweenCells, Transform parent)
        {
            float positionX = templateCell.X * distanceBetweenCells + parent.position.x;
            float positionY = templateCell.Y * distanceBetweenCells + parent.position.y;
            float positionZ = 0 + parent.position.z;

            var position = new Vector3(positionX, positionY, positionZ);

            Cell cell = _assetProvider.InstantiateWithDI<Cell>
                (AssetPaths.CellPrefabPath, position, Quaternion.identity, parent);


            if (templateCell.LeftWall == false)
                Remove(cell.LeftWall);

            if (templateCell.BottomWall == false)
                Remove(cell.BottomWall);

            return cell;
        }

        public Deadzone CreateDeadzone(Vector3 position, Quaternion rotation, Transform parent = null) =>
            _assetProvider.InstantiateWithDI<Deadzone>
                (AssetPaths.DeadzonePrefabPath, position, rotation);

        public PlayerMovement CreatePlayer(Vector3 position, Quaternion rotation, Transform parent = null) =>
            _assetProvider.InstantiateWithDI<PlayerMovement>
                            (AssetPaths.PlayerPrefabPath, position, rotation);

        public void Remove(GameObject obj) =>
            GameObject.Destroy(obj);

        public HUD CreateHUD() =>
            _assetProvider.InstantiateWithDI<HUD>(AssetPaths.HUDPrefabPath);

        public void CreateDeathScreen() =>
            _assetProvider.InstantiateWithDI<DeathScreen>(AssetPaths.DeathScreenPrefabPath);

        public MovingWall CreateMovingWall(Vector3 position, Quaternion rotation, Transform parent = null) =>
            _assetProvider.InstantiateWithDI<MovingWall>
                (AssetPaths.MovingWallPrefabPath, position, rotation, parent);

        public Gun CreateGun(Vector3 position, Quaternion rotation, Transform parent = null) =>
            _assetProvider.InstantiateWithDI<Gun>
                (AssetPaths.GunPrefabPath, position, rotation, parent);

        public Bullet CreateBullet(Vector3 position, Quaternion rotation, Transform parent = null) =>
            _assetProvider.InstantiateWithDI<Bullet>
                (AssetPaths.BulletPrefabPath, position, rotation, parent);
    }
}
