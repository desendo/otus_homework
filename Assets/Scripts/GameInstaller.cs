using Config;
using DependencyInjection;
using ECS.Systems;
using Effects;
using Leopotam.EcsLite;
using Pool;
using UnityEngine;
using Views;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private SceneInfo _sceneInfo;

    [SerializeField] private UnitViewPool _unitViewPool;
    [SerializeField] private TeleportationEffectPool _teleportationEffectPool;
    [SerializeField] private ProjectilePool _projectilePool;
    [SerializeField] private HitEffectPool _hitEffectPool;
    [SerializeField] private ExplosionEffectPool _explosionEffectPool;

    private DependencyContainer _container;
    private EcsWorld _world;
    private EcsSystems _systems;

    private void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        _container = new DependencyContainer();

        _container.Add(_gameConfig);

        _container.Add(_unitViewPool);
        _container.Add(_teleportationEffectPool);
        _container.Add(_projectilePool);
        _container.Add(_hitEffectPool);
        _container.Add(_explosionEffectPool);

        _container.Add(_sceneInfo);
        _container.Add(_systems);
        _container.Add(_world);

        _container.Bind<EffectsService>();

        var attackSystem = new AttackSystem();
        var hitSystem = new HitSystem();
        var spawnSystem = new SpawnSystem();
        _container.Inject(attackSystem);
        _container.Inject(hitSystem);
        _container.Inject(spawnSystem);

        _systems
            .Add(new UpdateViewsSystem())
            .Add(attackSystem)
            .Add(new MoveSystem())
            .Add(new TimeToLiveSystem())
            .Add(hitSystem)
            .Add(spawnSystem)
            .Init();
    }

    private void Update()
    {
        _systems.Run();
    }
}