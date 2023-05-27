using Character;
using Enemy.Agents;

namespace Enemy
{
    public sealed class EnemySetFireTargetController
    {
        private readonly EnemyManager _enemyManager;
        private readonly CharacterMono _characterMono;

        public EnemySetFireTargetController(EnemyManager enemyManager, CharacterMono characterMono)
        {
            _enemyManager = enemyManager;
            _characterMono = characterMono;
            enemyManager.OnEnemySpawn += OnEnemySpawn;
        }

        private void OnEnemySpawn(Enemy enemy)
        {
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(_characterMono.gameObject);
        }
    }
}