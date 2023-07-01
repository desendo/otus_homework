using System;
using Common.Atomic.Actions;
using Common.Atomic.Values;

namespace Models.Declarative
{
    public class LifeModel : IDisposable
    {
        public readonly AtomicEvent<int> OnTakeDamage = new AtomicEvent<int>();
        public readonly AtomicVariable<int> HitPoints = new AtomicVariable<int>();
        public readonly AtomicVariable<int> MaxHitPoints = new AtomicVariable<int>();
        public readonly AtomicVariable<bool> IsDead = new AtomicVariable<bool>();
        public readonly AtomicEvent OnDeath = new AtomicEvent();
        private IDisposable _onTakeDamage;
        private IDisposable _onHitPointsChanged;

        public void Construct()
        {
            _onTakeDamage = OnTakeDamage.Subscribe(damage => HitPoints.Value -= damage);
            _onHitPointsChanged = HitPoints.OnChanged.Subscribe(hitPoints =>
            {
                if (hitPoints <= 0 && MaxHitPoints.Value > 0)
                {
                    IsDead.Value = true;
                    OnDeath.Invoke();
                }
            });
            HitPoints.Value = MaxHitPoints.Value;
            MaxHitPoints.OnChanged.Subscribe(x => HitPoints.Value = x);
        }

        public void Dispose()
        {
            _onTakeDamage?.Dispose();
            _onHitPointsChanged?.Dispose();
        }
    }
}