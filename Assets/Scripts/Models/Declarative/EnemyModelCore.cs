using System;
using System.Collections.Generic;
using Common;
using Common.Atomic.Values;
using GameManager;
using Models.Declarative.Weapons;

namespace Models.Declarative
{
    public class EnemyModelCore : IDisposable
    {
        public readonly LifeModel LifeModel = new LifeModel();
        public readonly EnemyWeaponModelCore Weapon = new EnemyWeaponModelCore();

        public readonly AtomicVariable<float> Speed = new AtomicVariable<float>();
        public readonly AtomicVariable<float> CurrentSpeedMultiplier = new AtomicVariable<float>(1);
        public readonly AtomicVariable<float> TargetSpeedMultiplierOnHits = new AtomicVariable<float>(1);
        public readonly AtomicVariable<bool> IsActive = new AtomicVariable<bool>();

        private readonly List<IDisposable> _subs = new List<IDisposable>();

        public void Construct(IUpdateProvider updateProvider)
        {
            LifeModel.Construct();
            LifeModel.OnDeath.Subscribe(()=>IsActive.Value = false).AddTo(_subs);
            Weapon.Construct(updateProvider);
            Weapon.AttackReady.OnChanged.Subscribe(isReady =>
            {
                CurrentSpeedMultiplier.Value = isReady ? 1f : TargetSpeedMultiplierOnHits.Value;
            }).AddTo(_subs);
            Weapon.Activate.Invoke(true);
        }

        public void Dispose()
        {
            LifeModel.Dispose();
            Weapon.Dispose();
            _subs.Dispose();
        }
    }
}