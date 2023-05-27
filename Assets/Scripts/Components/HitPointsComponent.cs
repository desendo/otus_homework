using System;
using UnityEngine;

namespace Components
{
    public sealed class HitPointsComponent : MonoBehaviour, IReset
    {
        private int _hitPoints;
        private int _initialPoints;
        private bool _isSet;
        public event Action<GameObject> HpEmpty;
        public event Action<int> HpLeft;
        public int HitPoints => _hitPoints;

        public bool IsHitPointsExists()
        {
            return HitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            _hitPoints = HitPoints - damage;
            HpLeft?.Invoke(HitPoints);
            if (HitPoints <= 0) HpEmpty?.Invoke(gameObject);
        }

        public void DoReset()
        {
            if(_isSet)
                _hitPoints = _initialPoints;
        }

        public void SetHitPoints(int max, int current)
        {
            _isSet = true;
            _initialPoints = max;
            _hitPoints = current;
        }
    }
}