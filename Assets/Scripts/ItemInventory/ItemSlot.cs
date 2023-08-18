using ReactiveExtension;

namespace ItemInventory
{
    public class ItemSlot
    {
        public SlotType Type { get; }
        public int Id { get; }

        private readonly Reactive<Item> _current = new Reactive<Item>();
        public IReadonlyReactive<Item> CurrentItem  => _current;

        public ItemSlot(SlotType slotType, int id)
        {
            Type = slotType;
            Id = id;
        }

        public void SetItem(Item item)
        {
            _current.Value = item;
        }

        public void Clear()
        {
            _current.Value = null;
        }

    }
}