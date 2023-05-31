using System.Collections.Generic;
using Character;
using Components;
using Config;
using Enemy.Agents;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner
    {
        private readonly EnemyPool _enemyPool;
        private readonly EnemyPositions _enemyPositions;
        private readonly CharacterService _characterService;
        private readonly GameConfig _gameConfig;

        public EnemySpawner(EnemyPool enemyPool, EnemyPositions enemyPositions, CharacterService characterService, GameConfig gameConfig)
        {
            _enemyPool = enemyPool;
            _enemyPositions = enemyPositions;
            _characterService = characterService;
            _gameConfig = gameConfig;
        }

        public Enemy SpawnEnemy()
        {
            var enemy = _enemyPool.Spawn();
            var spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;

            enemy.GetComponent<HitPointsComponent>().SetHitPoints(_gameConfig.EnemyHealth);
            enemy.GetComponent<EnemyMoveAgent>().SetSpeed(_gameConfig.EnemyMoveSpeed);
            enemy.GetComponent<EnemyAttackAgent>().SetFireInterval(_gameConfig.EnemyFireInterval, _gameConfig.EnemyFireImmediately);
            enemy.GetComponent<WeaponComponent>()
                .SetBulletConfig(_gameConfig.EnemyBulletConfig);

            var attackPosition = _enemyPositions.RandomAttackPosition();
            var shift = Random.insideUnitCircle * _gameConfig.EnemyPositionRandomRadius;
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position + (Vector3) shift);

            enemy.GetComponent<EnemyAttackAgent>().SetTarget(_characterService.Character);

            return enemy;
        }

        public void Unspawn(Enemy enemy)
        {
            _enemyPool.Unspawn(enemy);
        }

        public void Clear()
        {
            _enemyPool.Clear();
        }
    }
}