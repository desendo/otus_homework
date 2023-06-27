using Common.Entities;
using Config;
using Managers;
using Models.Components;
using Services;
using UnityEngine;

namespace Controllers
{
    public class MachineGunController : WeaponShootControllerBase
    {
        protected override WeaponType WeaponType => WeaponType.MachineGun;

        public MachineGunController(HeroService heroService, BulletManager bulletManager) : base(heroService, bulletManager)
        {
        }
        protected override void HandleShoot(IEntity weapon)
        {
            var dispersion = weapon.Get<Component_Dispersion>();
            var pivot = weapon.Get<Component_Pivot>();
            var bulletSpeed = weapon.Get<Component_Speed>().Speed;

            var randomAngle = UnityEngine.Random.Range(-dispersion.Angle.Value, dispersion.Angle.Value);
            var randomized = Quaternion.Euler(0f, randomAngle, 0f) * pivot.Direction;
            _bulletManager.FireBullet(pivot.Position, randomized, bulletSpeed);

        }
    }
}