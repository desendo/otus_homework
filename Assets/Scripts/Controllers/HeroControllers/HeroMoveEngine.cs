using Models.Components;
using Services;
using UnityEngine;

namespace Controllers.HeroControllers
{
    public class HeroMoveEngine: IStartGameListener, IFinishGameListener
    {
        private readonly HeroManager _heroManager;

        public HeroMoveEngine(HeroManager heroManager)
        {
            _heroManager = heroManager;
        }

        public void OnStartGame()
        {
            var entity = _heroManager.HeroEntity.Value;
            entity.Get<Component_ObservedMove>().MoveRequested.Subscribe(OnMoveRequested);
        }
        private void OnMoveRequested(Vector3 moveStep)
        {
            var translateDelta = moveStep;
            //модификаторы скорости передвижения
            _heroManager.HeroEntity.Value.Get<Component_ObservedMove>().SetVelocity(translateDelta); 
            //_heroService.HeroEntity.Value.Get<Component_Transform>().Translate(translateDelta);
            _heroManager.HeroEntity.Value.Get<Component_Rigidbody>().SetVelocity(translateDelta);
        }

        public void OnFinishGame(bool gameWin)
        {
            _heroManager.HeroEntity.Value.Get<Component_ObservedMove>().MoveRequested.UnSubscribe(OnMoveRequested);
        }
    }
}