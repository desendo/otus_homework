using System.Collections.Generic;
using Common.Entities;
using Models.Components;
using Pool;
using UnityEngine;

namespace Managers
{
    public class BulletManager : IFixedUpdate
    {
        private readonly List<EntityMono> _spawnedBullets = new List<EntityMono>();
        private readonly HashSet<EntityMono> _removeBullets = new HashSet<EntityMono>();
        private readonly BulletPool _bulletPool;

        public BulletManager(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
        }

        public void FixedUpdate(float dt)
        {
            foreach (var spawnedBullet in _spawnedBullets)
            {
                var deltaMove = spawnedBullet.Get<Component_Move>().Velocity.Value * dt;
                spawnedBullet.Get<Component_Transform>().Translate(deltaMove);
                spawnedBullet.Get<Component_Finish>().TimeLeft.Value -= dt;
            }

            foreach (var removeBullet in _removeBullets)
            {
                _spawnedBullets.Remove(removeBullet);
            }
            _removeBullets.Clear();
        }

        public EntityMono FireBullet(Vector3 position, Vector3 direction, float speed)
        {
            var instance = _bulletPool.Spawn();
            _spawnedBullets.Add(instance);
            instance.Get<Component_Collision>().OnEventWithCollision.Subscribe(OnHit);
            instance.Get<Component_Finish>().OnFinish += DestroyBullet;
            instance.Get<Component_Direction>().Direction.Value = direction;
            instance.Get<Component_Speed>().Speed = speed;
            instance.Get<Component_Activate>().Activate();
            var rootTransform = instance.Get<Component_Transform>().RootTransform;
            rootTransform.position = position;
            rootTransform.forward = direction;

            return instance;

        }

        private void OnHit(IEntity arg1, Collision arg2)
        {

        }
        private void DestroyBullet(IEntity bullet)
        {
            bullet.Get<Component_Collision>().OnEventWithCollision.UnSubscribe(OnHit);
            bullet.Get<Component_Finish>().OnFinish -= DestroyBullet;
            _bulletPool.Unspawn(bullet as EntityMono);
            _removeBullets.Add(bullet as EntityMono);

        }
    }
}