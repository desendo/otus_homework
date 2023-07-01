using UnityEngine;

namespace Models.Components
{
    public sealed class Component_Rigidbody
    {
        public Rigidbody Rigidbody { get; }
        public Component_Rigidbody(Rigidbody rb)
        {
            Rigidbody = rb;
        }

        public void SetVelocity(Vector3 velocity)
        {
            Rigidbody.velocity = velocity;
        }
    }
}