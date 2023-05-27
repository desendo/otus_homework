using Enemy.Agents;

namespace Enemy
{
    public sealed class EnemySetMoveTargetController
    {
        private readonly EnemyPositions _positions;

        public EnemySetMoveTargetController(EnemyManager enemyManager, EnemyPositions positions)
        {
            _positions = positions;

            enemyManager.OnEnemySpawn += OnEnemySpawn;
        }

        private void OnEnemySpawn(Enemy enemy)
        {
            var attackPosition = _positions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
        }
    }
}