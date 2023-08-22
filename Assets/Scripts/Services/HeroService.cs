using System;
using System.Collections.Generic;
using Common.Atomic.Values;
using Common.Entities;
using Config;
using DependencyInjection;
using DependencyInjection.Util;
using GameManager;
using ItemInventory.Config;
using Models.Components;
using Models.Entities;
using Services.Effects;
using UnityEngine;

namespace Services
{
    public class HeroService : ISpawner, IEffectContainer
    {
        private readonly GameConfig _gameConfig;
        private readonly VisualConfig _visualConfig;
        private readonly DependencyContainer _dependencyContainer;
        public AtomicVariable<IEntity> HeroEntity { get; } = new AtomicVariable<IEntity>();

        public readonly List<IWeapon> CollectedWeapons = new List<IWeapon>();
        private readonly IUpdateProvider _updateProvider;
        private IDisposable _reloadSubscribe;
        private IDisposable _shotSubscribe;

        public HeroService(GameConfig gameConfig, VisualConfig visualConfig,
            DependencyContainer dependencyContainer, IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;
            _dependencyContainer = dependencyContainer;
            _gameConfig = gameConfig;
            _visualConfig = visualConfig;
        }

        public void Spawn()
        {
            var prefab = _visualConfig.PlayerPrefab;
            HeroEntity.Value = _dependencyContainer.InstantiateInject(prefab);
            HeroEntity.Value.Get<Component_HeroInstaller>().Setup(_gameConfig);


        }

        public void Clear()
        {

            if (HeroEntity.Value != null)
            {
                var gameObject = (HeroEntity.Value as MonoBehaviour)?.gameObject;
                UnityEngine.Object.Destroy(gameObject);
            }
        }

        public void AddEffect(Effect effect)
        {
            if(HeroEntity.Value == null)
                return;

        }

        public void RemoveEffect(Effect effect)
        {
            if(HeroEntity.Value == null)
                return;
        }
    }
}