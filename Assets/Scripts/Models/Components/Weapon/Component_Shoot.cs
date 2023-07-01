using Common.Atomic.Actions;

namespace Models.Components
{
    public sealed class Component_Shoot
    {
        public readonly AtomicEvent OnShot;

        public Component_Shoot(AtomicEvent onShot)
        {
            OnShot = onShot;
        }
    }
}