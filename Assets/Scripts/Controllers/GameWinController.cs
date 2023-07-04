using Services;
using Signals;

namespace Controllers
{
    public class GameWinController
    {
        private readonly EnemyService _enemyService;
        private readonly SignalBusService _signalBusService;

        public GameWinController(EnemyService enemyService, SignalBusService signalBusService)
        {
            _enemyService = enemyService;
            _signalBusService = signalBusService;
            _enemyService.Killed.OnChanged.Subscribe(x => UpdateState());
            _enemyService.TotalSpawned.OnChanged.Subscribe(x => UpdateState());
        }

        private void UpdateState()
        {
            if (_enemyService.Killed.Value == _enemyService.TotalSpawned.Value && _enemyService.TotalSpawned.Value != 0)
            {
                _signalBusService.Fire(new GameWinRequest());
            }
        }
    }
}