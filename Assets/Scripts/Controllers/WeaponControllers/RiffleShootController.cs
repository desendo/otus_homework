using Common.Entities;
using Config;
using Managers;
using Models.Components;
using Services;

namespace Controllers.WeaponControllers
{
    public class RiffleShootController : WeaponShootControllerBase
    {
        public RiffleShootController(WeaponManager weaponManager, IBulletSpawner bulletSpawner) : base(weaponManager, bulletSpawner)
        {
        }

        protected override WeaponType WeaponType => WeaponType.Riffle;

        protected override void HandleShoot(IEntity riffle)
        {
            var pivot = riffle.Get<Component_Pivot>();
            var bulletSpeed = riffle.Get<Component_Speed>().Speed;
            var damage = riffle.Get<Component_Damage>().Damage.Value;
            BulletSpawner.FireBullet(pivot.Position, pivot.Direction, bulletSpeed, damage);
        }
    }
}