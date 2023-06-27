using Common.Atomic.Actions;
using Common.Atomic.Values;
using UnityEngine;

namespace Models.Components
{
    public sealed class Component_Move
    {
        public readonly AtomicVariable<Vector3> Velocity = new AtomicVariable<Vector3>();

        public Component_Move(AtomicVariable<Vector3> dir, AtomicVariable<float> speed)
        {
            Velocity.Value = dir.Value * speed.Value;
            dir.OnChanged.Subscribe( x=> Velocity.Value = speed.Value * x);
            speed.OnChanged.Subscribe( x=> Velocity.Value = dir.Value * x);
        }
    }
}