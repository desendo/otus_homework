using System;
using System.Threading.Tasks;
using Common;
using Config;
using Effects;
using Services;
using UnityEngine;
using Views;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class UnitSpawnController : IUpdate
    {
        private readonly UnitService _unitService;
        private readonly GameConfig _gameConfig;
        private readonly EffectsService _effectsService;
        private readonly SceneInfo _sceneInfo;
        private readonly Timer _spawnTimer;

        public UnitSpawnController(UnitService unitService, GameConfig gameConfig,
            EffectsService effectsService, SceneInfo sceneInfo)
        {
            _sceneInfo = sceneInfo;
            _effectsService = effectsService;
            _gameConfig = gameConfig;
            _unitService = unitService;
            _spawnTimer = new Timer(_gameConfig.SpawnInterval);
            _spawnTimer.OnTime += TrySpawn;
        }

        private void TrySpawn()
        {
            var isLeft = Random.value < 0.5f;
            var tr = isLeft ? _sceneInfo.Left : _sceneInfo.Right;
            var team = isLeft ? 1 : 0;
            SpawnBurst(tr, team);
            _spawnTimer.Reset();
        }


        private void SpawnBurst(Transform tr, int team)
        {
            for (int i = 0; i < _gameConfig.SpawnBurstCount; i++)
            {
                var point = (Vector3) Random.insideUnitCircle * 10f + tr.position;
                var dir = tr.forward;
                SpawnOne(point, dir, team);
            }
        }

        private void SpawnOne(Vector3 point, Vector3 dir, int team)
        {
            _effectsService.ShowTeleportEffect(point, dir);
            _unitService.CreateUnit(point, dir, _gameConfig.EngineConfig, _gameConfig.WeaponConfig, team, _gameConfig.Health);
        }

        public void Update(float dt)
        {
            _spawnTimer.Update(dt);
        }

    }
}