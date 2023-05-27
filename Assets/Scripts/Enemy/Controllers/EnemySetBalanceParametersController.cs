using Components;
using Config;
using Enemy.Agents;

namespace Enemy
{
    public sealed class EnemySetBalanceParametersController
    {
        private readonly GameConfig _gameConfig;

        public EnemySetBalanceParametersController(EnemyManager enemyManager, GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            enemyManager.OnEnemySpawn += OnEnemySpawn;
        }

        private void OnEnemySpawn(Enemy enemy)
        {
            enemy.GetComponent<HitPointsComponent>().SetHitPoints(_gameConfig.EnemyHealth, _gameConfig.EnemyHealth);
            enemy.GetComponent<EnemyMoveAgent>().SetSpeed(_gameConfig.EnemyMoveSpeed);
            enemy.GetComponent<EnemyAttackAgent>().SetFireInterval(_gameConfig.EnemyFireInterval, _gameConfig.EnemyFireImmediately);
            enemy.GetComponent<WeaponComponent>()
                .SetBulletConfig(_gameConfig.EnemyBulletConfig);
        }
    }
}