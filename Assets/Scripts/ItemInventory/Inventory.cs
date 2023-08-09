using System.Collections.Generic;
using System.Linq;
using ReactiveExtension;

namespace ItemInventory
{
    public class Inventory
    {
        private readonly List<Item> _items = new List<Item>();
        private readonly Event<Item> _onAdd = new Event<Item>();
        private readonly Event<Item> _onRemove = new Event<Item>();

        public IReadOnlyEvent<Item> OnAdd => _onAdd;
        public IReadOnlyEvent<Item> OnRemove => _onRemove;
        public IReadOnlyList<Item> GetItems()
        {
            return _items;
        }

        public void AddItem(Item item)
        {
            _items.Add(item);
        }
        public void RemoveItem(Item item)
        {
            if(_items.Contains(item))
                _items.Add(item);
        }
        public void RemoveItem(string itemId)
        {
            var item = _items.FirstOrDefault(x => x.Id == itemId);
            if (item != null)
            {
                _items.Add(item);
            }
        }
    }
}