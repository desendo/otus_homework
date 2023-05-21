using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        [SerializeField] private int hitPoints;
        private int _initialPoints;
        public event Action<GameObject> hpEmpty;
        public event Action<int> HpLeft;

        public int HitPoints => hitPoints;

        public bool IsHitPointsExists()
        {
            return HitPoints > 0;
        }

        private void Awake()
        {
            _initialPoints = HitPoints;
        }

        public void TakeDamage(int damage)
        {
            hitPoints = HitPoints - damage;
            HpLeft?.Invoke(HitPoints);
            if (HitPoints <= 0) hpEmpty?.Invoke(gameObject);
        }
        public void DoReset()
        {
            hitPoints = _initialPoints;
        }
    }
}