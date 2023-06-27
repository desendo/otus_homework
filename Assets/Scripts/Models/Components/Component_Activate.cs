using Common.Atomic.Actions;

namespace Models.Components
{
    public sealed class Component_Activate
    {
        private readonly AtomicEvent _onActivate;
        public Component_Activate(AtomicEvent onActivate)
        {
            _onActivate = onActivate;
        }

        public void Activate()
        {
            _onActivate?.Invoke();
        }
    }
}