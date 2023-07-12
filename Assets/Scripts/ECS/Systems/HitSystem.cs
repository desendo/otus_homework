using DependencyInjection;
using ECS.Components;
using Effects;
using Leopotam.EcsLite;

namespace ECS.Systems
{
    public class HitSystem : IEcsInitSystem, IEcsRunSystem
    {

        private EcsWorld _world;
        private EcsPool<CDamage> _cDamagePool;
        private EcsPool<CHealth> _cHealthPool;
        private EcsPool<CView> _cViewPool;
        private EcsPool<CTeam> _cTeamPool;
        private EcsPool<CPosition> _cPositionPool;
        private EcsPool<CHit> _cHitPool;

        private EffectsService _effectsService;
        private EcsFilter _hitFilter;

        [Inject]
        public void Construct(EffectsService effectsService)
        {
            _effectsService = effectsService;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _cViewPool = _world.GetPool<CView>();
            _cHitPool = _world.GetPool<CHit>();
            _cPositionPool = _world.GetPool<CPosition>();
            _cTeamPool = _world.GetPool<CTeam>();
            _cHealthPool = _world.GetPool<CHealth>();
            _cDamagePool = _world.GetPool<CDamage>();

            _hitFilter = _world.Filter<CPosition>().Inc<CHit>().End();

        }
        public void Run(IEcsSystems systems)
        {
            foreach (var hit in _hitFilter)
            {
                var cHit = _cHitPool.Get(hit);
                var cPos = _cPositionPool.Get(hit);

                if(!cHit.EntityFirst.Unpack(out _, out var first))
                    continue;
                if(!cHit.EntitySecond.Unpack(out _, out var other))
                    continue;

                if (_cTeamPool.Has(first) && _cTeamPool.Has(other) &&
                    _cTeamPool.Get(first).Team != _cTeamPool.Get(other).Team)
                {
                    if (_cDamagePool.Has(first) && _cHealthPool.Has(other))
                    {
                        var damage = _cDamagePool.Get(first);
                        ref var health = ref _cHealthPool.Get(other);
                        health.Health -= damage.Damage;

                        if (health.Health <= 0f)
                        {
                            if (_cViewPool.Has(other))
                            {
                                var cView = _cViewPool.Get(other);
                                var pos = cView.Transform.position;
                                _effectsService.ShowExplosionEffect(pos);
                            }


                            if (_cViewPool.Has(other))
                                _cViewPool.Get(other).View.Dispose();

                            _world.DelEntity(other);
                        }

                        if (_cViewPool.Has(first))
                            _cViewPool.Get(first).View.Dispose();
                        _world.DelEntity(first);
                        _effectsService.ShowHitEffect(cPos.Position, cPos.Direction);
                    }

                }
                _world.DelEntity(hit);
            }
        }
    }
}