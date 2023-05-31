using System.Collections.Generic;
using ReactiveExtension;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletSpawner
    {
        private readonly HashSet<Bullet> _activeBullets = new HashSet<Bullet>();
        private readonly Event<Bullet, Collision2D> _onBulletCollision = new Event<Bullet, Collision2D>();
        private readonly BulletPool _bulletPool;
        public IReadOnlyEvent<Bullet, Collision2D> OnBulletCollision => _onBulletCollision;

        public IEnumerable<Bullet> Bullets => _activeBullets;
        public BulletSpawner(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
        }

        public void SpawnBullet(BulletManager.Args args)
        {
            var bullet = _bulletPool.Spawn();
            bullet.SetPosition(args.Position);
            bullet.SetColor(args.Color);
            bullet.SetPhysicsLayer(args.PhysicsLayer);
            bullet.Damage = args.Damage;
            bullet.IsPlayer = args.IsPlayer;
            bullet.SetVelocity(args.Velocity);

            if(_activeBullets.Add(bullet))
                bullet.OnCollisionEntered += OnBulletCollide;
        }

        private void OnBulletCollide(Bullet arg1, Collision2D arg2)
        {
            _onBulletCollision?.Invoke(arg1, arg2);
        }

        public void RemoveBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollide;
                _bulletPool.Unspawn(bullet);
            }
        }

        public void Clear()
        {
            foreach (var bullet in _activeBullets)
            {
                bullet.OnCollisionEntered -= OnBulletCollide;
            }
            _bulletPool.Clear();
        }
    }
}