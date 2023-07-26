﻿using System;
using Common.Atomic.Actions;
using Common.Atomic.Values;

namespace Models.Declarative.Weapons
{
    public abstract class WeaponModelCoreAbstract : IDisposable
    {
        public readonly AtomicVariable<bool> AttackReady = new AtomicVariable<bool>();
        public readonly AtomicVariable<int> Damage = new AtomicVariable<int>();
        public readonly AtomicVariable<bool> IsActive = new AtomicVariable<bool>();
        public readonly AtomicEvent AttackRequested = new AtomicEvent();
        public readonly AtomicVariable<string> Name = new AtomicVariable<string>();



        public abstract void Dispose();
        public virtual void Activate(bool value)
        {
        }
        public virtual void Attack()
        {
        }
    }
}