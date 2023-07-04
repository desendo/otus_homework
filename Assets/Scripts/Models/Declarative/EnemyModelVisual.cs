using System;
using System.Collections.Generic;
using Common;
using Common.Entities;
using GameManager;
using GameObjectsComponents;
using UnityEngine;

namespace Models.Declarative
{
    public class EnemyModelVisual
    {

        private static readonly int death = Animator.StringToHash("Death");
        private static readonly int attack = Animator.StringToHash("Attack");
        private static readonly int speed = Animator.StringToHash("MoveSpeed");


        private readonly List<IDisposable> _subs = new List<IDisposable>();

        public void Construct(EnemyModelCore core, Animator animator,
            IUpdateProvider updateProvider, Rigidbody rigidbody, Collider hitCollider,
            AnimationEventListener animationEventListener, CollisionSensor weaponCollisionSensor)
        {
            var isDead = core.LifeModel.IsDead;

            weaponCollisionSensor.gameObject.SetActive(false);
            animationEventListener.OnEvent.Subscribe(obj =>
            {
                if(obj == "enable_weapon_collider")
                    weaponCollisionSensor.gameObject.SetActive(true);
                if(obj == "disable_weapon_collider")
                    weaponCollisionSensor.gameObject.SetActive(false);

            }).AddTo(_subs);

            weaponCollisionSensor.Collision.Subscribe(x =>
            {
                if (x.body.TryGetComponent<IEntity>(out var entity))
                {
                    weaponCollisionSensor.gameObject.SetActive(false);
                    core.Weapon.DoHit(entity);
                }
            }).AddTo(_subs);

            core.IsActive.OnChanged.Subscribe(x=> hitCollider.enabled = x).AddTo(_subs);
            hitCollider.enabled = core.IsActive.Value;

            core.Weapon.AttackRequested.Subscribe(() =>
            {
                animator.ResetTrigger(attack);
                animator.SetTrigger(attack);
            }).AddTo(_subs);

            updateProvider.OnLateUpdate.Subscribe(() => {
                if (isDead.Value)
                {
                    animator.SetBool(death, true);
                    return;
                }
                animator.SetFloat(speed, rigidbody.velocity.sqrMagnitude);

            }).AddTo(_subs);
        }

        public void Dispose()
        {
            _subs.Dispose();
        }
    }
}