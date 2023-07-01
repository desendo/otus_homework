using Common.Entities;
using Config;
using DependencyInjection;
using GameManager;
using Models.Components;
using Models.Declarative;
using Pool;
using UnityEngine;

namespace Models.Entities
{
    public class EnemyEntityMono : EntityMono, ISpawn
    {
        [SerializeField] private Transform _collisionTransform;
        [SerializeField] private Transform _rootTransform;
        [SerializeField] private Collider _hitCollider;
        [SerializeField] private Animator _anim;

        readonly EnemyModel _model = new EnemyModel();
        private GameConfig _gameConfig;
        private IUpdateProvider _updateProvider;
        private Rigidbody _rb;

        [Inject]
        public void Construct(GameConfig gameConfig, IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;
            _gameConfig = gameConfig;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            Add(new Component_Speed(_model.Core.Speed));
            Add(new Component_Death(_model.Core.LifeModel.OnDeath, this));
            Add(new Component_IsActive(_model.Core.IsActive));

            Add(new Component_Transform(_rootTransform));
            Add(new Component_Health(_model.Core.LifeModel.HitPoints, _model.Core.LifeModel.MaxHitPoints));
            Add(new Component_EnemyInstaller(_model.Core.Speed, _model.Core.LifeModel.MaxHitPoints));
            Add(new Component_ZombieHandsInstaller(_model.Core.Weapon));
            Add(new Component_Rigidbody(_rb));
        }

        public void OnSpawn()
        {
            _model.Construct(_rootTransform, _anim, _updateProvider,_rb, _hitCollider);
        }

        public void OnUnSpawn()
        {
            _model.Dispose();
        }
    }
}