using Common.Atomic.Values;

namespace Models.Components
{
    public sealed class Component_Health
    {
        public readonly AtomicVariable<float> Current;
        public readonly AtomicVariable<float> Max;

        public Component_Health(AtomicVariable<float> current, AtomicVariable<float> max)
        {
            Current = current;
            Max = max;
        }
    }
}