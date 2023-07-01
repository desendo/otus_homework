using System;
using System.Collections.Generic;
using Common;
using Common.Atomic.Values;
using Services;

namespace UI.PresentationModel
{
    public class KillsPresentationModel
    {
        public AtomicVariable<int> Current { get; } = new AtomicVariable<int>();
        public AtomicVariable<int> Max { get; } = new AtomicVariable<int>();

        private readonly List<IDisposable> _subs = new List<IDisposable>();
        private readonly EnemyService _enemyService;

        public KillsPresentationModel(EnemyService enemyService)
        {
            _enemyService = enemyService;
            _enemyService.Killed.OnChanged.Subscribe(x => UpdateInfo()).AddTo(_subs);
            _enemyService.TotalSpawned.OnChanged.Subscribe(x => UpdateInfo()).AddTo(_subs);
        }

        private void UpdateInfo()
        {
            Max.Value = _enemyService.TotalSpawned.Value;
            Current.Value = _enemyService.Killed.Value;
        }
    }
}