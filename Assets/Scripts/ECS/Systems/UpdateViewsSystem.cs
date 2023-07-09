using ECS.Components;
using Leopotam.EcsLite;

namespace ECS.Systems
{
    public class UpdateViewsSystem : IEcsPostRunSystem, IEcsInitSystem
    {
        private EcsFilter _moveFilter;
        private EcsWorld _world;
        private EcsPool<CPosition> _cPositionPool;
        private EcsPool<CView> _cViewPool;
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _cPositionPool = _world.GetPool<CPosition>();
            _cViewPool = _world.GetPool<CView>();
            _moveFilter = _world.Filter<CPosition>().Inc<CView>().End();
        }
        public void PostRun(IEcsSystems systems)
        {
            foreach (var i in _moveFilter)
            {
                var pos = _cPositionPool.Get(i);
                var view = _cViewPool.Get(i);
                view.Transform.forward = pos.Direction;
                view.Transform.position = pos.Position;
            }
        }

    }
}