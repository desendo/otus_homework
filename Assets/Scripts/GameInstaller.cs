using Config;
using DependencyInjection;
using ECS.Systems;
using Leopotam.EcsLite;
using UnityEngine;
using Views;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private SceneInfo _sceneInfo;



    private DependencyContainer _container;
    private EcsWorld _world;
    private EcsSystems _systems;

    private void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        _container = new DependencyContainer();

        _container.Add(_gameConfig);

        _container.Add(_sceneInfo);
        _container.Add(_systems);
        _container.Add(_world);


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