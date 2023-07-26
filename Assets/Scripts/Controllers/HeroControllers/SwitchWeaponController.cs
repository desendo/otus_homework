using Input;
using Services;

namespace Controllers.HeroControllers
{
    public class SwitchWeaponController
    {
        private readonly InputService _inputService;
        private readonly HeroManager _heroManager;
        private bool _loaded;
        private bool _started;

        public SwitchWeaponController(InputService inputService, HeroManager heroManager)
        {
            _inputService = inputService;
            _heroManager = heroManager;
            inputService.DigitPressed.Subscribe(OnDigitPressed);
        }

        private void OnDigitPressed(int obj)
        {
            var count = _heroManager.CollectedWeapons.Count;
            if(obj > count)
                return;

            _heroManager.SetWeaponSelected(obj - 1);
        }
    }
}