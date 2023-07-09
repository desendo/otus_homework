using Config;
using Controllers;
using DependencyInjection;
using ECS.Systems;
using Effects;
using GameManager;
using Leopotam.EcsLite;
using Pool;
using Services;
using UnityEngine;
using Views;

public class GameInstaller : MonoBehaviour
{

    [SerializeField] private VisualConfig _visualConfig;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private SceneInfo _sceneInfo;

    [SerializeField] private UnitViewPool _unitViewPool;
    [SerializeField] private TeleportationEffectPool _teleportationEffectPool;
    [SerializeField] private ProjectilePool _projectilePool;
    [SerializeField] private HitEffectPool _hitEffectPool;
    [SerializeField] private ExplosionEffectPool _explosionEffectPool;

    [SerializeField] private UpdateManager _updateManager;
    private DependencyContainer _container;
    private EcsWorld _world;
    private EcsSystems _systems;

    private void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);

        _container = new DependencyContainer();
        _container.Add(_gameConfig);
        _container.Add(_visualConfig);
        _container.Add(_unitViewPool);
        _container.Add(_teleportationEffectPool);
        _container.Add(_projectilePool);
        _container.Add(_hitEffectPool);
        _container.Add(_explosionEffectPool);
        _container.Add(_sceneInfo);

        _container.Add(_systems);
        _container.Add(_world);

        _container.Bind<UnitService>();
        _container.Bind<EffectsService>();

        _container.Bind<ProjectileManager>();

        var attackSystem = new AttackSystem();
        var hitSystem = new HitSystem();
        _container.Inject(attackSystem);
        _container.Inject(hitSystem);

        _systems
            .Add(new UpdateViewsSystem())
            .Add(attackSystem)
            .Add(new MoveSystem())
            .Add(new TimeToLiveSystem())
            .Add(hitSystem)
            .Init();

        _container.Bind<UnitSpawnController>();
        _container.Bind(_updateManager);
    }


    private void Update()
    {
        _systems.Run();
    }
}