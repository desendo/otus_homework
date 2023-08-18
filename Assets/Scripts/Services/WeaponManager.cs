using System;
using System.Collections.Generic;
using System.Linq;
using Common.Atomic.Actions;
using Common.Atomic.Values;
using Common.Entities;
using Config;
using DependencyInjection;
using GameManager;
using ItemInventory;
using ItemInventory.Components;
using Models.Components;
using Models.Declarative.Weapons;
using Models.Entities;

namespace Services
{
    public class WeaponManager
    {
        private readonly DependencyContainer _dependencyContainer;
        private readonly GameConfig _gameConfig;
        private readonly HeroService _heroService;
        private readonly Inventory _inventory;
        private readonly IUpdateProvider _updateProvider;
        private readonly VisualConfig _visualConfig;
        private IEntity _heroEntity;
        private IDisposable _reloadSubscribe;
        private IDisposable _shotSubscribe;

        public AtomicVariable<IWeapon> CurrentWeaponEntity { get; } = new AtomicVariable<IWeapon>();
        public AtomicEvent<IWeapon> OnWeaponCollected { get; } = new AtomicEvent<IWeapon>();
        public AtomicEvent<IWeapon> OnWeaponRemoved { get; } = new AtomicEvent<IWeapon>();
        public AtomicEvent OnWeaponsClear { get; } = new AtomicEvent();
        public readonly List<IWeapon> CollectedWeapons = new List<IWeapon>();

        public WeaponManager(GameConfig gameConfig, VisualConfig visualConfig,
            DependencyContainer dependencyContainer, IUpdateProvider updateProvider, Inventory inventory,
            HeroService heroService)
        {
            _heroService = heroService;
            _updateProvider = updateProvider;
            _dependencyContainer = dependencyContainer;
            _gameConfig = gameConfig;
            _visualConfig = visualConfig;
            _inventory = inventory;
            _heroEntity = _heroService.HeroEntity.Value;
            _heroService.HeroEntity.OnChanged.Subscribe(entity => _heroEntity = entity);
            _inventory.OnAdd.Subscribe(OnItemAdd);
            _inventory.OnRemove.Subscribe(OnItemRemove);
            CurrentWeaponEntity.OnChanged.Subscribe(OnWeaponChange);
        }


        private void OnItemRemove(Item obj)
        {
            var weaponComponent = obj.Get<ItemComponent_Weapon>();
            if (weaponComponent != null)
            {
                var weapon = CollectedWeapons.FirstOrDefault(x => x.Id == weaponComponent.WeaponId);
                CollectedWeapons.Remove(weapon);
                OnWeaponRemoved.Invoke(weapon);
            }

            if (CollectedWeapons.Count == 0)
                CurrentWeaponEntity.Value = null;
        }

        private void OnItemAdd(Item obj)
        {
            var weaponComponent = obj.Get<ItemComponent_Weapon>();
            if (weaponComponent != null)
            {
                var weaponConfig = _gameConfig.Weapons
                    .FirstOrDefault(x => x.Id == weaponComponent.WeaponId);

                var weapon = CreateWeapon(weaponConfig);
                CollectedWeapons.Add(weapon);
                OnWeaponCollected.Invoke(weapon);
            }
        }


        private void OnWeaponChange(IWeapon weapon)
        {
            if (weapon == null || _heroEntity == null)
                return;

            _reloadSubscribe?.Dispose();
            _reloadSubscribe = weapon.Get<Component_Reload>()
                .ReloadStart.Subscribe(_heroEntity.Get<Component_IsReloading>().SetReloadingTime);

            _shotSubscribe?.Dispose();
            _shotSubscribe = weapon.Get<Component_OnAttack>()
                .OnAttack.Subscribe(() => { _heroEntity.Get<Component_OnAttack>().OnAttack.Invoke(); });

            foreach (var collectedWeapon in CollectedWeapons)
                collectedWeapon.Get<Component_SetActive>().SetActive(false);
            weapon.Get<Component_SetActive>().SetActive(true);
        }

        private IWeapon CreateWeapon(PlayerWeapon weaponConfig)
        {
            if (weaponConfig != null)
            {
                if (weaponConfig.Type == WeaponType.Riffle)
                {
                    var attackPoint = _heroEntity.Get<Component_AttackPivot>().AttackPoint;
                    var weaponEntity =
                        new RiffleEntity(_updateProvider, attackPoint, weaponConfig.Type, weaponConfig.Id);
                    var weaponModel = new RiffleModelCore();
                    weaponEntity.BindModel(weaponModel);
                    weaponEntity.Get<IComponent_WeaponInstaller>().Setup(weaponConfig.Parameters);
                    return weaponEntity;
                }

                if (weaponConfig.Type == WeaponType.Shotgun)
                {
                    var attackPoint = _heroEntity.Get<Component_AttackPivot>().AttackPoint;
                    var weaponEntity =
                        new ShotGunEntity(_updateProvider, attackPoint, weaponConfig.Type, weaponConfig.Id);
                    var weaponModel = new ShotgunModelCore();
                    weaponEntity.BindModel(weaponModel);
                    weaponEntity.Get<IComponent_WeaponInstaller>().Setup(weaponConfig.Parameters);
                    return weaponEntity;
                }

                if (weaponConfig.Type == WeaponType.MachineGun)
                {
                    var attackPoint = _heroEntity.Get<Component_AttackPivot>().AttackPoint;
                    var weaponEntity =
                        new MachineGunEntity(_updateProvider, attackPoint, weaponConfig.Type, weaponConfig.Id);
                    var weaponModel = new MachineGunModel();
                    weaponEntity.BindModel(weaponModel);
                    weaponEntity.Get<IComponent_WeaponInstaller>().Setup(weaponConfig.Parameters);
                    return weaponEntity;
                }
            }

            throw new Exception("no config " + weaponConfig?.Type);
        }



        public void SetWeaponSelected(in int i)
        {
            CurrentWeaponEntity.Value = CollectedWeapons[i];
        }

        /*
        public void OnStartGame()
        {
            throw new NotImplementedException();
        }

        public void OnFinishGame(bool gameWin)
        {
            foreach (var collectedWeapon in CollectedWeapons) collectedWeapon.Dispose();
            CollectedWeapons.Clear();
            OnWeaponsClear.Invoke();
        }*/
    }
}