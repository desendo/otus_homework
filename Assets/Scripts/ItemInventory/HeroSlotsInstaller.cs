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
            _heroSlotsService.OnChange.Invoke();
        }

        private void AddSlots()
        {
            var slot1 = new ItemSlot(SlotType.Hand, 0);
            _heroSlotsService.AddSlot(slot1);

            var slot2 = new ItemSlot(SlotType.Hand, 1);
            _heroSlotsService.AddSlot(slot2);

            var slot3 = new ItemSlot(SlotType.Body, 2);
            _heroSlotsService.AddSlot(slot3);

            var slot4 = new ItemSlot(SlotType.Legs, 3);
            _heroSlotsService.AddSlot(slot4);

            var slot5 = new ItemSlot(SlotType.Amulet, 4);
            _heroSlotsService.AddSlot(slot5);

            var slot6 = new ItemSlot(SlotType.Ring, 5);
            _heroSlotsService.AddSlot(slot6);
        }

        public void OnFinishGame(bool gameWin)
        {
            _heroSlotsService.Clear();
            _heroSlotsService.OnChange.Invoke();

        }
    }
}