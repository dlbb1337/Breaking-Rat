using BreakingRat.Data;
using BreakingRat.Data.Obstacles;
using BreakingRat.GameLogic.DeathLogic;
using BreakingRat.GameLogic.Location.MazeLogic;
using BreakingRat.GameLogic.Obstacles.GameObjects;
using BreakingRat.GameLogic.PlayerLogic;
using BreakingRat.UI;
using UnityEngine;

namespace BreakingRat.Infrastructure.Factory
{
    public interface IFactory
    {

        public Maze InstantiateMaze
            (int width, int height, Vector3 mazePosition, float distanceBetweenCells, TemplateCell? entry = null);

        public Maze InstantiateMaze
            (TemplateMaze templateMaze, Vector3 mazePosition, float distanceBetweenCells, TemplateCell? entry = null);



        public void Remove(GameObject obj);

        void CreateDeathScreen();

        Deadzone CreateDeadzone(Vector3 position, Quaternion rotation, Transform parent = null);
        PlayerMovement CreatePlayer(Vector3 position, Quaternion rotation, Transform parent = null);
        HUD CreateHUD();
        MovingWall CreateMovingWall(Vector3 position, Quaternion rotation, Transform parent = null);
        Gun CreateGun(Vector3 position, Quaternion rotation, Transform parent = null);
        Bullet CreateBullet(Vector3 position, Quaternion rotation, Transform parent = null);
    }
}
