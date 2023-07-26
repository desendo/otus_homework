using Input;
using Models.Components;
using Services;

namespace Controllers.HeroControllers
{
    public class HeroAttackController : IGameLoadedListener, IStartGameListener, IFinishGameListener
    {
        private readonly HeroManager _heroManager;
        private bool _loaded;
        private bool _started;

        public HeroAttackController(InputService inputService, HeroManager heroManager)
        {
            _heroManager = heroManager;
            inputService.FireState.Subscribe(HandleFireState);
            inputService.ReloadPressed.Subscribe(HandleReloadRequest);
        }

        private void HandleReloadRequest()
        {
            _heroManager.CurrentWeaponEntity.Value?.Get<Component_Reload>().Reload();

        }
        private void HandleFireState(FireState fireState)
        {
            if (!_loaded || !_started) return;

            if(fireState == FireState.Released || fireState == FireState.None)
                _heroManager.CurrentWeaponEntity.Value?.Get<Component_Attack>().StopAttack();
            else if(fireState == FireState.Pressed)
                _heroManager.CurrentWeaponEntity.Value?.Get<Component_Attack>().Attack();
            else if(fireState == FireState.IsPressing)
                _heroManager.CurrentWeaponEntity.Value?.Get<Component_Attack>().ContinueAttack();
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