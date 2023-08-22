using Common.Atomic.Values;
using ItemInventory.Config;

namespace Services.Effects
{
    public class WeaponDamageEffectHandler : IEffectHandler
    {
        private readonly AtomicVariable<float> _damageMultiplier;

        public WeaponDamageEffectHandler(AtomicVariable<float> damageMultiplier)
        {
            _damageMultiplier = damageMultiplier;
        }
        public void CancelEffect(Effect effect)
        {

            if (effect.Type == EffectType.WeaponDamageMult)
            {
                if(float.TryParse(effect.Value, out var mult))
                {
                    _damageMultiplier.Value /= mult;
                }
            }
        }

        public void ApplyEffect(Effect effect)
        {
            if (effect.Type == EffectType.WeaponDamageMult)
            {
                if(float.TryParse(effect.Value, out var mult))
                {
                    _damageMultiplier.Value *= mult;
                }
            }
        }
    }
}