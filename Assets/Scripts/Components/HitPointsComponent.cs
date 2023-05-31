using System;
using ReactiveExtension;
using UnityEngine;

namespace Components
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        private int _hitPoints;
        public event Action<GameObject> HpEmpty;
        public readonly Reactive<int> HpLeft = new Reactive<int>();

        public bool IsHitPointsExists()
        {
            return _hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            HpLeft.Value = _hitPoints;
            if (_hitPoints <= 0)
                HpEmpty?.Invoke(gameObject);
        }

        public void SetHitPoints(int current)
        {
            _hitPoints = current;
        }
    }
}