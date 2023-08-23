using System.Globalization;
using Common.Atomic.Values;
using ItemInventory.Config;

namespace Services.Effects
{
    public class MoveSpeedEffectHandler : IEffectHandler
    {
        private readonly AtomicVariable<float> _speedMult;

        public MoveSpeedEffectHandler(AtomicVariable<float> speedMult)
        {
            _speedMult = speedMult;
        }
        public void CancelEffect(Effect effect)
        {

            if (effect.Type == EffectType.MoveSpeedMult)
            {
                if(float.TryParse(effect.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                {

                    _speedMult.Value /= value;
                }
            }
        }

        public void ApplyEffect(Effect effect)
        {
            if (effect.Type == EffectType.MoveSpeedMult)
            {
                if(float.TryParse(effect.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                {
                    _speedMult.Value *= value;

                }
            }
        }
    }
}