using Common.Atomic.Values;

namespace Models.Components
{
    public sealed class Component_Speed
    {
        private readonly AtomicVariable<float> _speed;
        private readonly AtomicVariable<float> _speedMult;
        public float Speed
        {
            get => _speed.Value * _speedMult.Value;
            set => _speed.Value = value;
        }


        public Component_Speed(AtomicVariable<float> speed, AtomicVariable<float> multiplier = null)
        {
            _speed = speed;
            if(multiplier == null)
                _speedMult = new AtomicVariable<float>(1);
            else
            {
                _speedMult = multiplier;
            }
            Speed = speed.Value;
        }

    }
}