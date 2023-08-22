using System.Collections.Generic;
using System.Linq;
using ReactiveExtension;
using Signals;

namespace ItemInventory.UI.PresentationModel
{
    public class HeroItemSlotsPresentationModel
    {
        private readonly HeroSlotsService _heroSlotsService;
        private readonly SignalBusService _signalBusService;
        private readonly List<SlotPresentationModel> _slotsPm = new List<SlotPresentationModel>();
        public readonly Event<SlotPresentationModel> OnSlotPmAdd = new Event<SlotPresentationModel>();
        public readonly Event<SlotPresentationModel> OnSlotPmRemoved = new Event<SlotPresentationModel>();
        public IReadOnlyList<SlotPresentationModel> SlotsPms => _slotsPm;
        public HeroItemSlotsPresentationModel(HeroSlotsService heroSlotsService, SignalBusService signalBusService)
        {
            _heroSlotsService = heroSlotsService;
            _signalBusService = signalBusService;
            _heroSlotsService.OnSlotAdd.Subscribe(OnSlotAdd);
            _heroSlotsService.OnSlotRemoved.Subscribe(OnSlotRemoved);
            OnUpdate();
        }

        private void OnSlotRemoved(ItemSlot obj)
        {
            var pm = _slotsPm.FirstOrDefault(x => x.Id == obj.Id);
            if (pm != null)
            {
                _slotsPm.Remove(pm);
                OnSlotPmRemoved.Invoke(pm);
            }
        }

        private void OnSlotAdd(ItemSlot obj)
        {
            var pm = new SlotPresentationModel(obj);
            _slotsPm.Add(pm);
            OnSlotPmAdd.Invoke(pm);
        }

        private void OnUpdate()
        {
            _slotsPm.Clear();
            foreach (var itemSlot in _heroSlotsService.Slots)
            {
                _slotsPm.Add(new SlotPresentationModel(itemSlot));
            }
        }
    }
}