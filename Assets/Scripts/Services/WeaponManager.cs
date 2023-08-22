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
using ReactiveExtension;

namespace Services
{
    public class WeaponManager
    {
        private readonly GameConfig _gameConfig;
        private readonly Inventory _inventory;
        private readonly IUpdateProvider _updateProvider;
        private IEntity _heroEntity;
        private IDisposable _reloadSubscribe;
        private IDisposable _shotSubscribe;

        public AtomicVariable<IWeapon> CurrentWeaponEntity { get; } = new AtomicVariable<IWeapon>();
        public Event<IWeapon> OnWeaponCollected { get; } = new Event<IWeapon>();
        public Event<IWeapon> OnWeaponRemoved { get; } = new Event<IWeapon>();
        public AtomicEvent OnWeaponsClear { get; } = new AtomicEvent();

        public readonly List<IWeapon> CollectedWeapons = new List<IWeapon>();

        public WeaponManager(GameConfig gameConfig, IUpdateProvider updateProvider, HeroSlotsService heroSlotsService,
            HeroService heroService)
        {
            _updateProvider = updateProvider;
            _gameConfig = gameConfig;
            _heroEntity = heroService.HeroEntity.Value;
            heroService.HeroEntity.OnChanged.Subscribe(entity => _heroEntity = entity);
            heroSlotsService.WeaponIdUnEquip.Subscribe(OnWeaponUnEquip);
            heroSlotsService.WeaponIdEquip.Subscribe(OnWeaponEquip);
            CurrentWeaponEntity.OnChanged.Subscribe(OnWeaponChange);

        }

        private void OnWeaponEquip(string weaponId)
        {
            var weapon = CollectedWeapons.FirstOrDefault(x => x.Id == weaponId);
            if (weapon == null)
            {
                AddWeaponFromId(weaponId);
                SetWeaponSelected(CollectedWeapons.Count - 1);
            }
        }

        private void OnWeaponUnEquip(string weaponId)
        {
            var weapon = CollectedWeapons.FirstOrDefault(x => x.Id == weaponId);
            if (weapon != null)
            {
                CollectedWeapons.Remove(weapon);
                OnWeaponRemoved.Invoke(weapon);
                if (CollectedWeapons.Count == 0)
                    CurrentWeaponEntity.Value = null;
                else
                    SetWeaponSelected(CollectedWeapons.Count - 1);
            }
        }

        private void AddWeaponFromId(string id)
        {
            var weaponConfig = _gameConfig.Weapons
                .FirstOrDefault(x => x.Id == id);

            var weapon = CreateWeapon(weaponConfig);
            CollectedWeapons.Add(weapon);
            OnWeaponCollected.Invoke(weapon);
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
    }
}