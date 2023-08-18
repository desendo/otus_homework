using System;
using ReactiveExtension;
using Signals;
using UnityEngine;

namespace ItemInventory
{
    public class SlotPresentationModel
    {
        private readonly ItemSlot _slot;
        private SignalBusService _signalBusService;
        public SlotType SlotType { get;  }

        public SlotPresentationModel(ItemSlot slot, SignalBusService signalBusService)
        {
            _slot = slot;
            _signalBusService = signalBusService;
            SlotType = _slot.Type;
        }

        public void DropRequest(Action success, Action fail)
        {
            _signalBusService.Fire(new SetItemToSlotRequest(_slot, success, fail));
        }
    }
}