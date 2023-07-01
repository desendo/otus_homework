using System;
using Common.Atomic.Actions;
using GameManager;

namespace Models.Declarative.Weapons
{
    public class EnemyWeaponModelCore : WeaponModelCoreAbstract
    {
        public readonly ReloadModel ReloadModel = new ReloadModel();

        private IUpdateProvider _updateProvider;
        private IDisposable _updateSub;


        public void Construct(IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;
            _updateSub = _updateProvider.OnUpdate.Subscribe(Update);
            OnAttack = new AtomicAction(TryAttack);
            Activate.Subscribe(isActive =>
            {
                IsActive.Value = isActive;
                AttackReady.Value = true;
                if(!isActive)
                    ReloadModel.CancelReload();
            });
        }

        private void Update(float dt)
        {
            if(!IsActive.Value)
                return;

            ReloadModel.Update(dt);

        }

        private void TryAttack()
        {
            if (AttackReady.Value && !ReloadModel.IsReloading.Value)
            {
                AttackRequested.Invoke();
            }
        }

        public override void Dispose()
        {
            _updateSub?.Dispose();
        }
    }
}