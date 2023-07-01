using System;
using Common.Entities;
using Config;
using Managers;
using Models.Components;
using Models.Entities;
using Services;

namespace Controllers.WeaponControllers
{
    public abstract class WeaponShootControllerBase
    {
        private IDisposable _shootRequested;
        protected readonly IBulletSpawner BulletSpawner;

        protected abstract WeaponType WeaponType { get; }

        protected WeaponShootControllerBase(HeroService heroService, IBulletSpawner bulletSpawner)
        {
            BulletSpawner = bulletSpawner;
            heroService.CurrentWeaponEntity.OnChanged.Subscribe(OnWeaponChanged);
        }

        private void OnWeaponChanged(IWeapon obj)
        {
            _shootRequested?.Dispose();
            if (obj.WeaponType == WeaponType)
            {
                _shootRequested = obj.Get<Component_Shoot>().OnShot.Subscribe(() =>
                {
                    HandleShoot(obj);
                });
            }
        }

        protected abstract void HandleShoot(IEntity weapon);
    }
}