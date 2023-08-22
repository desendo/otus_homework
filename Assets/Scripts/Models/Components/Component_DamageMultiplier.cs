using Common.Atomic.Values;

namespace Models.Components
{
    public class Component_DamageMultiplier
    {
        private readonly AtomicVariable<float> _multiplier;

        public Component_DamageMultiplier(AtomicVariable<float> multiplier)
        {

            _multiplier = multiplier;
        }

        public void SetMultiplier(float val)
        {
            _multiplier.Value = val;
        }
    }
}