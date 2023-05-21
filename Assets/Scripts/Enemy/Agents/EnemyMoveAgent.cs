using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        [SerializeField] private MoveComponent moveComponent;

        private Vector2 destination;

        public bool IsReached { get; private set; }

        private void FixedUpdate()
        {
            if (IsReached) return;

            var vector = destination - (Vector2) transform.position;
            if (vector.magnitude <= 0.25f)
            {
                IsReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            moveComponent.MoveByRigidbodyVelocity(direction);
        }

        public void SetDestination(Vector2 endPoint)
        {
            destination = endPoint;
            IsReached = false;
        }
    }
}