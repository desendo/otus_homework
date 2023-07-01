using System;
using System.Collections.Generic;
using Common;
using GameManager;
using UnityEngine;

namespace Models.Declarative
{
    public class EnemyModelVisual
    {
        public Transform RootTransform;

        private static readonly int death = Animator.StringToHash("Death");
        private static readonly int attack = Animator.StringToHash("Attack");
        private static readonly int speed = Animator.StringToHash("MoveSpeed");


        private Animator _animator;
        private readonly List<IDisposable> _subs = new List<IDisposable>();
        public Rigidbody Rigidbody { get; private set; }


        public void Construct(EnemyModelCore core, Transform rootTransform, Animator animator,
            IUpdateProvider updateProvider, Rigidbody rigidbody, Collider hitCollider)
        {
            _animator = animator;
            Rigidbody = rigidbody;
            RootTransform = rootTransform;
            var isDead = core.LifeModel.IsDead;

            core.IsActive.OnChanged.Subscribe(x=>hitCollider.enabled = x).AddTo(_subs);
            hitCollider.enabled = core.IsActive.Value;
            core.AttackModel.OnAttackStart.Subscribe(() =>
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
                _animator.SetFloat(speed, Rigidbody.velocity.sqrMagnitude);


            }).AddTo(_subs);
        }

        public void Dispose()
        {
            _subs.Dispose();
        }
    }
}