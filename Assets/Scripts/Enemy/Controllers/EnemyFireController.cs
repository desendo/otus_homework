using Bullets;
using Components;
using Enemy.Agents;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyFireController
    {
        private readonly BulletSystem _bulletSystem;

        public EnemyFireController(EnemyManager enemyManager, BulletSystem bulletSystem)
        {
            _bulletSystem = bulletSystem;
            enemyManager.OnEnemySpawn += OnEnemySpawn;
            enemyManager.OnEnemyDeSpawn += OnEnemyDeSpawn;
        }
        private void OnEnemySpawn(Enemy enemy)
        {
            enemy.GetComponent<EnemyAttackAgent>().OnFire += OnFire;
        }
        private void OnEnemyDeSpawn(Enemy enemy)
        {
            enemy.GetComponent<EnemyAttackAgent>().OnFire -= OnFire;
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            var weaponComponent = enemy.GetComponent<WeaponComponent>();
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                IsPlayer = false,
                PhysicsLayer = (int) weaponComponent.BulletConfig.PhysicsLayer,
                Color = weaponComponent.BulletConfig.Color,
                Damage = weaponComponent.BulletConfig.Damage,
                Position = position,
                Velocity = direction * weaponComponent.BulletConfig.Speed
            });
        }
    }
}