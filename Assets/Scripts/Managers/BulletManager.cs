using System.Collections.Generic;
using Common.Entities;
using Effects;
using Models.Components;
using Pool;
using UnityEngine;

namespace Managers
{
    public interface IBulletSpawner
    {
        EntityMono FireBullet(Vector3 position, Vector3 direction, float speed, int damage);
    }

    public class BulletManager : IFixedUpdate, IBulletSpawner
    {
        private readonly List<EntityMono> _spawnedBullets = new List<EntityMono>();
        private readonly HashSet<EntityMono> _removeBullets = new HashSet<EntityMono>();
        private readonly BulletPool _bulletPool;
        private readonly EffectsService _effectsService;

        public BulletManager(BulletPool bulletPool, EffectsService effectsService)
        {
            _effectsService = effectsService;
            _bulletPool = bulletPool;
        }

        public void FixedUpdate(float dt)
        {
            foreach (var spawnedBullet in _spawnedBullets)
            {
                var deltaMove = spawnedBullet.Get<Component_Move>().Velocity * dt;
                spawnedBullet.Get<Component_Transform>().Translate(deltaMove);
                spawnedBullet.Get<Component_Finish>().TimeLeft.Value -= dt;
            }

            foreach (var removeBullet in _removeBullets)
            {
                _spawnedBullets.Remove(removeBullet);
            }
            _removeBullets.Clear();
        }

        public EntityMono FireBullet(Vector3 position, Vector3 direction, float speed, int damage)
        {
            var instance = _bulletPool.Spawn();
            _spawnedBullets.Add(instance);

            instance.Get<Component_Collision>().OnCollision.Subscribe(OnHit);
            instance.Get<Component_Finish>().OnFinish.Subscribe(DisposeBullet);

            instance.Get<Component_Direction>().Direction.Value = direction;
            instance.Get<Component_Speed>().Speed = speed;
            instance.Get<Component_Damage>().Damage.Value = damage;

            instance.Get<Component_Activate>().Activate();

            var rootTransform = instance.Get<Component_Transform>().RootTransform;
            rootTransform.position = position;
            rootTransform.forward = direction;

            return instance;

        }

        private void OnHit(IEntity bullet, Collision collision)
        {
            if(collision.body != null && collision.body.TryGetComponent<IEntity>(out var entity))
            {
                var takeDamage = entity.Get<Component_TakeDamage>();
                var damage = bullet.Get<Component_Damage>().Damage.Value;
                var bulletDir = bullet.Get<Component_Transform>().RootTransform.forward;
                entity.Get<Component_Rigidbody>().Rigidbody.AddForce(bulletDir * 50);
                takeDamage.DoDamage(damage);

                _effectsService.ShowHitEffect(collision);
            }

            DisposeBullet(bullet);
        }
        private void DisposeBullet(IEntity bullet)
        {
            bullet.Get<Component_Collision>().OnCollision.UnSubscribe(OnHit);
            bullet.Get<Component_Finish>().OnFinish.UnSubscribe(DisposeBullet);
            _bulletPool.Unspawn(bullet as EntityMono);
            _removeBullets.Add(bullet as EntityMono);
        }

    }
}