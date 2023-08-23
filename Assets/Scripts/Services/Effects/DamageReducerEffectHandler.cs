using System.Globalization;
using Common.Atomic.Values;
using ItemInventory.Config;

namespace Services.Effects
{
    public class DamageReducerEffectHandler : IEffectHandler
    {
        private readonly AtomicVariable<float> _damageReducer;

        public DamageReducerEffectHandler(AtomicVariable<float> damageReducer)
        {
            _damageReducer = damageReducer;
        }
        public void CancelEffect(Effect effect)
        {

            if (effect.Type == EffectType.ArmorDamageSub)
            {
                if(float.TryParse(effect.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                {
                    _damageReducer.Value -= value;
                }
            }
        }

        public void ApplyEffect(Effect effect)
        {
            if (effect.Type == EffectType.ArmorDamageSub)
            {
                if(float.TryParse(effect.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                {
                    _damageReducer.Value += value;
                }
            }
        }
    }
}