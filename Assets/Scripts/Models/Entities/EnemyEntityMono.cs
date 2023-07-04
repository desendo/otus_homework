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
        [SerializeField] private Transform _rootTransform;
        [SerializeField] private Collider _hitCollider;
        [SerializeField] private CollisionSensor _weaponCollisionSensor;
        [SerializeField] private Animator _anim;
        [SerializeField] private AnimationEventListener _eventListener;

        private readonly EnemyModel _model = new EnemyModel();
        private IUpdateProvider _updateProvider;
        private Rigidbody _rb;

        [Inject]
        public void Construct(GameConfig gameConfig, IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            Add(new Component_Speed(_model.Core.Speed, _model.Core.CurrentSpeedMultiplier));
            Add(new Component_Death(_model.Core.LifeModel.OnDeath, this));
            Add(new Component_IsActive(_model.Core.IsActive));
            Add(new Component_Attack(_model.Core.Weapon.OnAttack));
            Add(new Component_TakeDamage(_model.Core.LifeModel.OnTakeDamage));
            Add(new Component_Transform(_rootTransform));
            Add(new Component_Health(_model.Core.LifeModel.HitPoints, _model.Core.LifeModel.MaxHitPoints));
            Add(new Component_WeaponRange(_model.Core.Weapon.MaxRange));
            Add(new Component_Rigidbody(_rb));

            Add(new Component_EnemyInstaller(_model.Core.Speed, _model.Core.LifeModel.MaxHitPoints, _model.Core.TargetSpeedMultiplierOnHits));
            Add(new Component_ZombieHandsInstaller(_model.Core.Weapon));
        }

        public void OnSpawn()
        {
            _model.Construct(_rootTransform, _anim, _updateProvider,_rb, _hitCollider, _eventListener, _weaponCollisionSensor);
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