using Common.Entities;
using Config;
using Managers;
using Models.Components;
using Services;
using UnityEngine;

namespace Controllers.WeaponControllers
{
    public class MachineGunController : WeaponShootControllerBase
    {
        protected override WeaponType WeaponType => WeaponType.MachineGun;

        public MachineGunController(WeaponManager weaponManager, IBulletSpawner bulletSpawner) : base(weaponManager, bulletSpawner)
        {
        }
        protected override void HandleShoot(IEntity weapon)
        {
            var dispersion = weapon.Get<Component_Dispersion>();
            var pivot = weapon.Get<Component_Pivot>();
            var bulletSpeed = weapon.Get<Component_Speed>().Speed;

            var randomAngle = UnityEngine.Random.Range(-dispersion.Angle, dispersion.Angle);
            var randomized = Quaternion.Euler(0f, randomAngle, 0f) * pivot.Direction;
            var damage = weapon.Get<Component_Damage>().Damage.Value;

            BulletSpawner.FireBullet(pivot.Position, randomized, bulletSpeed, damage);
        }
    }
}