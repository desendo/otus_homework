using Common.Atomic.Values;
using UnityEngine;

namespace Models.Components
{
    public sealed class Component_Direction
    {
        private AtomicVariable<Vector3> _dir;
        public AtomicVariable<Vector3> Direction { get; } = new AtomicVariable<Vector3>();

        public Component_Direction(AtomicVariable<Vector3> dir)
        {
            _dir = dir;
            Direction.Value  = dir.Value;
            dir.OnChanged.Subscribe(x=>Direction.Value = x);
        }

        public void SetDirection(Vector3 dir)
        {
            _dir.Value = dir;
        }
    }
}