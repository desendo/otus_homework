using ItemInventory.Config;
using Services.Effects;

namespace Models.Declarative
{
    public class HeroEffectModel : IEffectContainer
    {
        private readonly Effector _effector = new Effector();

        public void Construct(Life life, MoveModel moveModel)
        {
            _effector.AddHandler(new DamageReducerEffectHandler(life.DamageReducer));
            _effector.AddHandler(new HealthEffectHandler(life.MaxHitPoints, life.HitPoints));
            _effector.AddHandler(new MoveSpeedEffectHandler(moveModel.SpeedMult));
            _effector.AddHandler(new EvasionEffectHandler(life.EvasionChance));
        }

        public void AddEffect(Effect effect)
        {
            _effector.AddEffect(effect);
        }

        public void RemoveEffect(Effect effect)
        {
            _effector.RemoveEffect(effect);
        }
    }
}