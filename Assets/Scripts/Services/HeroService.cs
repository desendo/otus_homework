using System;
using System.Collections.Generic;
using System.Linq;
using Common.Atomic.Actions;
using Common.Atomic.Values;
using Common.Entities;
using Config;
using DependencyInjection;
using DependencyInjection.Util;
using GameManager;
using Models.Components;
using Models.Entities;

namespace Services
{
    public class HeroService : ISpawner
    {
        private readonly GameConfig _gameConfig;
        private readonly VisualConfig _visualConfig;
        private readonly DependencyContainer _dependencyContainer;
        public AtomicVariable<EntityMono> HeroEntity { get; } = new AtomicVariable<EntityMono>();
        public AtomicVariable<IWeapon> CurrentWeaponEntity { get; private set; } = new AtomicVariable<IWeapon>();
        public AtomicEvent<IWeapon> OnWeaponCollected { get; private set; } = new AtomicEvent<IWeapon>();
        public AtomicEvent<IWeapon> OnWeaponDropped { get; private set; } = new AtomicEvent<IWeapon>();

        public readonly List<IWeapon> CollectedWeapons = new List<IWeapon>();
        private readonly IUpdateProvider _updateProvider;
        private IDisposable _reloadSubscribe;
        private IDisposable _shotSubscribe;

        public HeroService(GameConfig gameConfig, VisualConfig visualConfig,
            DependencyContainer dependencyContainer, IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;
            _dependencyContainer = dependencyContainer;
            _gameConfig = gameConfig;
            _visualConfig = visualConfig;
            CurrentWeaponEntity.OnChanged.Subscribe(OnWeaponChange);
        }

        public void Spawn()
        {
            var prefab = _visualConfig.PlayerPrefab;
            HeroEntity.Value = _dependencyContainer.InstantiateInject(prefab);
            HeroEntity.Value.Get<Component_HeroInstaller>().Setup(_gameConfig);

            var riffleConfig = _gameConfig.Weapons
                .FirstOrDefault(x => x.Type == WeaponType.Riffle);

            var riffleWeapon = CreateWeapon(riffleConfig);
            CollectedWeapons.Add(riffleWeapon);
            OnWeaponCollected.Invoke(riffleWeapon);

            var shotGun = _gameConfig.Weapons
                .FirstOrDefault(x => x.Type == WeaponType.Shotgun);

            var shotGunWeapon = CreateWeapon(shotGun);
            CollectedWeapons.Add(shotGunWeapon);
            OnWeaponCollected.Invoke(shotGunWeapon);

            var machineGun = _gameConfig.Weapons
                .FirstOrDefault(x => x.Type == WeaponType.MachineGun);

            var machineGunWeapon = CreateWeapon(machineGun);
            CollectedWeapons.Add(machineGunWeapon);
            OnWeaponCollected.Invoke(machineGunWeapon);

            CurrentWeaponEntity.Value = riffleWeapon;
            //OnWeaponChange()
        }

        private void OnWeaponChange(IWeapon weapon)
        {
            if(weapon == null || HeroEntity == null)
                return;

            _reloadSubscribe?.Dispose();
            _reloadSubscribe = weapon.Get<Component_Reload>()
                .ReloadStart.Subscribe(HeroEntity.Value.Get<Component_IsReloading>().SetReloadingTime);

            _shotSubscribe?.Dispose();
            _shotSubscribe = weapon.Get<Component_Shoot>()
                .OnShot.Subscribe(() =>
                {
                    HeroEntity.Value.Get<Component_Shoot>().OnShot.Invoke();
                });

            foreach (var collectedWeapon in CollectedWeapons)
            {
                collectedWeapon.Get<Component_SetActive>().SetActive(false);
            }
            weapon.Get<Component_SetActive>().SetActive(true);
        }

        private IWeapon CreateWeapon(PlayerWeapon weaponConfig)
        {
            if (weaponConfig != null)
            {
                if (weaponConfig.Type == WeaponType.Riffle)
                {
                    var attackPoint = HeroEntity.Value.Get<Component_AttackPivot>().AttackPoint;
                    var weapon = new RiffleEntity(_updateProvider, attackPoint, weaponConfig.Type);
                    weapon.Get<IComponent_WeaponInstaller>().Setup(weaponConfig.Parameters);
                    return weapon;
                }

                if (weaponConfig.Type == WeaponType.Shotgun)
                {
                    var attackPoint = HeroEntity.Value.Get<Component_AttackPivot>().AttackPoint;
                    var weapon = new ShotGunEntity(_updateProvider, attackPoint, weaponConfig.Type);
                    weapon.Get<IComponent_WeaponInstaller>().Setup(weaponConfig.Parameters);
                    return weapon;
                }

                if (weaponConfig.Type == WeaponType.MachineGun)
                {
                    var attackPoint = HeroEntity.Value.Get<Component_AttackPivot>().AttackPoint;
                    var weapon = new MachineGunEntity(_updateProvider, attackPoint, weaponConfig.Type);
                    weapon.Get<IComponent_WeaponInstaller>().Setup(weaponConfig.Parameters);
                    return weapon;
                }
            }

            throw new Exception("no config " + (WeaponType.Riffle));
        }

        public void Clear()
        {
            /*foreach (var collectedWeapon in _collectedWeapons)
            {
                collectedWeapon.Dispose();
            }*/
        }

        public void SetWeaponSelected(in int i)
        {
            CurrentWeaponEntity.Value = CollectedWeapons[i];
        }
    }
}