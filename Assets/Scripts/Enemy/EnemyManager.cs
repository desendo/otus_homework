using System;
using System.Collections.Generic;
using Bullets;
using Common;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyManager : IStartGame, IFinishGame, IUpdate
    {
        public event Action<int> OnEnemiesKilled;
        private readonly EnemyPool _enemyPool;
        private readonly BulletSystem _bulletSystem;
        private readonly EnemyPositions _enemyPositions;
        private readonly Character.Character _character;

        private readonly HashSet<Enemy> _activeEnemies = new HashSet<Enemy>();
        private float _delay;
        private float _timeToSpawn = 1f;
        private bool _gameStarted;
        private int _killedEnemies = 0;
        public int KilledEnemies => _killedEnemies;

        public EnemyManager(EnemyPool enemyPool, BulletSystem bulletSystem, EnemyPositions enemyPositions, Character.Character character)
        {
            _enemyPool = enemyPool;
            _bulletSystem = bulletSystem;
            _enemyPositions = enemyPositions;
            _character = character;
        }


        public void StartGame()
        {
            foreach (var enemy in _activeEnemies)
            {
                enemy.HitPointsComponent.hpEmpty -= OnDestroyed;
                enemy.EnemyAttackAgent.OnFire -= OnFire;

                _enemyPool.UnspawnEnemy(enemy);
            }
            _activeEnemies.Clear();
            _killedEnemies = 0;
            _gameStarted = true;
        }

        public void FinishGame()
        {
            _gameStarted = false;
        }

        public void Update(float dt)
        {
            if(!_gameStarted)
                return;

            _delay += dt;
            if(_delay < _timeToSpawn)
                return;

            _delay = 0f;
            var enemy = _enemyPool.SpawnEnemy();
            if (enemy != null)
                if (_activeEnemies.Add(enemy))
                {
                    var attackPosition = _enemyPositions.RandomAttackPosition();
                    enemy.EnemyMoveAgent.SetDestination(attackPosition.position);
                    enemy.EnemyAttackAgent.SetTarget(_character.gameObject);
                    enemy.HitPointsComponent.hpEmpty += OnDestroyed;
                    enemy.EnemyAttackAgent.OnFire += OnFire;
                }
        }

        private void OnDestroyed(GameObject obj)
        {
            var enemy = obj.GetComponent<Enemy>();
            if(enemy == null)
                return;

            if (_activeEnemies.Remove(enemy))
            {
                enemy.HitPointsComponent.hpEmpty -= OnDestroyed;
                enemy.EnemyAttackAgent.OnFire -= OnFire;

                _enemyPool.UnspawnEnemy(enemy);
            }

            _killedEnemies += 1;
            OnEnemiesKilled?.Invoke(_killedEnemies);
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = false,
                physicsLayer = (int) PhysicsLayer.ENEMY_BULLET,
                color = Color.red,
                damage = 1,
                position = position,
                velocity = direction * 2.0f
            });
        }


    }
}