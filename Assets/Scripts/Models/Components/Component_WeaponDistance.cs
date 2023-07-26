using Common.Atomic.Values;

namespace Models.Components
{
    public sealed class Component_WeaponRange
    {
        private float _maxRange;

        public Component_WeaponRange(AtomicVariable<float> maxRange)
        {
            _maxRange = maxRange.Value;
            maxRange.OnChanged.Subscribe(x => _maxRange = x);
        }

        public bool IsInRange(float squareDistance)
        {
            var val = _maxRange;
            val *= val;
            return val > squareDistance;
        }
    }
}