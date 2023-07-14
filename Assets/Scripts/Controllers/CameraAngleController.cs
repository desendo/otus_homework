using Input;
using UnityEngine;

namespace Controllers
{
    public class CameraAngleController : IStartGameListener, IFinishGameListener, IUpdate
    {
        private bool _isEnabled;
        private Transform _cameraTransform;
        private Camera _camera;
        private float _multiplier = 2f;
        private float _lerpSpeed = 10f;
        private float _target;

        private const float _defaultAngle = 70f;
        private const float _maxAngle = 90f;
        private const float _minAngle = 55f;
        public CameraAngleController(InputService inputService)
        {
            inputService.ScrollDelta.Subscribe(OnScroll);
            _target = _defaultAngle;
        }

        private void OnScroll(float obj)
        {
            if(!_isEnabled)
                return;

            _target +=  obj * _multiplier;
            _target = Mathf.Clamp(_target, _minAngle, _maxAngle);

        }

        public void OnStartGame()
        {
            _camera = Camera.main;
            _cameraTransform = _camera.transform;
            _isEnabled = true;
        }

        public void Update(float dt)
        {
            if(!_isEnabled)
                return;

            var eulerAngles = _cameraTransform.eulerAngles;
            var current = eulerAngles.x;
            current = Mathf.Lerp(current, _target, _lerpSpeed * dt);
            eulerAngles = new Vector3(current, eulerAngles.y, eulerAngles.z);
            _cameraTransform.eulerAngles = eulerAngles;
        }

        public void OnFinishGame(bool gameWin)
        {
            _isEnabled = false;
        }
    }
}