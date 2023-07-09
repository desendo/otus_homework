using System;
using Pool;
using UnityEngine;

namespace Effects
{
    public class Effect : MonoBehaviour
    {
        private PoolBase<Effect> _pool;

        private void Awake()
        {
            var main = GetComponentInChildren<ParticleSystem>().main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        public void Setup(PoolBase<Effect> pool)
        {
            _pool = pool;
        }

        private void OnParticleSystemStopped()
        {
            if(_pool != null)
                _pool.Unspawn(this);
        }
    }
}