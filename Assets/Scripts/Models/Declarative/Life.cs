using System;
using Common.Atomic.Actions;
using Common.Atomic.Values;

namespace Models.Declarative
{
    public class Life : IDisposable
    {
        public readonly AtomicEvent<float> OnTakeDamage = new AtomicEvent<float>();
        public readonly AtomicVariable<float> DamageMultiplier = new AtomicVariable<float>(1f);
        public readonly AtomicVariable<float> DamageReducer = new AtomicVariable<float>(0f);
        public readonly AtomicVariable<float> HitPoints = new AtomicVariable<float>();
        public readonly AtomicVariable<float> MaxHitPoints = new AtomicVariable<float>();
        public readonly AtomicVariable<float> EvasionChance = new AtomicVariable<float>();
        public readonly AtomicVariable<bool> IsDead = new AtomicVariable<bool>();
        public readonly AtomicEvent OnDeath = new AtomicEvent();
        private IDisposable _onTakeDamage;
        private IDisposable _onHitPointsChanged;

        public void Construct()
        {
            _onTakeDamage = OnTakeDamage.Subscribe(damage =>
            {
                if(UnityEngine.Random.value <= EvasionChance.Value)
                    return;

                var targetDamage = damage * DamageMultiplier.Value - DamageReducer.Value;
                if (targetDamage < 0f)
                    targetDamage = 0f;
                var targetHP = HitPoints.Value - targetDamage;
                if (targetHP < 0)
                    targetHP = 0;
                HitPoints.Value = targetHP;

            });
            _onHitPointsChanged = HitPoints.OnChanged.Subscribe(hitPoints =>
            {
                if (hitPoints <= 0 && MaxHitPoints.Value > 0 && !IsDead.Value)
                {
                    IsDead.Value = true;
                    OnDeath.Invoke();
                }
            });
            HitPoints.Value = MaxHitPoints.Value;
            MaxHitPoints.OnChanged.Subscribe(x => HitPoints.Value = x);
            IsDead.Value = HitPoints.Value <= 0 && MaxHitPoints.Value > 0;
        }

        public void Dispose()
        {
            _onTakeDamage?.Dispose();
            _onHitPointsChanged?.Dispose();
        }
    }
}