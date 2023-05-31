using System;
using System.Collections.Generic;
using Common;
using Config;
using Effects;
using ReactiveExtension;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyManager : IStartGame, IFinishGame, IUpdate
    {
        public event Action<Enemy> OnEnemySpawn;
        public event Action<Enemy> OnEnemyUnspawn;
        public readonly Reactive<int> OnEnemiesKilledReactive = new Reactive<int>();

        private readonly HashSet<Enemy> _activeEnemies = new HashSet<Enemy>();
        private readonly EnemySpawner _enemySpawner;
        private readonly EnemyPositions _enemyPositions;
        private readonly EffectsService _effectsService;
        private readonly Timer _spawnTimer;

        private bool _gameStarted;

        public EnemyManager(EnemySpawner enemySpawner, EffectsService effectsService, GameConfig gameConfig)
        {
            _effectsService = effectsService;
            _enemySpawner = enemySpawner;
            _spawnTimer = new Timer(gameConfig.EnemySpawnInterval);
        }

        public void FinishGame()
        {
            _gameStarted = false;
            _spawnTimer.OnTime -= SpawnEnemy;
        }

        public void StartGame()
        {
            DoReset();
            _gameStarted = true;
            _spawnTimer.Reset();
            _spawnTimer.OnTime += SpawnEnemy;
        }

        public void Update(float dt)
        {
            if (!_gameStarted)
                return;

            _spawnTimer.Update(dt);
        }

        private void SpawnEnemy()
        {
            var enemyInstance = _enemySpawner.SpawnEnemy();
            if(_activeEnemies.Add(enemyInstance))
                OnEnemySpawn?.Invoke(enemyInstance);
        }

        public void SetEnemyIsDead(GameObject gameObject)
        {
            var enemy = gameObject.GetComponent<Enemy>();
            if (enemy == null)
                throw new Exception("no enemy component");

            if (_activeEnemies.Remove(enemy))
            {
                OnEnemyUnspawn?.Invoke(enemy);
                _effectsService.ShowExplosionEffect(enemy.transform.position);
                _enemySpawner.Unspawn(enemy);
            }

            OnEnemiesKilledReactive.Value++;
        }

        public void DoReset()
        {
            foreach (var enemy in _activeEnemies)
            {
                OnEnemyUnspawn?.Invoke(enemy);
            }
            _enemySpawner.Clear();
            _activeEnemies.Clear();
            OnEnemiesKilledReactive.Value = 0;
        }
    }
}