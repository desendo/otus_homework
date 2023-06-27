using Input;
using Models.Components;
using Services;

namespace Controllers.HeroControllers
{
    public class HeroAttackController : IGameLoadedListener, IStartGameListener, ILostGameListener, IWinGameListener

    {
        private readonly HeroService _heroService;
        private bool _loaded;
        private bool _started;

        public HeroAttackController(InputService inputService, HeroService heroService)
        {
            _heroService = heroService;
            inputService.FireState.Subscribe(HandleFireState);
            inputService.ReloadPressed.Subscribe(HandleReloadRequest);
        }

        private void HandleReloadRequest()
        {
            _heroService.CurrentWeaponEntity.Value?.Get<Component_Reload>().Reload();

        }


        private void HandleFireState(FireState fireState)
        {
            if (!_loaded || !_started) return;

            if(fireState == FireState.Released || fireState == FireState.None)
                _heroService.CurrentWeaponEntity.Value?.Get<Component_Attack>().StopAttack();
            else if(fireState == FireState.Pressed)
                _heroService.CurrentWeaponEntity.Value?.Get<Component_Attack>().Attack();
            else if(fireState == FireState.IsPressing)
                _heroService.CurrentWeaponEntity.Value?.Get<Component_Attack>().ContinueAttack();
        }

        public void OnGameLoaded(bool isLoaded)
        {
            _loaded = isLoaded;
        }

        public void OnStartGame()
        {
            _started = true;
        }

        public void OnLostGame()
        {
            _started = false;
        }

        public void OnWinGame()
        {
            _started = false;
        }
    }
}