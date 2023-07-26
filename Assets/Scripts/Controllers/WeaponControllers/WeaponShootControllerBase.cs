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

        protected WeaponShootControllerBase(HeroManager heroManager, IBulletSpawner bulletSpawner)
        {
            BulletSpawner = bulletSpawner;
            heroManager.CurrentWeaponEntity.OnChanged.Subscribe(OnWeaponChanged);
        }

        private void OnWeaponChanged(IWeapon obj)
        {
            _shootRequested?.Dispose();
            if (obj.WeaponType == WeaponType)
            {
                _shootRequested = obj.Get<Component_OnAttack>().OnAttack.Subscribe(() =>
                {
                    HandleShoot(obj);
                });
            }
        }

        protected abstract void HandleShoot(IEntity weapon);
    }
}