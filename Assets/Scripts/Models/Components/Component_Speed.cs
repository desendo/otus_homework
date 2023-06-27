using Common.Atomic.Values;

namespace Models.Components
{
    public sealed class Component_Speed
    {
        private readonly AtomicVariable<float> _speed;
        public float Speed
        {
            get => _speed.Value;
            set => _speed.Value = value;
        }


        public Component_Speed(AtomicVariable<float> speed)
        {
            _speed = speed;
            Speed = speed.Value;
        }
    }
}