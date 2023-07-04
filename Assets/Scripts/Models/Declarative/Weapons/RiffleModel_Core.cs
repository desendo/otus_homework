using System;
using Common.Atomic.Values;
using GameManager;

namespace Models.Declarative.Weapons
{
    public class RiffleModelCore : WeaponModelCoreAbstract
    {
        public readonly ReloadModel ReloadModel = new ReloadModel();
        public readonly ClipModel ClipModel = new ClipModel();
        public readonly AttackDelayModel AttackDelayModel = new AttackDelayModel();


        public readonly AtomicVariable<float> BulletSpeed = new AtomicVariable<float>();

        private IUpdateProvider _updateProvider;
        private IDisposable _updateSub;


        public void Construct(IUpdateProvider updateProvider)
        {
            Activate.Subscribe(isActive =>
            {
                IsActive.Value = isActive;
                if(!isActive)
                    ReloadModel.CancelReload();
            });
            _updateProvider = updateProvider;
            _updateSub = _updateProvider.OnUpdate.Subscribe(Update);
            ReloadModel.Construct(ClipModel);
            AttackDelayModel.Construct(this);

        }

        private void Update(float dt)
        {
            if(!IsActive.Value)
                return;

            ReloadModel.Update(dt);
            AttackDelayModel.Update(dt);

        }

        protected override void TryAttack()
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