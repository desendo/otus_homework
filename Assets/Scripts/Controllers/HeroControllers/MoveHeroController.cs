using Input;
using Models.Components;
using Services;
using UnityEngine;

namespace Controllers.HeroControllers
{
    public class MoveHeroController : IGameLoadedListener, IStartGameListener, IFinishGameListener, IFixedUpdate
    {
        private readonly InputService _inputService;
        private readonly HeroService _heroService;
        private bool _loaded;
        private bool _started;

        public MoveHeroController(InputService inputService, HeroService heroService)
        {
            _inputService = inputService;
            _heroService = heroService;
        }

        public void FixedUpdate(float dt)
        {
            if (!_loaded || !_started) return;

            var dir = (Vector3)_inputService.MoveDirection.normalized;
            var componentMove = _heroService.HeroEntity.Value.Get<Component_ObservedMove>();
            dir = new Vector3(dir.x, 0, dir.y);
            componentMove.Move(dir);
        }
        public void OnGameLoaded(bool isLoaded)
        {
            _loaded = isLoaded;
        }

        public void OnStartGame()
        {
            _started = true;
        }
        public void OnFinishGame(bool gameWin)
        {
            _started = false;
        }
    }
}