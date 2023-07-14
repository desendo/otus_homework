using Common.Entities;
using Config;
using GameManager;
using Models.Components;
using Models.Declarative.Weapons;
using UnityEngine;

namespace Models.Entities
{
    public class ShotGunEntity : Entity, IWeapon
    {
        private readonly ShotgunModelCore _core = new ShotgunModelCore();
        private readonly Transform _shootTransform;

        public WeaponType WeaponType { get; }

        public ShotGunEntity(IUpdateProvider updateProvider, Transform shootTransform, WeaponType weaponConfigType)
        {
            _shootTransform = shootTransform;
            _core.Construct(updateProvider);
            WeaponType = weaponConfigType;
            AddComponents();
        }

        private void AddComponents()
        {
            Add(new Component_Info(_core.Name));
            Add(new Component_IsActive(_core.IsActive));
            Add(new Component_Attack(_core.OnAttack));
            Add(new Component_Clip(_core.ClipModel.ClipSize, _core.ClipModel.ShotsLeft));
            Add(new Component_Pivot(_shootTransform));
            Add(new Component_OnAttack(_core.AttackRequested));
            Add(new Component_Speed(_core.BulletSpeed));
            Add(new Component_Burst(_core.Burst.BurstAngle, _core.Burst.BurstCount));
            Add(new Component_Damage(_core.Damage));
            Add(new Component_Reload(_core.ReloadMechanics.OnReload, _core.ReloadMechanics.ReloadStarted,
                _core.ReloadMechanics.ReloadTimer, _core.ReloadMechanics.ReloadDelay));
            Add(new Component_SetActive(_core.Activate));
            Add(new Component_ShotGunInstaller(_core));
        }

        public override void Dispose()
        {
            _core.Dispose();
        }
    }
}