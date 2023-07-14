using Common.Atomic.Values;

namespace Models.Components
{
    public sealed class Component_Dispersion
    {
        public float Angle { get; private set; }

        public Component_Dispersion(AtomicVariable<float> angle)
        {
            angle.OnChanged.Subscribe(x => Angle = angle.Value);
            Angle = angle.Value;
        }
    }
}