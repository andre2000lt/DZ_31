using UnityEngine;

namespace _MiniGame
{
    public class BulletShooter
    {
        private Bullet _bulletPrefab;
        private Transform _bulletSpawnPoint;

        public BulletShooter(Bullet bulletPrefab, Transform bulletSpawnPoint)
        {
            _bulletPrefab = bulletPrefab;
            _bulletSpawnPoint = bulletSpawnPoint;
        }

        public void Shoot()
        {
            Bullet bullet = Object.Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
            bullet.Initialize(_bulletSpawnPoint.forward);
        }
    }
}