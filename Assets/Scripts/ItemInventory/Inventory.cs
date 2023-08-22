using System.Collections.Generic;
using System.Linq;
using ReactiveExtension;
using UnityEngine;

namespace ItemInventory
{
    public class Inventory
    {
        private readonly List<Item> _items = new List<Item>();
        private readonly Event<Item> _onAdd = new Event<Item>();
        private readonly Event<Item> _onRemove = new Event<Item>();

        public IReadOnlyEvent<Item> OnAdd => _onAdd;
        public IReadOnlyEvent<Item> OnRemove => _onRemove;
        public IReadOnlyList<Item> Items => _items;

        public void AddItem(Item item)
        {
            _items.Add(item);
            _onAdd.Invoke(item);

        }

        public void RemoveItem(string itemId)
        {
            var item = _items.FirstOrDefault(x => x.Id == itemId);
            if (item != null)
            {
                _items.Remove(item);
                _onRemove.Invoke(item);
            }

        }
        public void RemoveItem(Item item)
        {
            if (item != null && _items.Contains(item))
            {
                _items.Remove(item);
                _onRemove.Invoke(item);
            }

        }
        public void Clear()
        {
            var cache = _items.ToList();
            foreach (var item in cache)
            {
                RemoveItem(item);
            }
        }
    }
}