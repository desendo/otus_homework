using Common.Atomic.Actions;
using Common.Entities;
using UnityEngine;

namespace Models.Components
{
    public class Component_Collision
    {
        public readonly AtomicEvent<IEntity, Collision> OnCollision = new AtomicEvent<IEntity, Collision>();
        public Component_Collision(IEntity entity, AtomicEvent<Collision> onCollisionEntered)
        {
            onCollisionEntered.Subscribe(x => OnCollision?.Invoke(entity, x));
        }
    }
}