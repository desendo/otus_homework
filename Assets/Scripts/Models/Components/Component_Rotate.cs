using Common.Atomic.Values;

namespace Models.Components
{
    public sealed class Component_Rotate
    {
        public float RotationSpeed { get; private set; }
        public Component_Rotate(AtomicVariable<float> rotationSpeed)
        {
            RotationSpeed = rotationSpeed.Value;
            rotationSpeed.OnChanged.Subscribe(x => RotationSpeed = x);
        }
    }
}