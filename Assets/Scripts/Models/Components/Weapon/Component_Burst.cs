using Common.Atomic.Values;

namespace Models.Components
{
    public sealed class Component_Burst
    {
        public readonly AtomicVariable<float> Angle;
        public readonly AtomicVariable<int> Count;

        public Component_Burst(AtomicVariable<float> angle, AtomicVariable<int> count)
        {
            Angle = angle;
            Count = count;
        }
    }
}