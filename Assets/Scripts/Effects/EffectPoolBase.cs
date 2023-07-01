using System;
using Pool;

namespace Effects
{
    public class EffectPoolBase : PoolBase<Effect>
    {
        public override Effect Spawn(Action<Effect> callbackBeforeAwake = null)
        {
            var effect = base.Spawn(callbackBeforeAwake);
            effect.Setup(this);
            return effect;
        }
    }
}