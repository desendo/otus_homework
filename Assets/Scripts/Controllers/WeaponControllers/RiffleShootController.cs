using System;
using Common.Entities;
using Config;
using Managers;
using Models.Components;
using Services;

namespace Controllers
{
    public class RiffleShootController : WeaponShootControllerBase
    {
        public RiffleShootController(HeroService heroService, BulletManager bulletManager) : base(heroService, bulletManager)
        {
        }

        protected override WeaponType WeaponType => WeaponType.Riffle;

        protected override void HandleShoot(IEntity riffle)
        {
            var pivot = riffle.Get<Component_Pivot>();
            var bulletSpeed = riffle.Get<Component_Speed>().Speed;
            _bulletManager.FireBullet(pivot.Position, pivot.Direction, bulletSpeed);
        }
    }
}