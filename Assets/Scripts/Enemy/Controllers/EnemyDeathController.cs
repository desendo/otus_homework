using Components;
using Enemy.Agents;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyDeathController
    {
        private readonly EnemyManager _enemyManager;
        private readonly EnemyPositions _positions;

        public EnemyDeathController(EnemyManager enemyManager)
        {
            _enemyManager = enemyManager;

            enemyManager.OnEnemySpawn += OnEnemySpawn;
            enemyManager.OnEnemyDeSpawn += OnEnemyDeSpawn;
        }

        private void OnEnemyDeSpawn(Enemy obj)
        {
            obj.GetComponent<HitPointsComponent>().HpEmpty -= OnHpEmpty;
        }

        private void OnEnemySpawn(Enemy enemy)
        {
            enemy.GetComponent<HitPointsComponent>().HpEmpty += OnHpEmpty;
        }

        private void OnHpEmpty(GameObject obj)
        {
            _enemyManager.SetEnemyIsDead(obj);
        }
    }
}