using System;
using Common.Atomic.Values;
using Common.Entities;
using GameManager;
using Models.Components;

namespace Models.Declarative.Weapons
{
    public class EnemyWeaponModelCore : WeaponModelCoreAbstract
    {
        public readonly AtomicVariable<float> MaxRange = new AtomicVariable<float>();
        public readonly AttackDelayModel AttackDelayModel = new AttackDelayModel();

        private IUpdateProvider _updateProvider;
        private IDisposable _updateSub;
        private IDisposable _activateSub;


        public void Construct(IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;
            _updateSub = _updateProvider.OnUpdate.Subscribe(Update);
            _activateSub = Activate.Subscribe(isActive =>
            {
                IsActive.Value = isActive;
                AttackReady.Value = true;

            });
            AttackDelayModel.Construct(this);
        }

        private void Update(float dt)
        {
            if(!IsActive.Value)
                return;

            AttackDelayModel.Update(dt);
        }

        public void DoHit(IEntity entity)
        {
            entity.Get<Component_TakeDamage>().DoDamage(Damage.Value);
        }

        protected override void TryAttack()
        {
            if (AttackReady.Value)
            {
                AttackRequested.Invoke();
                AttackReady.Value = false;
            }
        }

        public override void Dispose()
        {
            _updateSub?.Dispose();
            _activateSub?.Dispose();
        }
    }
}