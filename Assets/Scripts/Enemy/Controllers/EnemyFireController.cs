using Bullets;
using Components;
using Enemy.Agents;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyFireController
    {
        private readonly BulletManager _bulletManager;

        public EnemyFireController(EnemyManager enemyManager, BulletManager bulletManager)
        {
            _bulletManager = bulletManager;
            enemyManager.OnEnemySpawn += OnEnemySpawn;
            enemyManager.OnEnemyUnspawn += OnEnemyUnspawn;
        }
        private void OnEnemySpawn(Enemy enemy)
        {
            enemy.GetComponent<EnemyAttackAgent>().OnFire += OnFire;
        }
        private void OnEnemyUnspawn(Enemy enemy)
        {
            enemy.GetComponent<EnemyAttackAgent>().OnFire -= OnFire;
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            var weaponComponent = enemy.GetComponent<WeaponComponent>();
            _bulletManager.FlyBulletByArgs(new BulletManager.Args
            {
                IsPlayer = false,
                PhysicsLayer = (int) weaponComponent.BulletConfig.PhysicsLayer,
                Color = weaponComponent.BulletConfig.Color,
                Damage = weaponComponent.BulletConfig.Damage,
                Position = position,
                Velocity = direction * weaponComponent.BulletConfig.Speed,
            });
        }
    }
}