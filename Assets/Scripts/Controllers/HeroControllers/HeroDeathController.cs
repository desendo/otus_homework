using System;
using Common.Entities;
using Models.Components;
using Services;
using Signals;

namespace Controllers.HeroControllers
{
    public class HeroDeathController : IStartGameListener, ILostGameListener, IWinGameListener
    {
        private readonly SignalBusService _signalBusService;
        private readonly HeroService _heroService;
        private IDisposable _sub;

        public HeroDeathController(SignalBusService signalBusService, HeroService heroService)
        {
            _signalBusService = signalBusService;
            _heroService = heroService;
        }


        public void OnStartGame()
        {
            _sub?.Dispose();
            _sub = _heroService.HeroEntity.Value.Get<Component_Death>().OnDeath.Subscribe(OnDeath);
        }

        private void OnDeath(IEntity obj)
        {
            _signalBusService.Fire(new GameOverRequest());
        }

        public void OnLostGame()
        {
            _sub?.Dispose();
        }

        public void OnWinGame()
        {
            _sub?.Dispose();
        }
    }
}