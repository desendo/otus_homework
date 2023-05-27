using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private Transform _container;
        [SerializeField] private Enemy _prefab;
        [SerializeField] private int _poolSize;

        private readonly Queue<Enemy> _enemyPool = new Queue<Enemy>();

        private void Awake()
        {
            for (var i = 0; i < _poolSize; i++)
            {
                AddEnemyToPool();
            }
        }

        private void AddEnemyToPool()
        {
            var enemy = Instantiate(_prefab, _container);
            _enemyPool.Enqueue(enemy);
        }

        public Enemy SpawnEnemy()
        {
            if(_enemyPool.Count == 0)
                AddEnemyToPool();

            if (_enemyPool.TryDequeue(out var enemy))
            {
                enemy.transform.SetParent(_worldTransform);
                return enemy;
            }
            throw new Exception("no enemies in pool");
        }

        public void UnspawnEnemy(Enemy enemy)
        {
            enemy.transform.SetParent(_container);
            _enemyPool.Enqueue(enemy);
        }
    }
}