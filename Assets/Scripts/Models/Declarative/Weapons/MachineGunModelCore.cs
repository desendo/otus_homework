﻿using System;
using Common.Atomic.Actions;
using Common.Atomic.Values;
using GameManager;

namespace Models.Declarative.Weapons
{
    public class MachineGunModelCore : WeaponModelCoreAbstract
    {
        public readonly ReloadModel ReloadModel = new ReloadModel();
        public readonly ClipModel ClipModel = new ClipModel();
        public readonly ShootDelayModel ShootDelayModel = new ShootDelayModel();
        public AtomicAction OnAttackContinue;
        public AtomicAction OnAttackStop;

        public readonly AtomicVariable<float> DisperseAngle = new AtomicVariable<float>();
        public readonly AtomicVariable<float> BulletSpeed = new AtomicVariable<float>();

        private IUpdateProvider _updateProvider;
        private IDisposable _updateSub;
        private bool _continueShoot;


        public void Construct(IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;
            _updateSub = _updateProvider.OnUpdate.Subscribe(Update);
            OnAttack = new AtomicAction(TryShoot);
            OnAttackContinue = new AtomicAction(() =>_continueShoot = true);
            OnAttackStop = new AtomicAction(() =>_continueShoot = false);
            Activate.Subscribe( isActive =>
            {
                IsActive.Value = isActive;
                if(!isActive)
                    ReloadModel.CancelReload();
            });
            ReloadModel.Construct(ClipModel);
            ShootDelayModel.Construct(this);
        }

        private void Update(float dt)
        {
            if(!IsActive.Value)
                return;

            ReloadModel.Update(dt);

            ShootDelayModel.Update(dt);

             if(_continueShoot)
                TryShoot();
        }

        private void TryShoot()
        {
            if (AttackReady.Value && ClipModel.ShotsLeft.Value > 0 && !ReloadModel.IsReloading.Value)
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