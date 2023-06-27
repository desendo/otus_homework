using UnityEngine;

namespace Models.Components
{
    public sealed class Component_AttackPivot
    {
        public Transform AttackPoint { get; }
        public Component_AttackPivot(Transform transform)
        {
            AttackPoint = transform;
        }
    }
}