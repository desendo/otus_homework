namespace ItemInventory
{
    public class HeroSlotsInstaller : IStartGameListener, IFinishGameListener
    {
        private readonly HeroSlotsService _heroSlotsService;

        public HeroSlotsInstaller(HeroSlotsService heroSlotsService)
        {
            _heroSlotsService = heroSlotsService;
        }

        public void OnStartGame()
        {
            AddSlots();
        }

        private void AddSlots()
        {
            var slot1 = new ItemSlot(SlotType.Hand, "r_hand_slot");
            _heroSlotsService.AddSlot(slot1);

            var slot2 = new ItemSlot(SlotType.Hand, "l_hand_slot");
            _heroSlotsService.AddSlot(slot2);

            var slot3 = new ItemSlot(SlotType.Body, "body_slot");
            _heroSlotsService.AddSlot(slot3);

            var slot4 = new ItemSlot(SlotType.Legs, "legs_slot");
            _heroSlotsService.AddSlot(slot4);

            var slot5 = new ItemSlot(SlotType.Amulet, "amulet_slot");
            _heroSlotsService.AddSlot(slot5);

            var slot6 = new ItemSlot(SlotType.Ring, "r_ring_slot");
            _heroSlotsService.AddSlot(slot6);
            var slot7 = new ItemSlot(SlotType.Ring, "l_ring_slot");
            _heroSlotsService.AddSlot(slot7);
        }

        public void OnFinishGame(bool gameWin)
        {
            _heroSlotsService.Clear();
        }
    }
}