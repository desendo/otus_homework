using System;
using Common.Atomic.Actions;
using Common.Atomic.Values;
using GameManager;

namespace Models.Declarative.Weapons
{
    public class ShotgunModelCore : WeaponModelCoreAbstract
    {
        public readonly ReloadModel ReloadModel = new ReloadModel();
        public readonly ClipModel ClipModel = new ClipModel();
        public readonly BurstModel BurstModel = new BurstModel();


        public readonly AtomicVariable<float> ShootDelay = new AtomicVariable<float>();
        public readonly AtomicVariable<float> ShootTimer = new AtomicVariable<float>();
        public readonly AtomicVariable<float> BulletSpeed = new AtomicVariable<float>();


        private IUpdateProvider _updateProvider;
        private IDisposable _updateSub;


        public void Construct(IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;
            _updateSub = _updateProvider.OnUpdate.Subscribe(Update);
            OnAttack = new AtomicAction(TryShoot);
            Activate.Subscribe(isActive =>
            {
                IsActive.Value = isActive;
                if(!isActive)
                    ReloadModel.CancelReload();
            });
            ReloadModel.Construct(ClipModel);
        }

        private void Update(float dt)
        {
            if(!IsActive.Value)
                return;

            ReloadModel.Update(dt);
            if (!AttackReady.Value)
            {
                ShootTimer.Value += dt;
                if (ShootTimer.Value > ShootDelay.Value)
                    AttackReady.Value = true;
            }
        }

        private void TryShoot()
        {
            if (AttackReady.Value && ClipModel.ShotsLeft.Value > 0 && !ReloadModel.IsReloading.Value)
            {
                ShootTimer.Value = ShootDelay.Value;
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