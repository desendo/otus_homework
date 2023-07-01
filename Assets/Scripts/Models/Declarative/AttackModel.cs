using System;
using Common.Atomic.Actions;

namespace Models.Declarative
{
    public class AttackModel : IDisposable
    {
        public AtomicAction Attack { get; }
        public AtomicEvent<float> IsReloadStarted { get; } = new AtomicEvent<float>();
        public AtomicEvent OnAttackStart { get; } = new AtomicEvent();

        public void Construct()
        {

        }

        public void Dispose()
        {
        }
    }
}