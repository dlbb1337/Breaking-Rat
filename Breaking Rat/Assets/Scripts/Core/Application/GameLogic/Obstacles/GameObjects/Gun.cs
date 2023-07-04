using BreakingRat.GameLogic.Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BreakingRat.GameLogic.Obstacles.GameObjects
{
    public class Gun : MonoBehaviour
    {
        private float _time;
        private int _pointer;
        public List<Bullet> Bullets { get; } = new();
        public float Distance { get; set; }
        public float FireCooldown { get; set; }

        private IPauseService _pauseService;
        private bool _isPaused = true;

        [Inject]
        private void Construct(IPauseService pauseService)
        {
            _pauseService = pauseService;

            _pauseService.Pause += OnPause;
            _pauseService.Unpause += OnUnpause;
        }

        private void OnPause()
        {
            _isPaused = true;
        }

        private void OnUnpause()
        {
            _isPaused = false;
        }

        private void OnDisable()
        {
            _pauseService.Pause -= OnPause;
            _pauseService.Unpause -= OnUnpause;
        }

        private void FixedUpdate()
        {
            if (_isPaused)
                return;

            _time += Time.fixedDeltaTime;

            if (_time > FireCooldown)
            {
                CheckBulletsRange();

                Shoot();

                _time = 0;
            }
        }

        private void CheckBulletsRange()
        {
            foreach (var bullet in Bullets)
            {
                var distance = Vector3.Distance(transform.position, bullet.transform.position);

                if (distance > Distance)
                    DisableBullet(bullet);
            }
        }

        private void Shoot()
        {
            _pointer++;
            var bullet = Bullets[_pointer % Bullets.Count];
            EnableBullet(bullet);
        }

        private void EnableBullet(Bullet bullet)
        {
            bullet.gameObject.SetActive(true);
        }

        private void DisableBullet(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            bullet.transform.position = transform.position;
        }
    }
}
