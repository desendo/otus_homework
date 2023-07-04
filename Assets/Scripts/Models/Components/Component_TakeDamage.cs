using Common.Atomic.Actions;

namespace Models.Components
{
    public sealed class Component_TakeDamage
    {
        private readonly AtomicEvent<int> _onTakeDamage;

        public Component_TakeDamage(AtomicEvent<int> onTakeDamage)
        {
            _onTakeDamage = onTakeDamage;
        }

        public void DoDamage(int val)
        {
            _onTakeDamage.Invoke(val);
        }
    }
}