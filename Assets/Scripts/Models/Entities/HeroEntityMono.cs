using Common.Entities;
using DependencyInjection;
using GameManager;
using Models.Components;
using Models.Declarative;
using UnityEngine;

namespace Models.Entities
{
    public class HeroEntityMono : EntityMono
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private HeroModel _heroModel;
        private IUpdateProvider _updateProvider;

        [Inject]
        public void Construct(IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;
        }

        private void FixedUpdate()
        {
            _shootPoint.forward = transform.forward;
        }

        private void Awake()
        {
            var animator = GetComponentInChildren<Animator>();
            _heroModel.Construct(_updateProvider);

            Add(new Component_ObservedMove(_heroModel.Core.MoveModel.OnMoveDir,
                _heroModel.Core.MoveModel.MoveRequested, _heroModel.Core.MoveModel.ResultVelocity));
            Add(new Component_Transform(_heroModel.Visual.RootTransform));
            Add(new Component_Rigidbody(_heroModel.Visual.Rigidbody));
            Add(new Component_Rotate(_heroModel.Core.MoveModel.RotationSpeed));
            Add(new Component_Attack(_heroModel.Core.AttackModel.Attack));
            Add(new Component_AttackPivot(_shootPoint));
            Add(new Component_Health(_heroModel.Core.Life.HitPoints, _heroModel.Core.Life.MaxHitPoints));
            Add(new Component_Death(_heroModel.Core.Life.OnDeath, this));
            Add(new Component_TakeDamage(_heroModel.Core.Life.OnTakeDamage));
            Add(new Component_OnAttack(_heroModel.Core.AttackModel.OnAttackStart));
            Add(new Component_IsReloading(_heroModel.Core.AttackModel.OnReloadStarted));
            Add(new Component_HeroInstaller(_heroModel.Core.MoveModel.Speed,
                _heroModel.Core.MoveModel.RotationSpeed, _heroModel.Core.Life.MaxHitPoints));
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public override void Dispose()
        {
            _heroModel.Dispose();
        }
    }
}