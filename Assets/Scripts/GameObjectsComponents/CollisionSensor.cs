using Common.Atomic.Actions;
using UnityEngine;

namespace GameObjectsComponents
{
    public class CollisionSensor : MonoBehaviour
    {
        public readonly AtomicEvent<Collision> Collision = new AtomicEvent<Collision>();
        private void OnCollisionEnter(Collision other)
        {
            Collision?.Invoke(other);
        }
    }
}