using ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Systems
{
    public class MoveSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _moveFilter;
        private EcsWorld _world;
        private EcsPool<CPosition> _cPositionPool;
        private EcsPool<CMove> _cMovePool;
        private EcsPool<CEngine> _cEnginePool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _cPositionPool = _world.GetPool<CPosition>();
            _cMovePool = _world.GetPool<CMove>();
            _cEnginePool = _world.GetPool<CEngine>();
            _moveFilter = _world.Filter<CPosition>().Inc<CMove>().End();
        }
        public void Run(IEcsSystems systems)
        {
            foreach (var i in _moveFilter)
            {
                ref var pos = ref _cPositionPool.Get(i);
                ref var move = ref _cMovePool.Get(i);

                if (_cEnginePool.Has(i))
                {
                    ref var engine = ref _cEnginePool.Get(i);

                    if (move.TargetMoveSpeed > engine.MaxMoveSpeed)
                        move.TargetMoveSpeed = engine.MaxMoveSpeed;

                    if (move.CurrentMoveSpeed < move.TargetMoveSpeed)
                        move.CurrentMoveSpeed += engine.MoveAcceleration * Time.deltaTime;
                    else if (move.CurrentMoveSpeed > move.TargetMoveSpeed && move.CurrentMoveSpeed > 0)
                        move.CurrentMoveSpeed -= engine.MoveAcceleration * Time.deltaTime;
                }

                pos.Position += Time.deltaTime * move.CurrentMoveSpeed * pos.Direction;
            }
        }
    }
}