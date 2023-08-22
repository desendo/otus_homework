using System.Collections.Generic;
using ReactiveExtension;
using UI.PresentationModel;

namespace ItemInventory.UI.PresentationModel
{
    public class InventoryPresentationModel
    {
        public readonly Event<ItemPresentationModel> OnAdd = new Event<ItemPresentationModel>();
        public readonly Event<ItemPresentationModel> OnRemove = new Event<ItemPresentationModel>();
        public IReadOnlyList<ItemPresentationModel> ItemPms => _itemPresentationModels;
        private readonly List<ItemPresentationModel> _itemPresentationModels = new List<ItemPresentationModel>();

        public InventoryPresentationModel(Inventory inventory)
        {
            foreach (var item in inventory.Items)
            {
                var itemPm = new ItemPresentationModel(item);
                _itemPresentationModels.Add(itemPm);
                OnAdd.Invoke(itemPm);
            }

            inventory.OnAdd.Subscribe(ProcessOnAdd);
            inventory.OnRemove.Subscribe(ProcessOnRemove);
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

            if (presentationModel != null)
            {
                _itemPresentationModels.Remove(presentationModel);
                OnRemove.Invoke(presentationModel);
            }
        }
    }
}