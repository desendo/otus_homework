using System;
using UnityEngine;

namespace Animations
{
    public class VerticalRotateAnimation : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform _target;

        private void Update()
        {
            _target.RotateAround(_target.position, Vector3.up, Time.deltaTime * _speed);
        }
    }
}
