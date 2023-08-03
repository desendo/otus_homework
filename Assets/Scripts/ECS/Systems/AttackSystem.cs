using DependencyInjection;
using ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Views;

namespace ECS.Systems
{
    public class AttackSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _possibleTargetFilter;
        private EcsFilter _targetSearcherFilter;

        private EcsWorld _world;
        private EcsPool<CPosition> _cPositionPool;
        private EcsPool<CMove> _cMovePool;
        private EcsPool<CTimeToLive> _cTimeToLivePool;
        private EcsPool<CTeam> _cTeamPool;
        private EcsPool<CWeapon> _cWeaponPool;
        private EcsPool<CView> _cViewPool;
        private EcsPool<CDamage> _cDamagePool;
        private ProjectileView _prefab;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _cTimeToLivePool = _world.GetPool<CTimeToLive>();
            _cViewPool = _world.GetPool<CView>();
            _cPositionPool = _world.GetPool<CPosition>();
            _cMovePool = _world.GetPool<CMove>();
            _cDamagePool = _world.GetPool<CDamage>();
            _cTeamPool = _world.GetPool<CTeam>();
            _cWeaponPool = _world.GetPool<CWeapon>();
            _possibleTargetFilter = _world.Filter<CTeam>().Inc<CHealth>().Inc<CPosition>().End();
            _targetSearcherFilter = _world.Filter<CWeapon>().Inc<CMove>().Inc<CPosition>().Exc<CTarget>().End();
            _prefab = Resources.Load<ProjectileView>("Projectile");
        }
        public void Run(IEcsSystems systems)
        {
            foreach (var i in _targetSearcherFilter)
            {
                ref var pos = ref _cPositionPool.Get(i);
                ref var move = ref _cMovePool.Get(i);
                ref var weapon = ref _cWeaponPool.Get(i);
                ref var team = ref _cTeamPool.Get(i);
                weapon.Timer -= Time.deltaTime;
                if(weapon.Timer > 0)
                    continue;

                foreach (var j in _possibleTargetFilter)
                {
                    if(j == i)
                        continue;

                    ref var team2 = ref _cTeamPool.Get(j);
                    if(team2.Team == team.Team)
                        continue;

                    ref var pos2 = ref _cPositionPool.Get(j);

                    var range = _cWeaponPool.Get(i).Range;
                    range *= range;


                    var delta = pos2.Position - pos.Position;
                    if (delta.sqrMagnitude < range)
                    {
                        var attackAngle = Vector3.Angle(delta, pos.Direction);
                        if(attackAngle > weapon.Angle)
                            continue;

                        weapon.Timer = weapon.Delay;
                        CreateProjectile( pos.Position, delta, team.Team,
                            weapon.Damage, weapon.Speed, weapon.TimeToLive);

                        break;
                    }
                }
            }
        }

        private void CreateProjectile(Vector3 position, Vector3 direction, int team, float damage, float speed, float ttl)
        {
            var entity = _world.NewEntity();


            ref var cMove = ref _cMovePool.Add(entity);
            ref var cView = ref _cViewPool.Add(entity);
            ref var cPosition = ref _cPositionPool.Add(entity);
            ref var cTeam = ref _cTeamPool.Add(entity);
            ref var cDamage = ref _cDamagePool.Add(entity);
            ref var cTimeToLive = ref _cTimeToLivePool.Add(entity);

            cPosition.Direction = direction;
            cPosition.Position = position;

            var view = GameObject.Instantiate(_prefab);

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
            view.SetEntity(_world.PackEntityWithWorld(entity));
            view.SetDisposeAction(()=>Object.Destroy(view.gameObject));
            view.SetTeam(team);
        }
    }
}