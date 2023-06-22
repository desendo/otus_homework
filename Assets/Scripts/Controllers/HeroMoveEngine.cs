using Models.Components;
using Services;
using UnityEngine;

namespace Controllers
{
    public class HeroMoveEngine: IStartGameListener, ILostGameListener, IWinGameListener
    {
        private readonly HeroService _heroService;

        public HeroMoveEngine(HeroService heroService)
        {
            _heroService = heroService;
        }

        public void OnStartGame()
        {
            var entity = _heroService.HeroEntity;
            entity.Get<Component_Move>().MoveRequested += OnMoveRequested;
        }
        public void OnLostGame()
        {
            _heroService.HeroEntity.Get<Component_Move>().MoveRequested -= OnMoveRequested;
        }

        public void OnWinGame()
        {
            _heroService.HeroEntity.Get<Component_Move>().MoveRequested -= OnMoveRequested;
        }
        private void OnMoveRequested(Vector3 moveStep)
        {
            var translateDelta = moveStep;
            //модификаторы скорости передвижения
            _heroService.HeroEntity.Get<Component_Move>().SetVelocity(translateDelta);
            _heroService.HeroEntity.Get<Component_Transform>().Translate(translateDelta);
        }
    }
}