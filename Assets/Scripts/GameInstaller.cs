using Config;
using Controllers;
using Controllers.HeroControllers;
using Controllers.WeaponControllers;
using DependencyInjection;
using Effects;
using GameManager;
using GameState;
using Input;
using Managers;
using Pool;
using Services;
using UI.PresentationModel;
using UnityEngine;
using View;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private VisualConfig _visualConfig;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private UpdateManager _updateManager;
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private HitEffectPool _hitEffectPool;
    [SerializeField] private LevelBounds _levelBounds;
    private DependencyContainer _container;

    private void Start()
    {
        Bind();
        StartGame();
    }

    private void StartGame()
    {
        _container.Get<GameStateManager>().State.Value = LevelState.None;
        var spawners = _container.GetList<ISpawner>();
        foreach (var spawner in spawners)
        {
            spawner.Spawn();
        }

        _container.Get<GameStateManager>().GameLoaded.Value = true;
        _container.Get<GameStateManager>().State.Value = LevelState.Started;
    }

    private void Bind()
    {
        //infrastructure
        _container = new DependencyContainer();
        _container.Add(_container);
        _container.Add(_visualConfig);
        _container.Add(_gameConfig);
        _container.Add<UpdateProvider>();

        //scene
        _container.Add(_levelBounds);
        //pools
        _container.Add(_bulletPool);
        _container.Add(_enemyPool);
        _container.Add(_hitEffectPool);

        //services
        _container.Bind<InputService>();
        _container.Bind<HeroService>();
        _container.Bind<EnemyService>();
        _container.Bind<EffectsService>();

        //managers
        _container.Bind<BulletManager>();

        //controllers

            //camera
        _container.Bind<CameraFollowController>();
        _container.Bind<CameraAngleController>();
            //enemy
        _container.Bind<EnemySpawnController>();
        _container.Bind<EnemyDeathController>();
        _container.Bind<EnemyMoveController>();

            //hero
        _container.Bind<HeroAttackController>();
        _container.Bind<MoveHeroController>();
        _container.Bind<HeroMoveEngine>();
        _container.Bind<RotateHeroController>();
        _container.Bind<RiffleShootController>();
        _container.Bind<ShotGunShootController>();
        _container.Bind<MachineGunController>();
        _container.Bind<SwitchWeaponController>();

        //pm
        _container.Bind<WeaponsListPresentationModel>();
        _container.Bind<HeroInfoPresentationModel>();
        _container.Bind<KillsPresentationModel>();

        //state and update managers
        _container.Bind<GameStateManager>();
        _container.Inject(_updateManager);
        SearchAndInject();

    }
    private void SearchAndInject()
    {
        var objects = FindObjectsOfType<MonoBehaviour>();
        foreach (var obj in objects)
        {
            var monoBehaviours = obj.GetComponents<MonoBehaviour>();
            if (monoBehaviours != null)
            {
                foreach (var monoBehaviour in monoBehaviours)
                {
                    _container.Inject(monoBehaviour);
                }
            }
        }
    }
}