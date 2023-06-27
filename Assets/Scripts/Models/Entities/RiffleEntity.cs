using Common.Entities;
using Config;
using GameManager;
using Models.Components;
using Models.Declarative.Weapons;
using UnityEngine;

namespace Models.Entities
{
    public class RiffleEntity : Entity, IWeapon
    {
        private readonly RiffleModelCore _core = new RiffleModelCore();
        private readonly Transform _shootTransform;
        public WeaponType WeaponType { get; }

        public RiffleEntity(IUpdateProvider updateProvider, Transform shootTransform, WeaponType weaponType)
        {
            WeaponType = weaponType;
            _shootTransform = shootTransform;
            _core.Construct(updateProvider);
            AddComponents();
        }

        private void AddComponents()
        {
            Add(new Component_Info(_core.Name));
            Add(new Component_Attack(_core.OnAttack));
            Add(new Component_IsActive(_core.IsActive));
            Add(new Component_Clip(_core.ClipModel.ClipSize, _core.ClipModel.ShotsLeft));
            Add(new Component_Pivot(_shootTransform));
            Add(new Component_Shoot(_core.AttackRequested));
            Add(new Component_Speed(_core.BulletSpeed));
            Add(new Component_Damage(_core.Damage));
            Add(new Component_Reload(_core.ReloadModel.OnReload, _core.ReloadModel.ReloadStarted,
                _core.ReloadModel.ReloadTimer, _core.ReloadModel.ReloadDelay));
            Add(new Component_SetActive(_core.Activate));
            Add(new Component_RiffleInstaller(_core));
        }

        ~RiffleEntity()
        {
            _core.Dispose();
        }
    }
}