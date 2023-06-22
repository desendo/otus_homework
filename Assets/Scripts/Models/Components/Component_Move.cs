using Common.Atomic.Actions;
using Common.Atomic.Values;
using UnityEngine;

namespace Models.Components
{
    public sealed class Component_Move
    {
        private readonly IAction<Vector3> _onDeltaMove;
        public AtomicEvent<Vector3> MoveRequested = new AtomicEvent<Vector3>();
        private readonly AtomicVariable<Vector3> _velocity;

        public Component_Move(IAction<Vector3> onDeltaMove, AtomicEvent<Vector3> moveRequested, AtomicVariable<Vector3> velocity)
        {
            _velocity = velocity;
            _onDeltaMove = onDeltaMove;
            moveRequested += moveStep => MoveRequested?.Invoke(moveStep);
        }

        public void Move(Vector3 deltaMove)
        {
            _onDeltaMove.Invoke(deltaMove);
        }

        public void SetVelocity(Vector3 translateDelta)
        {
            _velocity.Value = translateDelta;
        }
    }
}