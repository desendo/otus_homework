using ReactiveExtension;
using UI.PresentationModel;

namespace ItemInventory.UI.PresentationModel
{
    public class SlotPresentationModel
    {
        public SlotType SlotType { get;  }
        public string Id { get; private set; }
        private ItemSlot _slot;
        public  ItemPresentationModel CurrentItemPm;
        public Event OnChange = new Event();

        public SlotPresentationModel(ItemSlot slot)
        {
            Id = slot.Id;
            SlotType = slot.Type;
            _slot = slot;
            _slot.OnChange.Subscribe(UpdateSlotPm);
            UpdateSlotPm();
        }

        private void UpdateSlotPm()
        {
            if (_slot.CurrentItem == null)
            {
                CurrentItemPm = null;
                OnChange.Invoke();
            }
            else
            {
                var newPm = new ItemPresentationModel(_slot.CurrentItem);
                CurrentItemPm = newPm;
                OnChange.Invoke();
            }
        }
    }
}