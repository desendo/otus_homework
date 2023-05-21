using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public delegate void FireHandler(GameObject enemy, Vector2 position, Vector2 direction);
        public event FireHandler OnFire;

        [SerializeField] private WeaponComponent weaponComponent;
        [SerializeField] private EnemyMoveAgent moveAgent;
        [SerializeField] private float countdown;
        private float currentTime;

        private GameObject _target;

        public void Reset()
        {
            currentTime = countdown;
        }

        private void FixedUpdate()
        {
            if (!moveAgent.IsReached) return;

            if (!_target.GetComponent<HitPointsComponent>().IsHitPointsExists())
                return;

            currentTime -= Time.fixedDeltaTime;
            if (currentTime <= 0)
            {
                Fire();
                currentTime += countdown;
            }
        }


        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        private void Fire()
        {
            var startPosition = weaponComponent.Position;
            var vector = (Vector2) _target.transform.position - startPosition;
            var direction = vector.normalized;
            OnFire?.Invoke(gameObject, startPosition, direction);
        }
    }
}