using System;
using System.Collections.Generic;
using ItemInventory;
using ReactiveExtension;
using Signals;
using UnityEngine;

namespace UI.PresentationModel
{
    public class ItemPresentationModel
    {
        public Reactive<string> Name = new Reactive<string>();
        public Reactive<string> Count = new Reactive<string>();
        public Reactive<string> Description = new Reactive<string>();
        public Reactive<Sprite> Icon = new Reactive<Sprite>();
        public Item Item { get; }

        private readonly Reactive<Item> _onItemDrag;

        public ItemPresentationModel(Item item, Reactive<Item> onItemDrag)
        {
            Icon.Value = item.Icon;
            Count.Value = "";
            Description.Value = item.Description;
            Name.Value = item.Name;
            Item = item;
            _onItemDrag = onItemDrag;
        }

        public void SetDragging(bool isDragging)
        {
            _onItemDrag.Value = isDragging ? Item : null;
        }
    }

    public class InventoryPresentationModel
    {
        public readonly Event<ItemPresentationModel> OnAdd = new Event<ItemPresentationModel>();
        public readonly Event<ItemPresentationModel> OnRemove = new Event<ItemPresentationModel>();
        public IReadOnlyList<ItemPresentationModel> ItemPms => _itemPresentationModels;
        public readonly Reactive<Item> ItemDrag = new Reactive<Item>();

        private readonly List<ItemPresentationModel> _itemPresentationModels = new List<ItemPresentationModel>();

        public InventoryPresentationModel(Inventory inventory)
        {
            var items = inventory.GetItems();
            foreach (var item in items)
            {
                var itemPm = new ItemPresentationModel(item, ItemDrag);
                _itemPresentationModels.Add(itemPm);
                OnAdd.Invoke(itemPm);
            }

            inventory.OnAdd.Subscribe(ProcessOnAdd);
            inventory.OnRemove.Subscribe(ProcessOnRemove);
        }

        private void ProcessOnAdd(Item item)
        {
            var itemPm = new ItemPresentationModel(item, ItemDrag);
            _itemPresentationModels.Add(itemPm);
            OnAdd.Invoke(itemPm);
        }

        private void ProcessOnRemove(Item item)
        {
            ItemPresentationModel presentationModel = null;
            foreach (var itemPresentationModel in _itemPresentationModels)
            {
                if (item == itemPresentationModel.Item)
                {
                    presentationModel = itemPresentationModel;
                    break;
                }
            }

            if (presentationModel != null)
            {
                _itemPresentationModels.Remove(presentationModel);
                OnRemove.Invoke(presentationModel);
            }
        }
    }
}