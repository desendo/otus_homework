using Common.Atomic.Values;

namespace Models.Components
{
    public sealed class Component_Dispersion
    {
        public readonly AtomicVariable<float> Angle;

        public Component_Dispersion(AtomicVariable<float> angle)
        {
            Angle = angle;
        }
    }
}