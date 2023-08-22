using Common.Atomic.Values;
using ItemInventory.Config;
using Models.Components;
using Models.Entities;
using Services.Effects;

namespace Services
{
    public class WeaponEffectManager : IEffectContainer
    {
        private readonly Effector _effector  = new Effector();
        public void AddEffect(Effect effect) => _effector.AddEffect(effect);
        public void RemoveEffect(Effect effect) => _effector.RemoveEffect(effect);

        private readonly AtomicVariable<float> _damageMultiplier = new AtomicVariable<float>(1f);
        private readonly WeaponManager _weaponManager;

        public WeaponEffectManager(WeaponManager weaponManager)
        {
            _effector.AddHandler(new WeaponDamageEffectHandler(_damageMultiplier));

            _weaponManager = weaponManager;
            weaponManager.OnWeaponCollected.Subscribe(OnWeaponAdd);
            weaponManager.OnWeaponCollected.Subscribe(OnWeaponRemoved);
            _damageMultiplier.OnChanged.Subscribe(ApplyMultiplier);
            ApplyMultiplier(_damageMultiplier.Value);
        }

        private void ApplyMultiplier(float obj)
        {
            foreach (var weapon in _weaponManager.CollectedWeapons)
            {
                weapon.Get<Component_DamageMultiplier>().SetMultiplier(_damageMultiplier.Value);
            }
        }

        private void OnWeaponRemoved(IWeapon weapon)
        {
            weapon.Get<Component_DamageMultiplier>().SetMultiplier(1f);
        }

        private void OnWeaponAdd(IWeapon weapon)
        {
            weapon.Get<Component_DamageMultiplier>().SetMultiplier(_damageMultiplier.Value);
        }
    }
}