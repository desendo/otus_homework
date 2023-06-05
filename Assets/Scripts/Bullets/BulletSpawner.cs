namespace Bullets
{
    public sealed class BulletSpawner
    {
        private readonly BulletPool _bulletPool;

        public BulletSpawner(BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
        }

        public Bullet SpawnBullet(BulletManager.Args args)
        {
            var bullet = _bulletPool.Spawn();
            bullet.SetPosition(args.Position);
            bullet.SetColor(args.Color);
            bullet.SetPhysicsLayer(args.PhysicsLayer);
            bullet.Damage = args.Damage;
            bullet.IsPlayer = args.IsPlayer;
            bullet.SetVelocity(args.Velocity);
            return bullet;
        }
    }
}