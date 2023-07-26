using System;
using Common.Entities;
using Models.Components;
using Services;
using Signals;

namespace Controllers.HeroControllers
{
    public class HeroDeathController : IStartGameListener, IFinishGameListener
    {
        private readonly SignalBusService _signalBusService;
        private readonly HeroManager _heroManager;
        private IDisposable _sub;

        public HeroDeathController(SignalBusService signalBusService, HeroManager heroManager)
        {
            _signalBusService = signalBusService;
            _heroManager = heroManager;
        }


        public void OnStartGame()
        {
            _sub?.Dispose();
            _sub = _heroManager.HeroEntity.Value.Get<Component_Death>().OnDeath.Subscribe(OnDeath);
        }

        private void OnDeath(IEntity obj)
        {
            _signalBusService.Fire(new GameOverRequest());
        }


        public void OnFinishGame(bool gameWin)
        {
            _sub?.Dispose();
        }
    }
}