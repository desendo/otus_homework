using Common.Atomic.Values;

namespace Models.Components
{
    public sealed class Component_WeaponRange
    {
        private AtomicVariable<float> _maxRange;

        public Component_WeaponRange(AtomicVariable<float> maxRange)
        {
            _maxRange = maxRange;
        }

        public bool IsInRange(float squareDistance)
        {
            var val = _maxRange.Value;
            val *= val;
            return val > squareDistance;
        }
    }
}