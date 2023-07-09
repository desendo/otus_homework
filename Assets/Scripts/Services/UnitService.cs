using Config;
using ECS.Components;
using Leopotam.EcsLite;
using Pool;
using UnityEngine;

namespace Services
{
    public class UnitService
    {
        private readonly EcsWorld _ecsWorld;
        private readonly EcsPool<CMove> _cMovePool;
        private readonly EcsPool<CEngine> _cEnginePool;
        private readonly EcsPool<CPosition> _cPositionPool;
        private readonly EcsPool<CView> _cViewPool;
        private readonly EcsPool<CHealth> _cHealthPool;
        private readonly EcsPool<CWeapon> _cWeaponPool;
        private readonly EcsPool<CTeam> _cTeamPool;
        private readonly UnitViewPool _unitViewPool;

        public UnitService(EcsWorld ecsWorld, UnitViewPool unitViewPool)
        {
            _unitViewPool = unitViewPool;
            _ecsWorld = ecsWorld;
            _cMovePool = _ecsWorld.GetPool<CMove>();
            _cViewPool = _ecsWorld.GetPool<CView>();
            _cEnginePool = _ecsWorld.GetPool<CEngine>();
            _cPositionPool = _ecsWorld.GetPool<CPosition>();
            _cHealthPool = _ecsWorld.GetPool<CHealth>();
            _cWeaponPool = _ecsWorld.GetPool<CWeapon>();
            _cTeamPool = _ecsWorld.GetPool<CTeam>();

        }

        public int CreateUnit(Vector3 position, Vector3 direction,
            EngineConfig engineConfig, WeaponConfig weaponConfig, int team, float health)
        {
            var entity = _ecsWorld.NewEntity();

            ref var cMove = ref _cMovePool.Add(entity);
            ref var cView = ref _cViewPool.Add(entity);
            ref var cEngine = ref _cEnginePool.Add(entity);
            ref var cPosition = ref _cPositionPool.Add(entity);
            ref var cHealth = ref _cHealthPool.Add(entity);
            ref var cWeapon = ref _cWeaponPool.Add(entity);
            ref var cTeam = ref _cTeamPool.Add(entity);

            cPosition.Direction = direction;
            cPosition.Position = position;

            var view = _unitViewPool.Spawn();

            cView.Transform = view.transform;
            cView.View = view;

            cView.Transform.forward = cPosition.Direction;
            cView.Transform.position = cPosition.Position;

            cMove.TargetDirection = cPosition.Direction;
            cMove.TargetMoveSpeed = engineConfig.MaxMoveSpeed;
            cMove.CurrentMoveSpeed = cMove.TargetMoveSpeed  * 0.5f;

            cEngine.MoveAcceleration = engineConfig.MoveAcceleration;
            cEngine.MaxMoveSpeed = engineConfig.MaxMoveSpeed;

            cHealth.Health = health;

            cWeapon.Damage = weaponConfig.Damage;
            cWeapon.Range = weaponConfig.Range;
            cWeapon.Delay = weaponConfig.Delay;
            cWeapon.Speed = weaponConfig.Speed;
            cWeapon.Angle = weaponConfig.Angle;
            cWeapon.TimeToLive = weaponConfig.TimeToLive;

            cTeam.Team = team;
            view.SetEntity(_ecsWorld.PackEntity(entity));
            view.SetTeam(team);
            view.SetDisposeCallback(()=>_unitViewPool.Unspawn(view));
            return entity;
        }
    }
}