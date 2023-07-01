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
        public readonly AttackModel AttackModel = new AttackModel();
        public readonly EnemyWeaponModelCore Weapon = new EnemyWeaponModelCore();
        public readonly AtomicVariable<float> Speed = new AtomicVariable<float>();
        public AtomicVariable<bool> IsActive = new AtomicVariable<bool>();
        private readonly List<IDisposable> _subs = new List<IDisposable>();

        public void Construct(IUpdateProvider updateProvider)
        {
            LifeModel.Construct();
            LifeModel.OnDeath.Subscribe(()=>IsActive.Value = false).AddTo(_subs);
            AttackModel.Construct();
            Weapon.Construct(updateProvider);
        }

        public void Dispose()
        {
            LifeModel.Dispose();
            AttackModel.Dispose();
            Weapon.Dispose();
            _subs.Dispose();
        }
    }
}