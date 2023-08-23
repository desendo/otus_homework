using System.Globalization;
using Common.Atomic.Values;
using ItemInventory.Config;

namespace Services.Effects
{
    public class EvasionEffectHandler : IEffectHandler
    {
        private readonly AtomicVariable<float> _evasionChance;

        public EvasionEffectHandler(AtomicVariable<float> evasionChance)
        {
            _evasionChance = evasionChance;
        }
        public void CancelEffect(Effect effect)
        {

            if (effect.Type == EffectType.Evasion)
            {
                if(float.TryParse(effect.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                {

                    _evasionChance.Value -= value;
                }
            }
        }

        public void ApplyEffect(Effect effect)
        {
            if (effect.Type == EffectType.Evasion)
            {
                if(float.TryParse(effect.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                {
                    _evasionChance.Value += value;

                }
            }
        }
    }
}