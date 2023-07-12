using Config;
using DependencyInjection;
using ECS.Components;
using Effects;
using Leopotam.EcsLite;
using Pool;
using UnityEngine;
using Views;

namespace ECS.Systems
{
    public class SpawnSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _world;

        private  EcsPool<CMove> _cMovePool;
        private  EcsPool<CEngine> _cEnginePool;
        private  EcsPool<CPosition> _cPositionPool;
        private  EcsPool<CView> _cViewPool;
        private  EcsPool<CHealth> _cHealthPool;
        private  EcsPool<CWeapon> _cWeaponPool;
        private  EcsPool<CTeam> _cTeamPool;
        private  UnitViewPool _unitViewPool;
        private SceneInfo _sceneInfo;
        private float _timer;
        private GameConfig _gameConfig;
        private EffectsService _effectsService;

        [Inject]
        public void Construct(UnitViewPool unitPool, SceneInfo sceneInfo, GameConfig config, EffectsService effectsService)
        {
            _effectsService = effectsService;
            _gameConfig = config;
            _sceneInfo = sceneInfo;
            _unitViewPool = unitPool;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _cMovePool = _world.GetPool<CMove>();
            _cViewPool = _world.GetPool<CView>();
            _cEnginePool = _world.GetPool<CEngine>();
            _cPositionPool = _world.GetPool<CPosition>();
            _cHealthPool = _world.GetPool<CHealth>();
            _cWeaponPool = _world.GetPool<CWeapon>();
            _cTeamPool = _world.GetPool<CTeam>();

        }
        public void Run(IEcsSystems systems)
        {
            _timer += Time.deltaTime;
            if (_timer > _gameConfig.SpawnInterval)
            {
                var isLeft = Random.value < 0.5f;
                var tr = isLeft ? _sceneInfo.Left : _sceneInfo.Right;
                var team = isLeft ? 1 : 0;
                SpawnBurst(tr, team);
                _timer = 0f;
            }
        }

        private void SpawnBurst(Transform tr, int team)
        {
            for (int i = 0; i < _gameConfig.SpawnBurstCount; i++)
            {
                var point = (Vector3) Random.insideUnitCircle * 10f + tr.position;
                var dir = tr.forward;
                _effectsService.ShowTeleportEffect(point, dir);
                CreateUnit(point, dir, _gameConfig.EngineConfig, _gameConfig.WeaponConfig, team, _gameConfig.Health);
            }
        }
        private void CreateUnit(Vector3 position, Vector3 direction,
            EngineConfig engineConfig, WeaponConfig weaponConfig, int team, float health)
        {
            var entity = _world.NewEntity();

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
            view.SetTeam(team);
            view.SetDisposeAction(()=>_unitViewPool.Unspawn(view));
            view.SetEntity(_world.PackEntityWithWorld(entity));
        }

    }
}