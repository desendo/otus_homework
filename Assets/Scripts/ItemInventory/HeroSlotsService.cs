using System.Collections.Generic;
using System.Linq;
using ReactiveExtension;

namespace ItemInventory
{
    public class HeroSlotsService
    {
        private readonly List<ItemSlot> _slots = new List<ItemSlot>();

        public Event OnChange = new Event();
        public IReadOnlyList<ItemSlot> Slots => _slots;


        public void Clear()
        {
            foreach (var itemSlot in _slots)
            {
                itemSlot.Clear();
            }
        }

        public void AddSlot(ItemSlot slot)
        {
            _slots.Add(slot);
        }
    }
}