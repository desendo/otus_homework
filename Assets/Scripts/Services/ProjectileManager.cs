using System;
using ECS.Components;
using Effects;
using Leopotam.EcsLite;
using Pool;
using UnityEngine;
using Views;

namespace Services
{
    public class ProjectileManager
    {
        private readonly EcsWorld _ecsWorld;
        private readonly EffectsService _effectsService;
        private readonly EcsPool<CMove> _cMovePool;
        private readonly EcsPool<CPosition> _cPositionPool;
        private readonly EcsPool<CView> _cViewPool;
        private readonly EcsPool<CTeam> _cTeamPool;
        private readonly EcsPool<CDamage> _cDamagePool;
        private readonly EcsPool<CTimeToLive> _cTimeToLivePool;
        private readonly ProjectilePool _projectilePool;

        public event Action<int,int> OnProjectileCollision;

        public ProjectileManager(EcsWorld ecsWorld, EffectsService effectsService, ProjectilePool projectilePool)
        {
            _projectilePool = projectilePool;
            _ecsWorld = ecsWorld;
            _effectsService = effectsService;
            _cMovePool = _ecsWorld.GetPool<CMove>();
            _cViewPool = _ecsWorld.GetPool<CView>();
            _cPositionPool = _ecsWorld.GetPool<CPosition>();
            _cTeamPool = _ecsWorld.GetPool<CTeam>();
            _cDamagePool = _ecsWorld.GetPool<CDamage>();
            _cTimeToLivePool = _ecsWorld.GetPool<CTimeToLive>();


        }

        public int CreateProjectile(Vector3 position, Vector3 direction, int team, float damage,
            float speed, float ttl)
        {
            var entity = _ecsWorld.NewEntity();

            ref var cMove = ref _cMovePool.Add(entity);
            ref var cView = ref _cViewPool.Add(entity);
            ref var cPosition = ref _cPositionPool.Add(entity);
            ref var cTeam = ref _cTeamPool.Add(entity);
            ref var cDamage = ref _cDamagePool.Add(entity);
            ref var cTimeToLive = ref _cTimeToLivePool.Add(entity);

            cPosition.Direction = direction;
            cPosition.Position = position;

            var view = _projectilePool.Spawn();

            cView.Transform = view.transform;
            cView.View = view;

            cView.Transform.forward = cPosition.Direction;
            cView.Transform.position = cPosition.Position;

            cMove.TargetDirection = cPosition.Direction;
            cMove.CurrentMoveSpeed = speed;
            cMove.TargetMoveSpeed = speed;

            cDamage.Damage = damage;
            cTimeToLive.Current = ttl;

            cTeam.Team = team;
            view.SetEntity(_ecsWorld.PackEntity(entity));
            view.SetTeam(team);
            view.OnEntityCollision+= ViewOnOnEntityCollision;
            view.SetDisposeCallback(() =>
            {
                view.OnEntityCollision -= ViewOnOnEntityCollision;
                _projectilePool.Unspawn(view);
            });
            return entity;
        }

        private void ViewOnOnEntityCollision(EntityMono selfMono, Collision collision)
        {
            var self = selfMono.PackedEntity;
            _effectsService.ShowHitEffect(collision);
            if (self.Unpack(_ecsWorld, out var selfUnpacked))
            {
                if (collision.gameObject.TryGetComponent<EntityMono>(out var otherEntityMono))
                {
                    var other = otherEntityMono.PackedEntity;

                    if (other.Unpack(_ecsWorld, out var otherUnpacked))
                    {
                        OnProjectileCollision?.Invoke(selfUnpacked, otherUnpacked);
                    }
                }
                selfMono.Dispose();
                _ecsWorld.DelEntity(selfUnpacked);
            }
        }
    }
}