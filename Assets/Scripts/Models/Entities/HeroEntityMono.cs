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
        [SerializeField] private Rigidbody _rigidbody;
        private readonly HeroModel _heroModel = new HeroModel();
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
            _heroModel.Construct(transform, animator,  _updateProvider, _rigidbody);

            Add(new Component_ObservedMove(_heroModel.Core.MoveModel.OnMoveDir,
                _heroModel.Core.MoveModel.MoveRequested, _heroModel.Core.MoveModel.ResultVelocity));
            Add(new Component_Transform(_heroModel.Visual.RootTransform));
            Add(new Component_Rigidbody(_heroModel.Visual.Rigidbody));
            Add(new Component_Rotate(_heroModel.Core.MoveModel.RotationSpeed));
            Add(new Component_Attack(_heroModel.Core.AttackModel.Attack));
            Add(new Component_AttackPivot(_shootPoint));
            Add(new Component_Health(_heroModel.Core.LifeModel.HitPoints, _heroModel.Core.LifeModel.MaxHitPoints));
            Add(new Component_Shoot(_heroModel.Core.AttackModel.OnAttackStart));
            Add(new Component_IsReloading(_heroModel.Core.AttackModel.IsReloadStarted));
            Add(new Component_HeroInstaller(_heroModel.Core.MoveModel.Speed,
                _heroModel.Core.MoveModel.RotationSpeed, _heroModel.Core.LifeModel.MaxHitPoints));
        }

        private void OnDestroy()
        {
            _heroModel.Dispose();
        }
    }
}