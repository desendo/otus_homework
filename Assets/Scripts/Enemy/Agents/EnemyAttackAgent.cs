using Components;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        [SerializeField] private WeaponComponent _weaponComponent;
        [SerializeField] private EnemyMoveAgent _moveAgent;
        private float _countdown;
        public delegate void FireHandler(GameObject enemy, Vector2 position, Vector2 direction);
        public event FireHandler OnFire;
        private float _currentTime;
        private GameObject _target;

        private void FixedUpdate()
        {
            if (!_moveAgent.IsReached || !_target) return;

            if (!_target.GetComponent<HitPointsComponent>().IsHitPointsExists())
                return;

            _currentTime -= Time.fixedDeltaTime;
            if (_currentTime <= 0)
            {
                Fire();
                _currentTime += _countdown;
            }
        }

        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        private void Fire()
        {
            var startPosition = _weaponComponent.Position;
            var vector = (Vector2) _target.transform.position - startPosition;
            var direction = vector.normalized;
            OnFire?.Invoke(gameObject, startPosition, direction);
        }

        public void SetFireInterval(float interval, bool fireImmediately)
        {
            _countdown = interval;
            _currentTime = fireImmediately ? 0: _countdown;
        }
    }
}