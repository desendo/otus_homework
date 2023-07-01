using Common.Atomic.Actions;
using Common.Atomic.Values;
using UnityEngine;

namespace Models.Components
{
    public sealed class Component_ObservedMove
    {
        private readonly IAction<Vector3> _onDirMove;
        public readonly AtomicEvent<Vector3> MoveRequested = new AtomicEvent<Vector3>();
        public readonly AtomicVariable<Vector3> Velocity;

        public Component_ObservedMove(IAction<Vector3> onDirMove, AtomicEvent<Vector3> moveRequested, AtomicVariable<Vector3> velocity)
        {
            Velocity = velocity;
            _onDirMove = onDirMove;
            moveRequested.Subscribe(moveStep => MoveRequested?.Invoke(moveStep));
        }

        public void Move(Vector3 dirMove)
        {
            _onDirMove.Invoke(dirMove);
        }

        public void SetVelocity(Vector3 translateDelta)
        {
            Velocity.Value = translateDelta;
        }
    }
}