﻿using Common;
using Config;
using DependencyInjection;
using Models.Components;
using Pool;
using Services;
using UnityEngine;
using View;

namespace Controllers
{
    public class EnemyManager: IStartGameListener, IFinishGameListener, IUpdate
    {
        private readonly LevelBounds _levelBounds;
        private readonly EnemyService _enemyService;
        private readonly EnemyPool _enemyPool;
        private bool _spawnEnabled;

        private readonly Timer _spawnTimer;
        private readonly DependencyContainer _dependencyContainer;
        private readonly WeaponManager _weaponManager;
        private float _radius = 10f;
        private float _radiusWidth = 5f;
        private HeroService _heroService;

        public EnemyManager(LevelBounds levelBounds, EnemyService enemyService, DependencyContainer dependencyContainer,
            EnemyPool enemyPool, GameConfig gameConfig, WeaponManager weaponManager, HeroService heroService)
        {
            _heroService = heroService;
            _weaponManager = weaponManager;
            _dependencyContainer = dependencyContainer;
            _levelBounds = levelBounds;
            _enemyService = enemyService;
            _enemyPool = enemyPool;
            _spawnTimer = new Timer(gameConfig.ZombieSpawnInterval);
            _spawnTimer.OnTime += TrySpawn;

        }

        public void OnStartGame()
        {
            _spawnEnabled = true;
            _enemyService.Reset(x=>_enemyPool.Unspawn(x));
            _spawnTimer.Reset();
        }

        public void OnFinishGame(bool isWin)
        {
            _spawnEnabled = false;
        }

        public void Update(float dt)
        {
            if(!_spawnEnabled)
                return;

            _spawnTimer.Update(dt);

        }

        private void TrySpawn()
        {
            if(_enemyService.TotalSpawned.Value >= _enemyService.KillGoal.Value)
                return;

            var heroPos = _heroService.HeroEntity.Value.Get<Component_Transform>().RootTransform.position;
            var instance = _enemyPool.Spawn(entity=>_dependencyContainer.Inject(entity));

            while (true)
            {
                var dir = Random.insideUnitCircle.normalized;
                var radius = (UnityEngine.Random.value - 0.5f) * _radiusWidth + _radius;
                var trySpawnPos = heroPos + new Vector3(dir.x,0,dir.y) * radius;

                if (_levelBounds.InBounds(trySpawnPos))
                {
                    instance.transform.position = trySpawnPos;
                    _enemyService.AddUnit(instance);
                    break;
                }
            }
        }
    }
}