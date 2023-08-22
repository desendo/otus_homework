using System.Collections.Generic;
using ItemInventory.Config;

namespace ItemInventory.Components
{
    public class ItemComponent_Effect
    {
        public List<Effect> Effects { get; }

        public ItemComponent_Effect(List<Effect> effects)
        {
            Effects = effects;
        }
    }
}