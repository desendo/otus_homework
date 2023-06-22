using Common.Atomic.Actions;
using Common.Atomic.Values;
using UnityEngine;

namespace Models.Declarative
{
    public class MoveModel
    {
        public AtomicEvent<Vector3> OnMoveDir = new AtomicEvent<Vector3>();
        public readonly AtomicEvent<Vector3> MoveRequested = new AtomicEvent<Vector3>();
        public readonly AtomicVariable<bool> IsMoving = new AtomicVariable<bool>();
        public readonly AtomicVariable<Vector3> ResultVelocity = new AtomicVariable<Vector3>();
        public readonly AtomicVariable<float> Speed = new AtomicVariable<float>();
        public readonly AtomicVariable<float> RotationSpeed = new AtomicVariable<float>();

        public void Construct()
        {
            OnMoveDir += dir =>
            {
                var delta = dir * Speed.Value;
                MoveRequested.Invoke(dir * Speed.Value);
                IsMoving.Value = delta.sqrMagnitude > float.Epsilon;
            };
            ResultVelocity.OnChanged += x =>
            {
                IsMoving.Value = x.sqrMagnitude > 0f;
            };
        }
    }
}