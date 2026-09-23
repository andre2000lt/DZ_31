using UnityEngine;

namespace _MiniGame
{
    public class Hero : Character, IShooter
    {
        [SerializeField] private Transform _bulletSpawnPoint;

        private Bullet _bulletPrefab;
        private BulletShooter _bulletShooter;

        public void Initialize(int healthAmount, Rotator rotator, float speed, Bullet bulletPrefab)
        {
            base.Initialize(healthAmount, rotator, speed);
            _bulletPrefab = bulletPrefab;

            _bulletShooter = new BulletShooter(_bulletPrefab, _bulletSpawnPoint);
        }

        public void Shoot()
        {
            _bulletShooter.Shoot();
        }
    }
}