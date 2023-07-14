using Common;
using Common.Entities;
using Config;
using DependencyInjection;
using GameManager;
using GameObjectsComponents;
using Models.Components;
using Models.Declarative;
using Pool;
using UnityEngine;

namespace Models.Entities
{
    public class EnemyEntityMono : EntityMono, ISpawn
    {
        [SerializeField] private EnemyModel _model;
        private IUpdateProvider _updateProvider;

        [Inject]
        public void Construct(GameConfig gameConfig, IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;
        }

        private void Awake()
        {
            Add(new Component_Speed(_model.Core.Speed, _model.Core.CurrentSpeedMultiplier));
            Add(new Component_Death(_model.Core.LifeModel.OnDeath, this));
            Add(new Component_IsActive(_model.Core.IsActive));
            Add(new Component_Attack(_model.Core.Weapon.OnAttack));
            Add(new Component_TakeDamage(_model.Core.LifeModel.OnTakeDamage));
            Add(new Component_Transform(_model.Visual.RootTransform));
            Add(new Component_Health(_model.Core.LifeModel.HitPoints, _model.Core.LifeModel.MaxHitPoints));
            Add(new Component_WeaponRange(_model.Core.Weapon.MaxRange));
            Add(new Component_Rigidbody(_model.Visual.RigidBody));

            Add(new Component_EnemyInstaller(_model.Core.Speed, _model.Core.LifeModel.MaxHitPoints, _model.Core.TargetSpeedMultiplierOnHits));
            Add(new Component_ZombieHandsInstaller(_model.Core.Weapon));
        }

        public void OnSpawn()
        {
            _model.Construct(_updateProvider);
        }
        public override void Dispose()
        {
            _model.Dispose();
        }
        public void OnUnSpawn()
        {
            Dispose();
        }
    }
}