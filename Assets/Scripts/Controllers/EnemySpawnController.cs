using Common;
using Config;
using DependencyInjection;
using Pool;
using Services;
using UnityEngine;
using View;

namespace Controllers
{
    public class EnemySpawnController: IStartGameListener, ILostGameListener, IWinGameListener, IUpdate
    {
        private readonly LevelBounds _levelBounds;
        private readonly EnemyService _enemyService;
        private readonly EnemyPool _enemyPool;
        private bool _spawnEnabled;

        private readonly Timer _spawnTimer;
        private readonly DependencyContainer _dependencyContainer;
        private HeroService _heroService;

        public EnemySpawnController(LevelBounds levelBounds, EnemyService enemyService, DependencyContainer dependencyContainer,
            EnemyPool enemyPool, GameConfig gameConfig, HeroService heroService)
        {
            _heroService = heroService;
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

        public void OnLostGame()
        {
            _spawnEnabled = false;
        }

        public void OnWinGame()
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

            var instance = _enemyPool.Spawn(entity=>_dependencyContainer.Inject(entity));

            var x = Random.value * _levelBounds.W + _levelBounds.MinX;
            var z = Random.value * _levelBounds.H + _levelBounds.MinZ;
            var pos = new Vector3(x,0,z);
            instance.transform.position = pos;
            _enemyService.AddUnit(instance);
        }
    }
}