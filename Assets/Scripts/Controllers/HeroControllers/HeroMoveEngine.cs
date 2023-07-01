using Models.Components;
using Services;
using UnityEngine;

namespace Controllers.HeroControllers
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
            var entity = _heroService.HeroEntity.Value;
            entity.Get<Component_ObservedMove>().MoveRequested.Subscribe(OnMoveRequested);
        }
        public void OnLostGame()
        {
            _heroService.HeroEntity.Value.Get<Component_ObservedMove>().MoveRequested.UnSubscribe(OnMoveRequested);
        }

        public void OnWinGame()
        {
            _heroService.HeroEntity.Value.Get<Component_ObservedMove>().MoveRequested.UnSubscribe(OnMoveRequested);
        }
        private void OnMoveRequested(Vector3 moveStep)
        {
            var translateDelta = moveStep;
            //модификаторы скорости передвижения
            _heroService.HeroEntity.Value.Get<Component_ObservedMove>().SetVelocity(translateDelta); 
            //_heroService.HeroEntity.Value.Get<Component_Transform>().Translate(translateDelta);
            _heroService.HeroEntity.Value.Get<Component_Rigidbody>().SetVelocity(translateDelta);
        }
    }
}