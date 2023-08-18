using Common.Entities;
using Config;
using Managers;
using Models.Components;
using Services;
using UnityEngine;

namespace Controllers.WeaponControllers
{
    public class ShotGunShootController : WeaponShootControllerBase
    {
        protected override WeaponType WeaponType => WeaponType.Shotgun;

        public ShotGunShootController(WeaponManager weaponManager, IBulletSpawner bulletSpawner) : base(weaponManager, bulletSpawner)
        {
        }
        protected override void HandleShoot(IEntity weapon)
        {
            var burst = weapon.Get<Component_Burst>();
            var pivot = weapon.Get<Component_Pivot>();
            var bulletSpeed = weapon.Get<Component_Speed>().Speed;
            for (int i = 0; i < burst.Count; i++)
            {
                var randomAngle = UnityEngine.Random.Range(-burst.Angle, burst.Angle);
                var randomized = Quaternion.Euler(0f, randomAngle, 0f) * pivot.Direction;
                var damage = weapon.Get<Component_Damage>().Damage.Value;

                BulletSpawner.FireBullet(pivot.Position, randomized, bulletSpeed, damage);
            }
        }
    }
}