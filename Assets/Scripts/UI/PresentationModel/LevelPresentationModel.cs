using System;
using System.Collections.Generic;
using Common.Atomic.Values;
using Common.Entities;
using Services;

namespace UI.PresentationModel
{
    public class LevelPresentationModel
    {
        public AtomicVariable<int> Killed { get; } = new AtomicVariable<int>();
        public AtomicVariable<int> Spawned { get; } = new AtomicVariable<int>();

        private readonly List<IDisposable> _subs = new List<IDisposable>();
        public LevelPresentationModel(EnemyService heroService)
        {
            heroService.Killed.OnChanged.Subscribe(x => UpdateInfo());
            heroService.TotalSpawned.OnChanged.Subscribe(x => UpdateInfo());
        }

        private void UpdateInfo()
        {
        }

        private void OnHero(EntityMono obj)
        {
            _subs.Clear();
            if(obj == null)
                return;

        }
    }
}