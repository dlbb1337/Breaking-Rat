using BreakingRat.Data.Obstacles;
using BreakingRat.GameLogic.DeathLogic.Services;
using BreakingRat.GameLogic.Location.MazeLogic;
using BreakingRat.GameLogic.Obstacles.GameObjects;
using BreakingRat.Infrastructure.Factory;
using UnityEngine;

namespace BreakingRat.GameLogic.Obstacles
{
    public class GunsObstacle : IObstacles
    {
        private GunsStaticData _data;
        private IDeathService _deathService;
        private IFactory _factory;

        public int ObstacleId => 3;

        public GunsObstacle(IFactory factory, IDeathService deathService)
        {
            _deathService = deathService;
            _factory = factory;
        }

        public void Add(Maze maze, ObstaclesStaticData data)
        {
            _data = data as GunsStaticData;

            var countPerMaze = _data.CountPerMaze;

            for (int i = 0; i < countPerMaze; i++)
                CreateGun(maze, _data);
        }

        private void CreateGun(Maze maze, GunsStaticData data)
        {
            var rightOrLeft = Random.Range(0, 2) % 2 == 0;
            Vector3 position = GetGunPosition(maze, data, rightOrLeft);
            var gun = _factory.CreateGun(position, Quaternion.identity, maze.transform);

            SetGunValues(data, rightOrLeft, gun);

            for (int i = 0; i < data.BulletsCapacity; i++)
            {
                var bullet = Createbullet(data, position, rightOrLeft ? Vector3.left : Vector3.right, gun.transform);
                gun.Bullets.Add(bullet);
                bullet.gameObject.SetActive(false);
                bullet.Trigger.Enter.AddListener(collider => _deathService.Death());
            }

        }

        private void SetGunValues(GunsStaticData data, bool rightOrLeft, Gun gun)
        {
            var localScale = GetGunLocalScale(gun, rightOrLeft);

            gun.transform.localScale = localScale;
            gun.FireCooldown = Random.Range(data.MinFireCooldown, data.MaxFireCooldown);
            gun.Distance = data.ProjectileDistance;
        }

        private Vector3 GetGunLocalScale(Gun gun, bool rightOrLeft)
        {
            var localScaleX = rightOrLeft ? -gun.transform.localScale.x : gun.transform.localScale.x;
            var localScaleY = gun.transform.localScale.y;
            var localScaleZ = gun.transform.localScale.z;
            var localScale = new Vector3(localScaleX, localScaleY, localScaleZ);

            return localScale;
        }

        private Vector3 GetGunPosition(Maze maze, GunsStaticData data, bool rightOrLeft)
        {
            var positionX = rightOrLeft ? data.LeftPositionX : data.RightPositionX;
            var positionY = maze[0, Random.Range(0, maze.Height)].transform.position.y + .5f;
            var position = new Vector3(positionX, positionY, 0);
            return position;
        }

        private Bullet Createbullet(GunsStaticData data, Vector3 position, Vector3 direction, Transform parent)
        {
            var bullet = _factory.CreateBullet(position, Quaternion.identity, parent);

            bullet.MovementSpeed = Random.Range(data.MinProjectileSpeed, data.MaxProjectileSpeed);
            bullet.Direction = direction;

            return bullet;
        }
    }
}
