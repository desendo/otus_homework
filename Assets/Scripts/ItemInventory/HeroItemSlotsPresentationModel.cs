using System.Collections.Generic;
using ReactiveExtension;
using Signals;

namespace ItemInventory
{
    public class HeroItemSlotsPresentationModel
    {
        private readonly HeroSlotsService _heroSlotsService;
        private readonly SignalBusService _signalBusService;
        private readonly List<SlotPresentationModel> _slotsPm = new List<SlotPresentationModel>();
        public Event OnChange = new Event();

        public IReadOnlyList<SlotPresentationModel> SlotsPms => _slotsPm;
        public HeroItemSlotsPresentationModel(HeroSlotsService heroSlotsService, SignalBusService signalBusService)
        {
            _heroSlotsService = heroSlotsService;
            _signalBusService = signalBusService;
            _heroSlotsService.OnChange.Subscribe(OnUpdate);
            OnUpdate();
        }

        private void OnUpdate()
        {
            _slotsPm.Clear();
            foreach (var itemSlot in _heroSlotsService.Slots)
            {
                _slotsPm.Add(new SlotPresentationModel(itemSlot, _signalBusService));
            }
            OnChange.Invoke();
        }
    }
}