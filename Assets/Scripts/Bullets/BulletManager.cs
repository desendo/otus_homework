using System;
using System.Collections.Generic;
using Effects;
using Level;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletManager : IStartGame, IFixedUpdate
    {
        private readonly BulletSpawner _bulletSpawner;
        private readonly LevelBounds _levelBounds;
        private readonly EffectsService _effectsService;
        private readonly BulletPool _bulletPool;

        private readonly HashSet<Bullet> _activeBullets = new HashSet<Bullet>();

        public BulletManager(BulletSpawner bulletSpawner, BulletPool bulletPool, LevelBounds levelBounds, EffectsService effectsService)
        {
            _bulletSpawner = bulletSpawner;
            _levelBounds = levelBounds;
            _effectsService = effectsService;
            _bulletPool = bulletPool;
        }

        public void StartGame()
        {
            Clear();
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                _bulletPool.Unspawn(bullet);
            }
        }

        public void FlyBulletByArgs(Args args)
        {
            var bullet = _bulletSpawner.SpawnBullet(args);
            if(_activeBullets.Add(bullet))
                bullet.OnCollisionEntered += OnBulletCollision;
        }

        public void FixedUpdate(float dt)
        {
            var bulletsToRemove = new List<Bullet>();

            foreach (var bullet in _activeBullets)
            {
                if (!_levelBounds.InBounds(bullet.transform.position))
                {
                    bulletsToRemove.Add(bullet);
                }
            }

            foreach (var bullet in bulletsToRemove)
            {
                RemoveBullet(bullet);
            }
        }

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            ShowHitEffect(collision);
            RemoveBullet(bullet);
        }

        private void ShowHitEffect(Collision2D collision)
        {
            var midPoint = Vector2.zero;
            var midNormal = Vector2.zero;
            foreach (var contactPoint2D in collision.contacts)
            {
                midPoint += contactPoint2D.point;
                midNormal += contactPoint2D.normal;
            }

            midNormal /= collision.contacts.Length;
            midPoint /= collision.contacts.Length;
            _effectsService.ShowHitEffect(midPoint, midNormal);
        }

        private void Clear()
        {
            foreach (var bullet in _activeBullets)
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                _bulletPool.Unspawn(bullet);
            }
            _activeBullets.Clear();
        }
        public struct Args
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public Color Color;
            public int PhysicsLayer;
            public int Damage;
            public bool IsPlayer;
        }
    }
}