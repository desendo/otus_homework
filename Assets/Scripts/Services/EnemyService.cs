using System;
using System.Collections.Generic;
using System.Linq;
using Common.Atomic.Actions;
using Common.Atomic.Values;
using Common.Entities;
using Config;
using Models.Components;
using Models.Entities;

namespace Services
{
    public class EnemyService
    {
        private readonly GameConfig _gameConfig;
        public readonly AtomicVariable<int> TotalSpawned = new AtomicVariable<int>();
        public readonly AtomicVariable<int> KillGoal = new AtomicVariable<int>();
        public readonly AtomicVariable<int> Killed = new AtomicVariable<int>();
        public readonly AtomicEvent<IEntity> OnDeath = new AtomicEvent<IEntity>();
        public List<EnemyEntityMono> Units { get; } = new List<EnemyEntityMono>();

        public EnemyService(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            KillGoal.Value = gameConfig.KillGoal;
        }

        public void AddUnit(EnemyEntityMono instance)
        {
            Units.Add(instance);

            instance.Get<Component_ZombieHandsInstaller>()
                .Setup(_gameConfig.Weapons.FirstOrDefault(x=>x.Type == WeaponType.ZombieHands)?.Parameters);
            instance.Get<Component_EnemyInstaller>().Setup(_gameConfig);
            instance.Get<Component_Death>().SetCallback(x=> OnDeath.Invoke(x));
            instance.Get<Component_IsActive>().IsActive.Value = true;

            TotalSpawned.Value++;
        }

        public void Reset(Action<EnemyEntityMono> onReset)
        {
            foreach (var entityMono in Units)
            {
                onReset.Invoke(entityMono);
            }
            Units.Clear();
            TotalSpawned.Value = 0;
            Killed.Value = 0;
        }

    }
}