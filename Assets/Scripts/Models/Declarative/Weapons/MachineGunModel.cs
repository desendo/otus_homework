using System;
using Common.Atomic.Actions;
using Common.Atomic.Values;
using GameManager;

namespace Models.Declarative.Weapons
{
    public class MachineGunModel : WeaponModelCoreAbstract
    {
        public readonly Reload_Mechanics ReloadMechanics = new Reload_Mechanics();
        public readonly ClipModel ClipModel = new ClipModel();
        public readonly AttackDelay_Mechanics AttackDelayMechanics = new AttackDelay_Mechanics();
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
            OnAttackContinue = new AtomicAction(() =>_continueShoot = true);
            OnAttackStop = new AtomicAction(() =>_continueShoot = false);
            Activate.Subscribe( isActive =>
            {
                IsActive.Value = isActive;
                if(!isActive)
                    ReloadMechanics.CancelReload();
            });
            ReloadMechanics.Construct(ClipModel);
            AttackDelayMechanics.Construct(this);
        }

        private void Update(float dt)
        {
            if(!IsActive.Value)
                return;

            ReloadMechanics.Update(dt);

            AttackDelayMechanics.Update(dt);

             if(_continueShoot)
                 TryAttack();
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