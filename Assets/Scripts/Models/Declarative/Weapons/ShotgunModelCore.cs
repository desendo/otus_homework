using System;
using Common.Atomic.Actions;
using Common.Atomic.Values;
using GameManager;

namespace Models.Declarative.Weapons
{
    public class ShotgunModelCore : WeaponModelCoreAbstract
    {
        public readonly Reload_Mechanics ReloadMechanics = new Reload_Mechanics();
        public readonly ClipModel ClipModel = new ClipModel();
        public readonly Burst Burst = new Burst();
        public readonly AttackDelay_Mechanics AttackDelayMechanics = new AttackDelay_Mechanics();


        public readonly AtomicVariable<float> ShootTimer = new AtomicVariable<float>();
        public readonly AtomicVariable<float> BulletSpeed = new AtomicVariable<float>();

        private IUpdateProvider _updateProvider;
        private IDisposable _updateSub;


        public void Construct(IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;
            _updateSub = _updateProvider.OnUpdate.Subscribe(Update);
            Activate.Subscribe(isActive =>
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