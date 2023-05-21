using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private Transform container;
        [SerializeField] private Enemy prefab;

        private readonly Queue<Enemy> _enemyPool = new Queue<Enemy>();

        private void Awake()
        {
            for (var i = 0; i < 7; i++)
            {
                var enemy = Instantiate(prefab, container);
                _enemyPool.Enqueue(enemy);
            }
        }

        public Enemy SpawnEnemy()
        {
            if (!_enemyPool.TryDequeue(out var enemy)) return null;

            enemy.transform.SetParent(worldTransform);

            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;


            return enemy;
        }

        public void UnspawnEnemy(Enemy enemy)
        {
            enemy.transform.SetParent(container);
            _enemyPool.Enqueue(enemy);
        }
    }
}