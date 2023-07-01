using Common.Entities;
using Services;

namespace Controllers
{
    public class EnemyDeathController
    {
        private EnemyService _enemyService;

        public EnemyDeathController(EnemyService enemyService)
        {
            enemyService.OnDeath.Subscribe(OnDeath);
            _enemyService = enemyService;
        }

        private void OnDeath(IEntity obj)
        {
            _enemyService.Killed.Value++;
        }
    }
}