using Common.Atomic.Actions;

namespace Models.Components
{
    public sealed class Component_TakeDamage
    {
        private readonly AtomicEvent<float> _onTakeDamage;

        public Component_TakeDamage(AtomicEvent<float> onTakeDamage)
        {
            _onTakeDamage = onTakeDamage;
        }

        public void DoDamage(float val)
        {
            _onTakeDamage.Invoke(val);
        }
    }
}