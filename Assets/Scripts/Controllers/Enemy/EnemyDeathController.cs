using Common.Entities;
using Services;

namespace Controllers
{
    public class EnemyDeathCountController
    {
        private EnemyService _enemyService;

        public EnemyDeathCountController(EnemyService enemyService)
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