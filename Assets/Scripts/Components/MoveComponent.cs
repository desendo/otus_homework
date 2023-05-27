using UnityEngine;

namespace Components
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            var nextPosition = _rigidbody2D.position + vector;
            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}