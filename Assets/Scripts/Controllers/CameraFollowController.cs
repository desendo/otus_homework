﻿using Models.Components;
using Services;
using UnityEngine;

namespace Controllers
{
    public class CameraFollowController : IStartGameListener, ILostGameListener, IWinGameListener, ILateUpdate
    {
        private readonly HeroService _heroService;
        private bool _follow;
        private Transform _targetTransform;
        private Transform _cameraTransform;
        private Camera _camera;

        public CameraFollowController(HeroService heroService)
        {
            _heroService = heroService;
        }

        public void OnStartGame()
        {
            _targetTransform = _heroService.HeroEntity.Value.Get<Component_Transform>().RootTransform;
            _camera = Camera.main;
            _cameraTransform = _camera.transform;
            _follow = true;
        }
        public void OnLostGame()
        {
            _follow = false;
        }

        public void OnWinGame()
        {
            _follow = false;
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
    }
}