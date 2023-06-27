using Common.Atomic.Actions;
using Common.Atomic.Values;
using UnityEngine;

namespace Models.Components
{
    public sealed class Component_ObservedMove
    {
        private readonly IAction<Vector3> _onDeltaMove;
        public AtomicEvent<Vector3> MoveRequested = new AtomicEvent<Vector3>();
        public readonly AtomicVariable<Vector3> Velocity;

        public Component_ObservedMove(IAction<Vector3> onDeltaMove, AtomicEvent<Vector3> moveRequested, AtomicVariable<Vector3> velocity)
        {
            Velocity = velocity;
            _onDeltaMove = onDeltaMove;
            moveRequested.Subscribe(moveStep => MoveRequested?.Invoke(moveStep));
        }

        public void Move(Vector3 deltaMove)
        {
            _onDeltaMove.Invoke(deltaMove);
        }

        public void SetVelocity(Vector3 translateDelta)
        {
            Velocity.Value = translateDelta;
        }
    }
}