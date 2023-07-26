using Common.Atomic.Values;
using UnityEngine;

namespace Models.Components
{
    public sealed class Component_Move
    {
        public Vector3 Velocity { get; private set; }

        public Component_Move(AtomicVariable<Vector3> dir, AtomicVariable<float> speed)
        {
            Velocity = dir.Value * speed.Value;
            dir.OnChanged.Subscribe( x=> Velocity = speed.Value * x);
            speed.OnChanged.Subscribe( x=> Velocity = dir.Value * x);
        }
    }

}