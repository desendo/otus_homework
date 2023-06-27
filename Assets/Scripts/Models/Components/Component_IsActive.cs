using Common.Atomic.Values;

namespace Models.Components
{
    public sealed class Component_IsActive
    {
        public readonly AtomicVariable<bool> IsActive;
        public Component_IsActive(AtomicVariable<bool> isActive)
        {
            IsActive = isActive;
        }
    }
}