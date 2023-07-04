using Common.Atomic.Actions;

namespace Models.Components
{
    public sealed class Component_OnAttack
    {
        public readonly AtomicEvent OnAttack;

        public Component_OnAttack(AtomicEvent onAttack)
        {
            OnAttack = onAttack;
        }
    }
}