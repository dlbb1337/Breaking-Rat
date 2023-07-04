using BreakingRat.Assets.Scripts.Core.Application.GameLogic.DeathLogic;
using BreakingRat.Assets.Scripts.Core.Application.GameLogic.Location.MazeLogic;
using BreakingRat.Assets.Scripts.Core.Application.GameLogic.Obstacles.GameObjects;
using BreakingRat.Assets.Scripts.Core.Application.GameLogic.PlayerLogic;
using BreakingRat.Assets.Scripts.Core.Application.UI;
using BreakingRat.Assets.Scripts.Core.Domain.Entities;
using System.Threading.Tasks;
using UnityEngine;

namespace BreakingRat.Assets.Scripts.Core.Application.Abstractions.Services
{
    public interface IFactory
    {

        public Task<Maze> InstantiateMaze
            (int width,
             int height,
             Vector3 mazePosition,
             float distanceBetweenCells,
             TemplateCell? entry = null);

        public Task<Maze> InstantiateMaze
            (TemplateMaze templateMaze,
             Vector3 mazePosition,
             float distanceBetweenCells,
             TemplateCell? entry = null);

        public void Remove(GameObject obj);

        Task<DeathScreen> CreateDeathScreenAsync();

        Task<Deadzone> CreateDeadzone
            (Vector3 position, Quaternion rotation, Transform parent = null);

        Task<PlayerMovement> CreatePlayer
            (Vector3 position, Quaternion rotation, Transform parent = null);

        Task<HUD> CreateHUD();

        Task<MovingWall> CreateMovingWall
            (Vector3 position, Quaternion rotation, Transform parent = null);

        Task<Gun> CreateGun(Vector3 position, Quaternion rotation, Transform parent = null);

        Task<Bullet> CreateBullet(Vector3 position, Quaternion rotation, Transform parent = null);
    }
}
