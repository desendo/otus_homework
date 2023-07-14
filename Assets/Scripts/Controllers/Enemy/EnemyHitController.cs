using Models.Components;
using Services;

namespace Controllers
{
    //тут почти ецс система
    public class EnemyAttackController: IUpdate, IStartGameListener, IFinishGameListener
    {
        private readonly EnemyService _enemyService;
        private readonly HeroService _heroService;
        private bool _gameStarted;

        public EnemyAttackController(EnemyService enemyService, HeroService heroService)
        {
            _enemyService = enemyService;
            _heroService = heroService;
        }


        public void Update(float dt)
        {
            if(!_gameStarted || _heroService.HeroEntity.Value == null)
                return;

            var heroPos = _heroService.HeroEntity.Value.Get<Component_Transform>().RootTransform.position;

            foreach (var unit in _enemyService.Units)
            {
                var componentActive = unit.Get<Component_IsActive>();
                if(!componentActive.IsActive.Value)
                    continue;

                var componentAttack = unit.Get<Component_Attack>();
                var componentRange = unit.Get<Component_WeaponRange>();
                var componentTransform = unit.Get<Component_Transform>();

                if(componentRange.IsInRange((heroPos - componentTransform.RootTransform.position).sqrMagnitude))
                {
                    componentAttack.Attack();
                }
            }
        }

        public void OnStartGame()
        {
            _gameStarted = true;

        }

        public void OnFinishGame(bool gameWin)
        {
            _gameStarted = false;
        }
    }
}