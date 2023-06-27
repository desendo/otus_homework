using Input;
using Services;

namespace Controllers.HeroControllers
{
    public class SwitchWeaponController
    {
        private readonly InputService _inputService;
        private readonly HeroService _heroService;
        private bool _loaded;
        private bool _started;

        public SwitchWeaponController(InputService inputService, HeroService heroService)
        {
            _inputService = inputService;
            _heroService = heroService;
            inputService.DigitPressed.Subscribe(OnDigitPressed);
        }

        private void OnDigitPressed(int obj)
        {
            var count = _heroService.CollectedWeapons.Count;
            if(obj > count)
                return;

            _heroService.SetWeaponSelected(obj - 1);
        }
    }
}