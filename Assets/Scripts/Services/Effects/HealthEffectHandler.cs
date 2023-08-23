using System.Globalization;
using Common.Atomic.Values;
using ItemInventory.Config;

namespace Services.Effects
{
    public class HealthEffectHandler : IEffectHandler
    {
        private readonly AtomicVariable<float> _maxHealth;
        private readonly AtomicVariable<float> _currentHealth;

        public HealthEffectHandler(AtomicVariable<float> maxHealth, AtomicVariable<float> currentHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = currentHealth;
        }
        public void CancelEffect(Effect effect)
        {

            if (effect.Type == EffectType.MaxHealthAdd)
            {
                if(float.TryParse(effect.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                {
                    var normalized = _currentHealth.Value / _maxHealth.Value;
                    _maxHealth.Value -= value;
                    _currentHealth.Value = _maxHealth.Value * normalized;
                }
            }
        }

        public void ApplyEffect(Effect effect)
        {
            if (effect.Type == EffectType.MaxHealthAdd)
            {
                if(float.TryParse(effect.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                {
                    var normalized = _currentHealth.Value / _maxHealth.Value;
                    _maxHealth.Value += value;
                    _currentHealth.Value = _maxHealth.Value * normalized;

                }
            }
        }
    }
}