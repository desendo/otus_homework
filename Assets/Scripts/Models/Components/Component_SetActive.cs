using Common.Atomic.Actions;

namespace Models.Components
{
    public sealed class Component_SetActive
    {
        private readonly AtomicEvent<bool> _setActive;
        public Component_SetActive(AtomicEvent<bool> setActive)
        {
            _setActive = setActive;
        }

        public void SetActive(bool isActive)
        {
            _setActive?.Invoke(isActive);
        }
    }
}