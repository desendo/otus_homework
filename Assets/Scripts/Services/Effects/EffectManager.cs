using System.Collections.Generic;
using ItemInventory.Config;

namespace Services.Effects
{
    public class EffectManager: IStartGameListener, IFinishGameListener
    {

        private readonly List<IEffectContainer> _effectContainers = new List<IEffectContainer>();
        private readonly List<Effect> _effects = new List<Effect>();

        public EffectManager(HeroService heroService, WeaponEffectManager weaponEffectManager)
        {

            _effectContainers.Add(weaponEffectManager);
            _effectContainers.Add(heroService);
        }

        public void AddEffect(Effect effect)
        {
            foreach (var effectContainer in _effectContainers)
            {
                effectContainer.AddEffect(effect);
            }
        }
        public void RemoveEffect(Effect effect)
        {
            _effects.Remove(effect);
            foreach (var effectContainer in _effectContainers)
            {
                effectContainer.RemoveEffect(effect);
            }
        }

        public void OnStartGame()
        {
            ClearAll();
        }

        public void OnFinishGame(bool gameWin)
        {
            ClearAll();
        }
        private void ClearAll()
        {
            foreach (var effect in _effects)
            {
                foreach (var effectContainer in _effectContainers)
                {
                    effectContainer.RemoveEffect(effect);
                }
            }

            _effects.Clear();
        }
    }
}