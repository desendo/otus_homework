using ReactiveExtension;
using Event = ReactiveExtension.Event;

namespace ItemInventory
{
    public class ItemSlot
    {
        public SlotType Type { get; }
        public string Id { get; }

        private Item _current;

        public Item CurrentItem  => _current;

        public readonly Event OnChange = new Event();
        public readonly Event<Item> OnEquip = new Event<Item>();
        public readonly Event<Item> OnUnEquip = new Event<Item>();
        public ItemSlot(SlotType slotType, string id)
        {
            Type = slotType;
            Id = id;
        }

        public void SetCurrentItem(Item item)
        {
            var was = _current;
            _current = item;
            if (was != item)
            {
                if(was != null)
                    OnUnEquip.Invoke(was);
                if(item != null)
                    OnEquip.Invoke(item);
            }

            OnChange.Invoke();
        }

        public void Clear()
        {
            if(_current != null)
                OnUnEquip.Invoke(_current);

            _current = null;
            OnChange.Invoke();
        }

    }
}