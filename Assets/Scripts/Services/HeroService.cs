using Common.Entities;
using Config;
using DependencyInjection;
using DependencyInjection.Util;
using Models.Entities;
using UnityEngine;

namespace Services
{
    public class HeroService : ISpawner
    {
        private readonly GameConfig _gameConfig;
        private readonly VisualConfig _visualConfig;
        private DependencyContainer _dependencyContainer;
        public EntityMono HeroEntity { get; private set; }

        public HeroService(GameConfig gameConfig, VisualConfig visualConfig, DependencyContainer dependencyContainer)
        {
            _dependencyContainer = dependencyContainer;
            _gameConfig = gameConfig;
            _visualConfig = visualConfig;
        }

        public void Spawn()
        {
            var prefab = _visualConfig.PlayerPrefab;
            HeroEntity = InstantiateUtil.Instantiate(prefab,
                instance => _dependencyContainer.Inject(instance));
            if (HeroEntity is HeroEntityMono entity)
            {
                entity.Setup(_gameConfig);
            }
        }

        public void Clear()
        {

        }
    }
}