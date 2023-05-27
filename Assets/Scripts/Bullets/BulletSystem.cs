using System.Collections.Generic;
using System.Linq;
using Level;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletSystem : MonoBehaviour, IStartGame
    {
        [SerializeField] private int _initialCount = 50;
        [SerializeField] private Transform _container;
        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private LevelBounds _levelBounds;

        private readonly HashSet<Bullet> _activeBullets = new HashSet<Bullet>();
        private readonly Queue<Bullet> _bulletPool = new Queue<Bullet>();
        private readonly List<Bullet> _cache = new List<Bullet>();

        private void Awake()
        {
            for (var i = 0; i < _initialCount; i++)
            {
                var bullet = Instantiate(_prefab, _container);
                _bulletPool.Enqueue(bullet);
            }
        }

        public void StartGame()
        {
            var list = _activeBullets.ToList();
            foreach (var activeBullet in list)
            {
                RemoveBullet(activeBullet);
            }
            _activeBullets.Clear();
        }

        private void FixedUpdate()
        {
            _cache.Clear();
            _cache.AddRange(_activeBullets);

            for (int i = 0, count = _cache.Count; i < count; i++)
            {
                var bullet = _cache[i];
                if (!_levelBounds.InBounds(bullet.transform.position))
                    RemoveBullet(bullet);
            }
        }

        public void FlyBulletByArgs(Args args)
        {
            if (_bulletPool.TryDequeue(out var bullet))
                bullet.transform.SetParent(_worldTransform);
            else
                bullet = Instantiate(_prefab, _worldTransform);

            bullet.SetPosition(args.Position);
            bullet.SetColor(args.Color);
            bullet.SetPhysicsLayer(args.PhysicsLayer);
            bullet.Damage = args.Damage;
            bullet.IsPlayer = args.IsPlayer;
            bullet.SetVelocity(args.Velocity);

            if (_activeBullets.Add(bullet))
                bullet.OnCollisionEntered += OnBulletCollision;
        }

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                bullet.transform.SetParent(_container);
                _bulletPool.Enqueue(bullet);
            }
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