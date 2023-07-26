using Common.Atomic.Values;
using UnityEngine;

namespace Models.Components
{
    public sealed class Component_Direction
    {
        public AtomicVariable<Vector3> Direction { get; } = new AtomicVariable<Vector3>();

        public Component_Direction(AtomicVariable<Vector3> dir)
        {
            Direction.Value  = dir.Value;
            dir.OnChanged.Subscribe(x=>Direction.Value = x);

        }
    }
}