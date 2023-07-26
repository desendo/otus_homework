using Input;
using Models.Components;
using Services;
using UnityEngine;

namespace Controllers.HeroControllers
{
    public class RotateHeroController : IGameLoadedListener, IStartGameListener, IFinishGameListener, IUpdate
    {
        private readonly InputService _inputService;
        private readonly HeroManager _heroManager;
        private bool _loaded;
        private bool _started;
        private Camera _camera;

        public RotateHeroController(InputService inputService, HeroManager heroManager)
        {
            _inputService = inputService;
            _heroManager = heroManager;
        }

        public void Update(float dt)
        {
            if (!_loaded || !_started) return;

            var root = _heroManager.HeroEntity.Value.Get<Component_Transform>().RootTransform;
            var rotationSpeedValue = _heroManager.HeroEntity.Value.Get<Component_Rotate>().RotationSpeed;
            var attackPivotTransform = _heroManager.HeroEntity.Value.Get<Component_AttackPivot>().AttackPoint;

            var height = new Vector3(0,attackPivotTransform.position.y,0);
            var position = root.position;

            var ray  = _camera.ScreenPointToRay(_inputService.MouseScreenPosition);
            var plane = new Plane(root.up, position + height);
            if (plane.Raycast(ray, out var distance))
            {
                var direction = (ray.GetPoint(distance) - position + height).normalized;
                var targetAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg - 90f;
                var rotationAmount = rotationSpeedValue * dt;
                root.rotation = Quaternion.RotateTowards(root.rotation,
                    Quaternion.Euler(0f, -targetAngle,0f ), rotationAmount);
            }
        }
        public void OnGameLoaded(bool isLoaded)
        {
            _loaded = isLoaded;
        }

        public void OnStartGame()
        {
            _camera = Camera.main;
            _started = true;
        }
        public void OnFinishGame(bool gameWin)
        {
            _started = false;
        }
    }
}