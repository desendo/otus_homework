using Common.Atomic.Values;

namespace Models.Components
{
    public sealed class Component_Burst
    {
        public float Angle { get; private set; }
        public int Count { get; private set; }

        public Component_Burst(AtomicVariable<float> angle, AtomicVariable<int> count)
        {
            angle.OnChanged.Subscribe(x => Angle = angle.Value);
            count.OnChanged.Subscribe(x => Count = count.Value);
            Angle = angle.Value;
            Count = count.Value;
        }
    }
}