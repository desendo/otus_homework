using System;
using Common.Atomic.Actions;
using Common.Atomic.Values;

namespace Models.Declarative
{
    public class AttackModel : IDisposable
    {
        public void Attack()
        {
        }

        public AtomicEvent<float> OnReloadStarted { get; } = new AtomicEvent<float>();
        public AtomicEvent OnAttackStart { get; } = new AtomicEvent();

        public void Construct()
        {

        }

        public void Dispose()
        {
        }
    }
}