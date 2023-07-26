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
        public readonly AttackDelay_Mechanics AttackDelayMechanics = new AttackDelay_Mechanics();

        private IUpdateProvider _updateProvider;
        private IDisposable _updateSub;


        public void Construct(IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;
            _updateSub = _updateProvider.OnUpdate.Subscribe(Update);

            AttackDelayMechanics.Construct(this);
        }

        public override void Activate(bool value)
        {
            IsActive.Value = value;
            AttackReady.Value = true;
        }

        private void Update(float dt)
        {
            if(!IsActive.Value)
                return;

            AttackDelayMechanics.Update(dt);
        }

        public void DoHit(IEntity entity)
        {
            entity.Get<Component_TakeDamage>().DoDamage(Damage.Value);
        }

        public override void Attack()
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
        }
    }
}