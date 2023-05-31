using System;
using System.Collections.Generic;
using Effects;
using Level;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletManager : IStartGame, IFixedUpdate, IFinishGame
    {
        private readonly BulletSpawner _bulletSpawner;
        private readonly LevelBounds _levelBounds;
        private readonly EffectsService _effectsService;
        private IDisposable _collisionSubscription;
        private readonly BulletPool _bulletPool;

        public BulletManager(BulletSpawner bulletSpawner, LevelBounds levelBounds, EffectsService effectsService)
        {
            _bulletSpawner = bulletSpawner;
            _levelBounds = levelBounds;
            _effectsService = effectsService;
        }

        public void StartGame()
        {
            _collisionSubscription = _bulletSpawner.OnBulletCollision.Subscribe(OnBulletCollision);
            _bulletSpawner.Clear();
        }

        public void FixedUpdate(float dt)
        {
            List<Bullet> bulletsToRemove = new List<Bullet>();
            foreach (var bullet in _bulletSpawner.Bullets)
            {
                if (!_levelBounds.InBounds(bullet.transform.position))
                {
                    bulletsToRemove.Add(bullet);
                }
            }

            foreach (var bullet in bulletsToRemove)
            {
                _bulletSpawner.RemoveBullet(bullet);
            }
        }

        public void FlyBulletByArgs(Args args)
        {
            _bulletSpawner.SpawnBullet(args);
        }
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);

            ShowHitEffect(collision);

            _bulletSpawner.RemoveBullet(bullet);
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

        public struct Args
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public Color Color;
            public int PhysicsLayer;
            public int Damage;
            public bool IsPlayer;
        }

        public void FinishGame()
        {
            _collisionSubscription?.Dispose();
        }
    }
}