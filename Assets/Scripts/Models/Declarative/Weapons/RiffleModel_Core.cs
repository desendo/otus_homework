﻿using System;
using Common.Atomic.Values;
using GameManager;

namespace Models.Declarative.Weapons
{
    public class RiffleModelCore : WeaponModelCoreAbstract
    {
        public readonly Reload_Mechanics ReloadMechanics = new Reload_Mechanics();
        public readonly ClipModel ClipModel = new ClipModel();
        public readonly AttackDelay_Mechanics AttackDelayMechanics = new AttackDelay_Mechanics();


        public readonly AtomicVariable<float> BulletSpeed = new AtomicVariable<float>();

        private IUpdateProvider _updateProvider;
        private IDisposable _updateSub;


        public void Construct()
        {
            Activate.Subscribe(isActive =>
            {
                IsActive.Value = isActive;
                if(!isActive)
                    ReloadMechanics.CancelReload();
            });
            ReloadMechanics.Construct(ClipModel);
            AttackDelayMechanics.Construct(this);
        }
        public void Construct_Mechanics(IUpdateProvider updateProvider)
        {
            _updateSub = updateProvider.OnUpdate.Subscribe(dt =>
            {
                if(!IsActive.Value)
                    return;

                ReloadMechanics.Update(dt);
                AttackDelayMechanics.Update(dt);

            });
        }


        protected override void TryAttack()
        {
            if (AttackReady.Value && ClipModel.ShotsLeft.Value > 0 && !ReloadMechanics.IsReloading.Value)
            {
                AttackRequested.Invoke();
                AttackReady.Value = false;
                ClipModel.ShotsLeft.Value--;
            }
        }

        public override void Dispose()
        {
            _updateSub?.Dispose();
        }
    }
}