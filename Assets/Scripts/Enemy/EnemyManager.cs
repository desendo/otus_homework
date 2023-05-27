using System;
using System.Collections.Generic;
using Config;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyManager : IStartGame, IFinishGame, IUpdate,IReset
    {
        public event Action<Enemy> OnEnemySpawn;
        public event Action<Enemy> OnEnemyDeSpawn;
        public event Action<int> OnEnemiesKilled;
        public int KilledEnemies
        {
            get => _killed;
            private set
            {
                _killed = value;
                OnEnemiesKilled?.Invoke(_killed);
            }
        }

        private readonly HashSet<Enemy> _activeEnemies = new HashSet<Enemy>();
        private readonly EnemyPool _enemyPool;
        private readonly GameConfig _gameConfig;
        private readonly EnemyPositions _enemyPositions;
        private float _delay;
        private bool _gameStarted;
        private int _killed;

        public EnemyManager(EnemyPool enemyPool, GameConfig gameConfig, EnemyPositions enemyPositions)
        {
            _gameConfig = gameConfig;
            _enemyPositions = enemyPositions;
            _enemyPool = enemyPool;
        }

        public void FinishGame()
        {
            _gameStarted = false;
        }

        public void StartGame()
        {
            _gameStarted = true;
        }

        public void Update(float dt)
        {
            if (!_gameStarted)
                return;

            _delay += dt;
            if (_delay < _gameConfig.EnemySpawnInterval)
                return;

            _delay = 0f;
            var enemy = _enemyPool.SpawnEnemy();
            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            if (_activeEnemies.Add(enemy))
                OnEnemySpawn?.Invoke(enemy);
        }

        public void SetEnemyIsDead(GameObject gameObject)
        {
            var enemy = gameObject.GetComponent<Enemy>();
            if (enemy == null)
                throw new Exception("no enemy component");

            if (_activeEnemies.Remove(enemy))
            {
                OnEnemyDeSpawn?.Invoke(enemy);
                _enemyPool.UnspawnEnemy(enemy);
            }

            KilledEnemies++;
        }

        public void DoReset()
        {
            foreach (var enemy in _activeEnemies)
            {
                OnEnemyDeSpawn?.Invoke(enemy);
                _enemyPool.UnspawnEnemy(enemy);
            }

            KilledEnemies = 0;
            _activeEnemies.Clear();
            _delay = _gameConfig.EnemySpawnInterval;
        }
    }
}