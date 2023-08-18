using Signals;
using UI.PresentationModel;
using UnityEngine;

namespace ItemInventory.Controllers
{
    public class ItemSetToSlotService
    {
        private readonly HeroItemSlotsPresentationModel _slotsPresentationModel;
        private readonly SignalBusService _signalBusService;
        private readonly InventoryPresentationModel _inventoryPresentationModel;

        public ItemSetToSlotService(HeroItemSlotsPresentationModel slotsPresentationModel,
            SignalBusService signalBusService,
            InventoryPresentationModel inventoryPresentationModel)
        {
            _slotsPresentationModel = slotsPresentationModel;
            _signalBusService = signalBusService;
            _inventoryPresentationModel = inventoryPresentationModel;
            _signalBusService.Subscribe<SetItemToSlotRequest>(HandleDrop);
        }

        private void HandleDrop(SetItemToSlotRequest obj)
        {
            var item = _inventoryPresentationModel.ItemDrag.Value;
            if (item == null)
            {
                obj.Fail.Invoke();
                return;
            }
            else
            {
                var currentSlotItem = obj.Slot.CurrentItem.Value;
                if (currentSlotItem != null)
                {
                    obj.Fail.Invoke();
                    return;
                }
                else
                {
                    obj.Slot.SetItem(_inventoryPresentationModel.ItemDrag.Value);
                    obj.Success.Invoke();
                    return;
                }
            }
        } }
}