using System;
using System.Collections.Generic;
using Common;
using Common.Entities;
using GameManager;
using GameObjectsComponents;
using UnityEngine;

namespace Models.Declarative
{
    [Serializable]
    public class EnemyModelVisual
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationEventListener _animationEventListener;
        [SerializeField] private CollisionSensor _weaponCollisionSensor;
        [SerializeField] private Collider _hitCollider;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _rootTransform;

        public Rigidbody RigidBody => _rigidbody;
        public Transform RootTransform => _rootTransform;

        private static readonly int death = Animator.StringToHash("Death");
        private static readonly int attack = Animator.StringToHash("Attack");
        private static readonly int speed = Animator.StringToHash("MoveSpeed");


        private readonly List<IDisposable> _subs = new List<IDisposable>();


        public void Construct(EnemyModelCore core, IUpdateProvider updateProvider)
        {
            var isDead = core.LifeModel.IsDead;
            _animator.ResetTrigger(death);

            _weaponCollisionSensor.gameObject.SetActive(false);
            _animationEventListener.OnEvent.Subscribe(obj =>
            {
                if(obj == "enable_weapon_collider")
                    _weaponCollisionSensor.gameObject.SetActive(true);
                if(obj == "disable_weapon_collider")
                    _weaponCollisionSensor.gameObject.SetActive(false);

            }).AddTo(_subs);

            _weaponCollisionSensor.Collision.Subscribe(x =>
            {
                if (x.body.TryGetComponent<IEntity>(out var entity))
                {
                    _weaponCollisionSensor.gameObject.SetActive(false);
                    core.Weapon.DoHit(entity);
                }
            }).AddTo(_subs);

            core.IsActive.OnChanged.Subscribe(x=> _hitCollider.enabled = x).AddTo(_subs);
            _hitCollider.enabled = core.IsActive.Value;

            core.Weapon.AttackRequested.Subscribe(() =>
            {
                _animator.ResetTrigger(attack);
                _animator.SetTrigger(attack);
            }).AddTo(_subs);

            updateProvider.OnLateUpdate.Subscribe(() => {
                if (isDead.Value)
                {
                    _animator.SetBool(death, true);
                    return;
                }
                _animator.SetFloat(speed, _rigidbody.velocity.sqrMagnitude);

            }).AddTo(_subs);
        }

        public void Dispose()
        {
            _subs.Dispose();
        }
    }
}