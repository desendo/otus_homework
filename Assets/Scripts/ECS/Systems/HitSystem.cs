using DependencyInjection;
using ECS.Components;
using Effects;
using Leopotam.EcsLite;
using Services;

namespace ECS.Systems
{
    public class HitSystem : IEcsInitSystem,IEcsDestroySystem
    {

        private EcsWorld _world;
        private EcsPool<CDamage> _cDamagePool;
        private EcsPool<CHealth> _cHealthPool;
        private EcsPool<CView> _cViewPool;
        private EcsPool<CTeam> _cTeamPool;

        private ProjectileManager _projectileManager;
        private EffectsService _effectsService;

        [Inject]
        public void Construct(ProjectileManager projectileManager, EffectsService effectsService)
        {
            _effectsService = effectsService;
            _projectileManager = projectileManager;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _cViewPool = _world.GetPool<CView>();
            _cTeamPool = _world.GetPool<CTeam>();
            _cHealthPool = _world.GetPool<CHealth>();
            _cDamagePool = _world.GetPool<CDamage>();


            _projectileManager.OnProjectileCollision += OnProjectileCollision;
        }

        private void OnProjectileCollision(int damageGiver, int damageAcceptor)
        {
            if (_cDamagePool.Has(damageGiver) && _cHealthPool.Has(damageAcceptor))
            {
                var damage = _cDamagePool.Get(damageGiver);
                ref var health = ref _cHealthPool.Get(damageAcceptor);
                health.Health -= damage.Damage;

                if (health.Health <= 0f)
                {
                    if (_cViewPool.Has(damageAcceptor))
                    {
                        var cView = _cViewPool.Get(damageAcceptor);
                        var pos = cView.Transform.position;
                        _effectsService.ShowExplosionEffect(pos);
                        cView.View.Dispose();
                    }
                    _world.DelEntity(damageAcceptor);

                }
            }
        }


        public void Destroy(IEcsSystems systems)
        {
            _projectileManager.OnProjectileCollision -= OnProjectileCollision;
        }
    }
}