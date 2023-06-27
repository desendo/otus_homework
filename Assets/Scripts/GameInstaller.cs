using Config;
using Controllers;
using Controllers.HeroControllers;
using DependencyInjection;
using GameManager;
using GameState;
using Input;
using Managers;
using Pool;
using Services;
using UI.PresentationModel;
using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private VisualConfig _visualConfig;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private UpdateManager _updateManager;
    [SerializeField] private BulletPool _bulletPool;
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

        //pools
        _container.Add(_bulletPool);

        //services
        _container.Bind<InputService>();
        _container.Bind<HeroService>();

        //managers
        _container.Bind<BulletManager>();

        //controllers
        _container.Bind<HeroAttackController>();
        _container.Bind<MoveHeroController>();
        _container.Bind<HeroMoveEngine>();
        _container.Bind<CameraFollowController>();
        _container.Bind<RotateHeroController>();
        _container.Bind<RiffleShootController>();
        _container.Bind<ShotGunShootController>();
        _container.Bind<MachineGunController>();
        _container.Bind<SwitchWeaponController>();

        //pm
        _container.Bind<WeaponsListPresentationModel>();

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