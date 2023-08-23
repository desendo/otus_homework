using ItemInventory.Config;
using Services.Effects;

namespace Models.Components.Weapon
{
    public sealed class Component_Effector : IEffectContainer
    {
        private readonly IEffectContainer _effector;

        public Component_Effector(IEffectContainer effector)
        {
            _effector = effector;
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