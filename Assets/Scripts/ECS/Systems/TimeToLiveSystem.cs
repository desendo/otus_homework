using System.Collections.Generic;
using ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Systems
{
    public class TimeToLiveSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _ttlFilter;
        private EcsWorld _world;
        private EcsPool<CTimeToLive> _cTTLPool;
        private EcsPool<CView> _cViewPool;
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _cTTLPool = _world.GetPool<CTimeToLive>();
            _cViewPool = _world.GetPool<CView>();
            _ttlFilter = _world.Filter<CTimeToLive>().Inc<CView>().End();
        }
        public void Run(IEcsSystems systems)
        {
            List<int> destroyBuffer = new List<int>();
            foreach (var i in _ttlFilter)
            {
                ref var ttl = ref _cTTLPool.Get(i);
                ref var view = ref _cViewPool.Get(i);
                ttl.Current -= Time.deltaTime;
                if (ttl.Current < 0f)
                {
                    destroyBuffer.Add(i);
                    view.View.Dispose();
                }
            }
            foreach (var i in destroyBuffer)
            {
                _world.DelEntity(i);
            }
        }

    }
}