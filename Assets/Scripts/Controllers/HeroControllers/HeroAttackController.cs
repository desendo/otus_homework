using Input;
using Models.Components;
using Services;

namespace Controllers.HeroControllers
{
    public class HeroAttackController : IGameLoadedListener, IStartGameListener, IFinishGameListener
    {
        private readonly WeaponManager _weaponManager;
        private bool _loaded;
        private bool _started;

        public HeroAttackController(InputService inputService, WeaponManager weaponManager)
        {
            _weaponManager = weaponManager;
            inputService.FireState.Subscribe(HandleFireState);
            inputService.ReloadPressed.Subscribe(HandleReloadRequest);
        }

        private void HandleReloadRequest()
        {
            _weaponManager.CurrentWeaponEntity.Value?.Get<Component_Reload>().Reload();

        }
        private void HandleFireState(FireState fireState)
        {
            if (!_loaded || !_started) return;

            if(fireState == FireState.Released || fireState == FireState.None)
                _weaponManager.CurrentWeaponEntity.Value?.Get<Component_Attack>().StopAttack();
            else if(fireState == FireState.Pressed)
                _weaponManager.CurrentWeaponEntity.Value?.Get<Component_Attack>().Attack();
            else if(fireState == FireState.IsPressing)
                _weaponManager.CurrentWeaponEntity.Value?.Get<Component_Attack>().ContinueAttack();
        }

        public void OnGameLoaded(bool isLoaded)
        {
            _loaded = isLoaded;
        }

        public void OnStartGame()
        {
            _started = true;
        }

        public void OnFinishGame(bool isWin)
        {
            _started = false;
        }
    }
}