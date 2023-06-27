using Common.Atomic.Values;
using UnityEngine;

namespace Models.Components
{
    public sealed class Component_Direction
    {
        public readonly AtomicVariable<Vector3> Direction;

        public Component_Direction(AtomicVariable<Vector3> dir)
        {
            Direction  = dir;

        }
    }
}