using DependencyInjection;
using ECS.Components;
using Effects;
using Leopotam.EcsLite;
using Services;
using UnityEngine;

namespace ECS.Systems
{
    public class AttackSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _possibleTargetFilter;
        private EcsFilter _targetSearcherFilter;

        private EcsWorld _world;
        private EcsPool<CPosition> _cPositionPool;
        private EcsPool<CMove> _cMovePool;
        private EcsPool<CTeam> _cTeamPool;
        private EcsPool<CWeapon> _cWeaponPool;

        private ProjectileManager _projectileManager;

        [Inject]
        public void Construct(ProjectileManager projectileManager)
        {
            _projectileManager = projectileManager;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _cPositionPool = _world.GetPool<CPosition>();
            _cMovePool = _world.GetPool<CMove>();
            _cTeamPool = _world.GetPool<CTeam>();
            _cWeaponPool = _world.GetPool<CWeapon>();
            _possibleTargetFilter = _world.Filter<CTeam>().Inc<CHealth>().Inc<CPosition>().End();
            _targetSearcherFilter = _world.Filter<CWeapon>().Inc<CMove>().Inc<CPosition>().Exc<CTarget>().End();
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
                        _projectileManager.CreateProjectile(pos.Position, delta, team.Team,
                            weapon.Damage, weapon.Speed, weapon.TimeToLive);
                        break;
                    }
                }
            }
        }

    }
}