using BreakingRat.GameLogic.DeathLogic;
using BreakingRat.GameLogic.Location.MazeLogic;
using BreakingRat.GameLogic.Obstacles.GameObjects;
using BreakingRat.GameLogic.PlayerLogic;
using BreakingRat.Infrastructure.Services.AssetManagement;
using BreakingRat.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<Maze> InstantiateMaze
            (int width,
             int height,
             Vector3 mazePosition,
             float distanceBetweenCells,
             TemplateCell? entry = null)
        {
            var templateMaze = _mazeGenerator.Generate(width, height, entry);

            return await InstantiateMaze(templateMaze, mazePosition, distanceBetweenCells, entry);
        }

        public async Task<Maze> InstantiateMaze
            (TemplateMaze templateMaze,
             Vector3 mazePosition,
             float distanceBetweenCells,
             TemplateCell? entry = null)
        {
            var width = templateMaze.Width;
            var height = templateMaze.Height;

            Maze maze = await _assetProvider.InstantiateWithDI<Maze>
                (AssetPaths.MazePrefabPath, mazePosition, Quaternion.identity);

            var cells = new Cell[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    cells[x, y] = await SpawnCell
                        (templateMaze[x, y], distanceBetweenCells, maze.transform);
                }
            }

            var bounds = await SpawnBounds(templateMaze, distanceBetweenCells, maze);

            maze.Construct(templateMaze, cells, bounds);

            maze.ExitTrigger.transform.position += Vector3.up * height * distanceBetweenCells;

            return maze;
        }

        private async Task<Cell[]> SpawnBounds
            (TemplateMaze templateMaze,
             float distanceBetweenCells,
             Maze maze)
        {
            var bounds = new List<Cell>();

            foreach (var templateCell in templateMaze.Bounds)
                bounds.Add(await SpawnCell(templateCell, distanceBetweenCells, maze.transform));

            return bounds.ToArray();
        }

        private async Task<Cell> SpawnCell
            (TemplateCell templateCell,
             float distanceBetweenCells,
             Transform parent)
        {
            float positionX = templateCell.X * distanceBetweenCells + parent.position.x;
            float positionY = templateCell.Y * distanceBetweenCells + parent.position.y;
            float positionZ = 0 + parent.position.z;

            var position = new Vector3(positionX, positionY, positionZ);

            Cell cell = await _assetProvider.InstantiateWithDI<Cell>
                (AssetPaths.CellPrefabPath, position, Quaternion.identity, parent);


            if (templateCell.LeftWall == false)
                Remove(cell.LeftWall);

            if (templateCell.BottomWall == false)
                Remove(cell.BottomWall);

            return cell;
        }

        public async Task<Deadzone> CreateDeadzone
            (Vector3 position,
             Quaternion rotation,
             Transform parent = null)
        {
            return await _assetProvider.InstantiateWithDI<Deadzone>
                (AssetPaths.DeadzonePrefabPath, position, rotation);
        }

        public async Task<PlayerMovement> CreatePlayer
            (Vector3 position,
             Quaternion rotation,
             Transform parent = null)
        {
            return await _assetProvider.InstantiateWithDI<PlayerMovement>
                            (AssetPaths.PlayerPrefabPath, position, rotation);
        }

        public void Remove(GameObject obj) =>
            GameObject.Destroy(obj);

        public async Task<HUD> CreateHUD()
        {
            return await _assetProvider.InstantiateWithDI<HUD>(AssetPaths.HUDPrefabPath);
        }

        public async Task<DeathScreen> CreateDeathScreenAsync()
        {
            return await _assetProvider.InstantiateWithDI<DeathScreen>
                (AssetPaths.DeathScreenPrefabPath);
        }

        public async Task<MovingWall> CreateMovingWall
            (Vector3 position,
             Quaternion rotation,
             Transform parent = null)
        {
            return await _assetProvider.InstantiateWithDI<MovingWall>
                (AssetPaths.MovingWallPrefabPath, position, rotation, parent);
        }

        public async Task<Gun> CreateGun
            (Vector3 position,
             Quaternion rotation,
             Transform parent = null)
        {
            return await _assetProvider.InstantiateWithDI<Gun>
                (AssetPaths.GunPrefabPath, position, rotation, parent);
        }

        public async Task<Bullet> CreateBullet
            (Vector3 position,
             Quaternion rotation,
             Transform parent = null)
        {
            return await _assetProvider.InstantiateWithDI<Bullet>
                (AssetPaths.BulletPrefabPath, position, rotation, parent);
        }
    }
}
