using UnityEngine;

namespace Models.Components
{
    public sealed class Component_Pivot
    {
        private readonly Transform _pivot;
        public Vector3 Direction => _pivot.forward;
        public Vector3 Position => _pivot.position;

        public Component_Pivot(Transform pivot)
        {
            _pivot = pivot;
        }
    }
}