using System.Collections.Generic;
using ItemInventory.Config;

namespace Services.Effects
{
    public interface IEffectHandler
    {
        void CancelEffect(Effect effect);
        void ApplyEffect(Effect effect);
    }
    public interface IEffectContainer
    {
        void AddEffect(Effect effect);
        void RemoveEffect(Effect effect);
    }
    public class Effector : IEffectContainer
    {
        private readonly List<Effect> _effects = new List<Effect>();
        private readonly List<IEffectHandler> _handlers = new List<IEffectHandler>();

        public void AddHandler(IEffectHandler effectHandler)
        {
            _handlers.Add(effectHandler);
        }

        public void AddEffect(Effect effect)
        {
            _effects.Add(effect);
            foreach (var effectHandler in _handlers)
            {
                effectHandler.ApplyEffect(effect);
            }
        }
        public void RemoveEffect(Effect effect)
        {
            _effects.Remove(effect);
            foreach (var effectHandler in _handlers)
            {
                effectHandler.CancelEffect(effect);
            }
        }
    }
}