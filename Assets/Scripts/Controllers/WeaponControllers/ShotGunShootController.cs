using Common.Entities;
using Config;
using Managers;
using Models.Components;
using Services;
using UnityEngine;

namespace Controllers
{
    public class ShotGunShootController : WeaponShootControllerBase
    {
        protected override WeaponType WeaponType => WeaponType.Shotgun;

        public ShotGunShootController(HeroService heroService, BulletManager bulletManager) : base(heroService, bulletManager)
        {
        }
        protected override void HandleShoot(IEntity weapon)
        {
            var burst = weapon.Get<Component_Burst>();
            var pivot = weapon.Get<Component_Pivot>();
            var bulletSpeed = weapon.Get<Component_Speed>().Speed;
            for (int i = 0; i < burst.Count.Value; i++)
            {
                var randomAngle = UnityEngine.Random.Range(-burst.Angle.Value, burst.Angle.Value);
                var randomized = Quaternion.Euler(0f, randomAngle, 0f) * pivot.Direction;

                _bulletManager.FireBullet(pivot.Position, randomized, bulletSpeed);
            }
        }
    }
}