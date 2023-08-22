using Config;
using Controllers;
using Controllers.HeroControllers;
using Controllers.WeaponControllers;
using DependencyInjection;
using Effects;
using GameManager;
using Input;
using ItemInventory;
using ItemInventory.Controllers;
using ItemInventory.UI;
using ItemInventory.UI.PresentationModel;
using Managers;
using Pool;
using Services;
using Services.Effects;
using Signals;
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
    [SerializeField] private InventoryItemViewPool _inventoryItemViewPool;
    [SerializeField] private LevelBounds _levelBounds;
    [SerializeField] private InventoryInstaller _inventoryInstaller;
    private DependencyContainer _container;

    private void Start()
    {
        Bind();
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
        _container.Add(_inventoryItemViewPool);

        //services
        _container.Bind<InputService>();
        _container.Bind<EnemyService>();
        _container.Bind<EffectsService>();
        _container.Bind<SignalBusService>();
        _container.Bind<Inventory>();
        _container.Bind<HeroService>();
        _container.Bind<HeroSlotsService>();

        //managers
        _container.Bind<BulletManager>();
        _container.Bind<WeaponManager>();
        _container.Bind<WeaponEffectManager>();

        _container.Bind<EffectManager>();

        //controllers
            //game
        _container.Bind<GameWinController>();
            //camera
        _container.Bind<CameraFollowController>();
        _container.Bind<CameraAngleController>();
            //enemy
        _container.Bind<EnemyManager>();
        _container.Bind<EnemyDeathCountController>();
        _container.Bind<EnemyMoveController>();
        _container.Bind<EnemyAttackController>();

            //hero
        _container.Bind<HeroAttackController>();
        _container.Bind<MoveHeroController>();
        _container.Bind<HeroMoveEngine>();
        _container.Bind<RotateHeroController>();
        _container.Bind<RiffleShootController>();
        _container.Bind<ShotGunShootController>();
        _container.Bind<MachineGunController>();
        _container.Bind<SwitchWeaponController>();
        _container.Bind<HeroDeathController>();


        //item effects
        //pm
        _container.Bind<WeaponsListPresentationModel>();
        _container.Bind<HeroInfoPresentationModel>();
        _container.Bind<KillsPresentationModel>();
        _container.Bind<InventoryPresentationModel>();
        _container.Bind<HeroItemSlotsPresentationModel>();
        
        _container.Bind<ItemSetToSlotController>();
        _container.Bind<SlotItemEffectController>();
        //installers
        _container.Bind<HeroSlotsInstaller>();
        _container.Bind(_inventoryInstaller);
        //state and update managers
        _container.Bind<GameStateManager>();
        _container.Inject(_updateManager);

        SearchAndInject();


    }
    private void SearchAndInject()
    {
        var objects = FindObjectsOfType<MonoBehaviour>(true);
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