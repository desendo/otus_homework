using System;
using System.Collections.Generic;
using Common;
using Common.Entities;
using ItemInventory;
using Models.Components;
using ReactiveExtension;
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

        public ItemPresentationModel(Item item)
        {
            Item = item;
        }
    }

    public class InventoryPresentationModel
    {
        public readonly Event<ItemPresentationModel> OnAdd = new Event<ItemPresentationModel>();
        public readonly Event<ItemPresentationModel> OnRemove = new Event<ItemPresentationModel>();
        public IReadOnlyList<ItemPresentationModel> ItemPms => _itemPresentationModels;

        private readonly List<ItemPresentationModel> _itemPresentationModels  = new List<ItemPresentationModel>();
        private readonly List<IDisposable> _subs = new List<IDisposable>();
        public InventoryPresentationModel(Inventory inventory)
        {
            var items = inventory.GetItems();
            foreach (var item in items)
            {
                var itemPm = new ItemPresentationModel(item);
                _itemPresentationModels.Add(itemPm);
                OnAdd.Invoke(itemPm);
            }
            inventory.OnAdd.Subscribe(ProcessOnAdd).AddTo(_subs);
            inventory.OnRemove.Subscribe(ProcessOnRemove).AddTo(_subs);
        }
        private void ProcessOnAdd(Item item)
        {
            var itemPm = new ItemPresentationModel(item);
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

            if(presentationModel != null)
                OnRemove.Invoke(presentationModel);
        }

    }
}