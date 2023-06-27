using System;
using Common.Atomic.Actions;
using Common.Atomic.Values;
using UnityEngine;

namespace Models.Declarative
{
    public class MoveModel : IDisposable
    {
        public AtomicEvent<Vector3> OnMoveDir = new AtomicEvent<Vector3>();
        public readonly AtomicEvent<Vector3> MoveRequested = new AtomicEvent<Vector3>();
        public readonly AtomicVariable<bool> IsMoving = new AtomicVariable<bool>();
        public readonly AtomicVariable<Vector3> ResultVelocity = new AtomicVariable<Vector3>();
        public readonly AtomicVariable<float> Speed = new AtomicVariable<float>();
        public readonly AtomicVariable<float> RotationSpeed = new AtomicVariable<float>();
        private IDisposable _onMoveSub;
        private IDisposable _velocitySub;

        public void Construct()
        {
            _onMoveSub = OnMoveDir.Subscribe(dir =>
            {
                var delta = dir * Speed.Value;
                MoveRequested.Invoke(dir * Speed.Value);
                IsMoving.Value = delta.sqrMagnitude > float.Epsilon;
            });
            _velocitySub = ResultVelocity.OnChanged.Subscribe(x =>
            {
                IsMoving.Value = x.sqrMagnitude > 0f;
            });
        }

        public void Dispose()
        {
            _velocitySub?.Dispose();
            _onMoveSub?.Dispose();
        }
    }
}