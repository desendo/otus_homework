using Input;
using Services;

namespace Controllers.HeroControllers
{
    public class SwitchWeaponController
    {
        private readonly InputService _inputService;
        private readonly WeaponManager _weaponManager;
        private bool _loaded;
        private bool _started;

        public SwitchWeaponController(InputService inputService, WeaponManager weaponManager)
        {
            _inputService = inputService;
            _weaponManager = weaponManager;
            inputService.DigitPressed.Subscribe(OnDigitPressed);
        }

        private void OnDigitPressed(int obj)
        {
            var count = _weaponManager.CollectedWeapons.Count;
            if(obj > count)
                return;

            _weaponManager.SetWeaponSelected(obj - 1);
        }
    }
}