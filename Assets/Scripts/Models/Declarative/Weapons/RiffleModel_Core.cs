using System;
using Common.Atomic.Actions;
using Common.Atomic.Values;
using GameManager;

namespace Models.Declarative.Weapons
{
    public class RiffleModelCore : WeaponModelCoreAbstract
    {
        public readonly ReloadModel ReloadModel = new ReloadModel();
        public readonly ClipModel ClipModel = new ClipModel();
        public readonly ShootDelayModel ShootDelayModel = new ShootDelayModel();


        public readonly AtomicVariable<float> BulletSpeed = new AtomicVariable<float>();

        private IUpdateProvider _updateProvider;
        private IDisposable _updateSub;


        public void Construct(IUpdateProvider updateProvider)
        {
            OnAttack = new AtomicAction(TryShoot);
            Activate.Subscribe(isActive =>
            {
                IsActive.Value = isActive;
                if(!isActive)
                    ReloadModel.CancelReload();
            });
            _updateProvider = updateProvider;
            _updateSub = _updateProvider.OnUpdate.Subscribe(Update);
            ReloadModel.Construct(ClipModel);
            ShootDelayModel.Construct(this);

        }

        private void Update(float dt)
        {
            if(!IsActive.Value)
                return;

            ReloadModel.Update(dt);
            ShootDelayModel.Update(dt);

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