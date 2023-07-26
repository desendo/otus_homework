using Models.Components;
using Services;
using UnityEngine;

namespace Controllers
{
    public class CameraFollowController : IStartGameListener, IFinishGameListener, ILateUpdate
    {
        private readonly HeroManager _heroManager;
        private bool _follow;
        private Transform _targetTransform;
        private Transform _cameraTransform;
        private Camera _camera;

        public CameraFollowController(HeroManager heroManager)
        {
            _heroManager = heroManager;
        }

        public void OnStartGame()
        {
            _targetTransform = _heroManager.HeroEntity.Value.Get<Component_Transform>().RootTransform;
            _camera = Camera.main;
            _cameraTransform = _camera.transform;
            _follow = true;
        }

        public void LateUpdate()
        {
            if(!_follow)
                return;

            var cameraTransformPosition = _cameraTransform.position;
            var targetPos = _targetTransform.position;

            var plane = new Plane(_targetTransform.up, targetPos);
            var ray  = new Ray(cameraTransformPosition, _cameraTransform.forward);

            if (plane.Raycast(ray, out var distance))
            {
                var delta = (ray.GetPoint(distance) - targetPos);
                delta.y = 0f;
                //_cameraTransform.position = Vector3.Lerp(cameraTransformPosition, - delta + cameraTransformPosition, 5f * Time.deltaTime);
                _cameraTransform.position = -delta + cameraTransformPosition;

            }
        }

        public void OnFinishGame(bool gameWin)
        {
            _follow = false;
        }
    }
}