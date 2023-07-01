using Common.Atomic.Values;

namespace Models.Components
{
    public sealed class Component_Health
    {
        public readonly AtomicVariable<int> Current;
        public readonly AtomicVariable<int> Max;

        public Component_Health(AtomicVariable<int> current, AtomicVariable<int> max)
        {
            Current = current;
            Max = max;
        }
    }
}