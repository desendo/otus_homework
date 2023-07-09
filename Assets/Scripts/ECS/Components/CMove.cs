using UnityEngine;

namespace ECS.Components
{
    public struct CMove
    {
        public Vector3 TargetDirection;
        public float TargetMoveSpeed;
        public float TargetRotationSpeed;
        public float CurrentMoveSpeed;
        public float CurrentRotationSpeed;
    }
}