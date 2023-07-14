using Models.Components;
using Services;
using UnityEngine;

namespace Controllers.HeroControllers
{
    public class HeroMoveEngine: IStartGameListener, IFinishGameListener
    {
        private readonly HeroService _heroService;

        public HeroMoveEngine(HeroService heroService)
        {
            _heroService = heroService;
        }

        public void OnStartGame()
        {
            var entity = _heroService.HeroEntity.Value;
            entity.Get<Component_ObservedMove>().MoveRequested.Subscribe(OnMoveRequested);
        }
        private void OnMoveRequested(Vector3 moveStep)
        {
            var translateDelta = moveStep;
            //модификаторы скорости передвижения
            _heroService.HeroEntity.Value.Get<Component_ObservedMove>().SetVelocity(translateDelta); 
            //_heroService.HeroEntity.Value.Get<Component_Transform>().Translate(translateDelta);
            _heroService.HeroEntity.Value.Get<Component_Rigidbody>().SetVelocity(translateDelta);
        }

        public void OnFinishGame(bool gameWin)
        {
            _heroService.HeroEntity.Value.Get<Component_ObservedMove>().MoveRequested.UnSubscribe(OnMoveRequested);
        }
    }
}