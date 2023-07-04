using System;
using Common.Atomic.Values;
using UnityEngine;

namespace Models.Declarative.Weapons
{
    public class AttackDelayModel
    {
        public readonly AtomicVariable<float> AttackDelayValue = new AtomicVariable<float>();
        public readonly AtomicVariable<float> AttackDelayTimer = new AtomicVariable<float>();
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
                AttackDelayTimer.Value += dt;
                if (AttackDelayTimer.Value > AttackDelayValue.Value)
                {

                    AttackDelayTimer.Value = 0;
                    _core.AttackReady.Value = true;
                }
            }
        }
    }
}