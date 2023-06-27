using System;
using Common.Atomic.Actions;
using Common.Atomic.Values;

namespace Models.Declarative
{
    public class LifeModel : IDisposable
    {
        public readonly AtomicEvent<int> OnTakeDamage = new AtomicEvent<int>();
        public readonly AtomicVariable<int> HitPoints = new AtomicVariable<int>();
        public readonly AtomicVariable<bool> IsDead = new AtomicVariable<bool>();
        private IDisposable _onTakeDamage;
        private IDisposable _onHitPointsChanged;

        public void Construct()
        {
            _onTakeDamage = OnTakeDamage.Subscribe(damage => HitPoints.Value -= damage);
            _onHitPointsChanged = HitPoints.OnChanged.Subscribe(hitPoints =>
            {
                if (hitPoints <= 0) IsDead.Value = true;
            });
        }

        public void Dispose()
        {
            _onTakeDamage?.Dispose();
            _onHitPointsChanged?.Dispose();
        }
    }
}