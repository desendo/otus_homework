using System;
using Common.Atomic.Values;
using UnityEngine;

namespace Models.Declarative.Weapons
{
    public class ShootDelayModel
    {
        public readonly AtomicVariable<float> ShootDelay = new AtomicVariable<float>();
        public readonly AtomicVariable<float> ShootTimer = new AtomicVariable<float>();
        private WeaponModelCoreAbstract _core;


        public void Construct(WeaponModelCoreAbstract core)
        {
            _core = core;
        }

        public void Update(in float dt)
        {
            if(_core == null)
                return;

            if (!_core.AttackReady.Value)
            {
                ShootTimer.Value += dt;
                if (ShootTimer.Value > ShootDelay.Value)
                {

                    ShootTimer.Value = 0;
                    _core.AttackReady.Value = true;
                }
            }
        }
    }
}