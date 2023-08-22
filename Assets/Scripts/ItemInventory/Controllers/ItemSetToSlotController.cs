using System;
using System.Linq;
using ItemInventory.Components;
using Signals;

namespace ItemInventory.Controllers
{
    public class ItemSetToSlotController
    {
        private readonly HeroSlotsService _heroSlotsService;
        private readonly Inventory _itemInventory;

        public ItemSetToSlotController(HeroSlotsService heroSlotsService, Inventory itemInventory,
            SignalBusService signalBusService)
        {
            _heroSlotsService = heroSlotsService;
            _itemInventory = itemInventory;
            signalBusService.Subscribe<SetItemToSlotRequest>(HandleDropToSlot);
            signalBusService.Subscribe<SetItemToInventory>(HandleDropToInventory);
        }

        private void HandleDropToInventory(SetItemToInventory obj)
        {
            var slot = _heroSlotsService.Slots.FirstOrDefault(x=> x.CurrentItem !=  null && x.CurrentItem.Id == obj.ItemId);
            if (slot?.CurrentItem != null )
            {
                _itemInventory.AddItem(slot.CurrentItem);
                slot.SetCurrentItem(null);
            }
        }

        private void HandleDropToSlot(SetItemToSlotRequest obj)
        {
            var item = _itemInventory.Items.FirstOrDefault(x=>x.Id == obj.ItemId);
            var slot = _heroSlotsService.Slots.FirstOrDefault(x=>x.Id == obj.SlotId);
            var slotComponent = item?.Get<ItemComponent_SlotType>();
            if (slotComponent != null && slot != null && slotComponent.SlotType == slot.Type)
            {
                var currentItem = slot.CurrentItem;

                if(currentItem != null)
                {
                    slot.SetCurrentItem(null);

                    _itemInventory.AddItem(currentItem);
                }
                slot.SetCurrentItem(item);

                _itemInventory.RemoveItem(item);

            }
        }
    }
}