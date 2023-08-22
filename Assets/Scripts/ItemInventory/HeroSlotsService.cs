using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using ItemInventory.Components;
using ReactiveExtension;

namespace ItemInventory
{
    public class HeroSlotsService
    {
        private readonly List<ItemSlot> _slots = new List<ItemSlot>();

        public IReadOnlyList<ItemSlot> Slots => _slots;

        public readonly Event<ItemSlot> OnSlotAdd = new Event<ItemSlot>();
        public readonly Event<ItemSlot> OnSlotRemoved = new Event<ItemSlot>();
        public readonly Event<string> WeaponIdEquip = new Event<string>();
        public readonly Event<string> WeaponIdUnEquip = new Event<string>();
        private readonly List<IDisposable> _disposables = new List<IDisposable>();
        private readonly List<Item> _items = new List<Item>();
        public IReadOnlyList<Item> Items => _items;

        public void Clear()
        {
            _items.Clear();
            foreach (var itemSlot in _slots)
            {
                itemSlot.Clear();
            }
        }

        public void AddSlot(ItemSlot slot)
        {
            _slots.Add(slot);
            OnSlotAdd.Invoke(slot);
            slot.OnEquip.Subscribe(OnSlotEquip).AddTo(_disposables);
            slot.OnUnEquip.Subscribe(OnSlotUnEquip).AddTo(_disposables);
        }

        private void OnSlotUnEquip(Item item)
        {
            if (item.TryGet<ItemComponent_Weapon>(out var weapon))
            {
                WeaponIdUnEquip.Invoke(weapon.WeaponId);
            }
            _items.Remove(item);
        }

        private void OnSlotEquip(Item item)
        {
            if (item.TryGet<ItemComponent_Weapon>(out var weapon))
            {
                WeaponIdEquip.Invoke(weapon.WeaponId);
            }
            _items.Add(item);
        }
    }
}