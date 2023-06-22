using Common.Atomic.Values;

namespace Models.Components
{
    public sealed class Component_Rotate
    {
        public AtomicVariable<float> RotationSpeed { get; }
        public Component_Rotate(AtomicVariable<float> rotationSpeed)
        {
            RotationSpeed = rotationSpeed;
        }
    }
}