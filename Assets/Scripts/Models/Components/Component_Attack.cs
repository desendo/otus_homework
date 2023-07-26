using System;
using Common.Atomic.Actions;
using UnityEngine;

namespace Models.Components
{
    public sealed class Component_Attack
    {
        private readonly Action _attack;
        private readonly AtomicAction _attackContinue;
        private readonly AtomicAction _stopAttack;

        public Component_Attack(Action attack, AtomicAction stopAttack = null, AtomicAction attackContinue = null)
        {
            _attack = attack;
            _attackContinue = attackContinue;
            _stopAttack = stopAttack;
        }

        public void Attack()
        {
            _attack.Invoke();
        }

        public void StopAttack()
        {
            _stopAttack?.Invoke();
        }
        public void ContinueAttack()
        {
            _attackContinue?.Invoke();
        }
    }
}