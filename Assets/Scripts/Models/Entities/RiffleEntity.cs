using Config;
using GameManager;
using Models.Components;
using Models.Declarative.Weapons;
using UnityEngine;

namespace Models.Entities
{
    public class RiffleEntity : WeaponEntityBase
    {
        private RiffleModelCore _core;

        public RiffleEntity(IUpdateProvider updateProvider, Transform shootTransform, WeaponType weaponType, string id)
            : base(updateProvider, shootTransform, weaponType, id)
        {
        }

        public void BindModel(RiffleModelCore modelCore)
        {
            _core = modelCore;
            _core.Construct();
            _core.Construct_Mechanics(UpdateProvider);
            AddComponents();
        }

        private void AddComponents()
        {
            Add(new Component_Info(_core.Name));
            Add(new Component_Attack(_core.Attack));
            Add(new Component_IsActive(_core.IsActive));
            Add(new Component_Clip(_core.ClipModel.ClipSize, _core.ClipModel.ShotsLeft));
            Add(new Component_Pivot(ShootTransform));
            Add(new Component_OnAttack(_core.AttackRequested));
            Add(new Component_Speed(_core.BulletSpeed));
            Add(new Component_Damage(_core.Damage));
            Add(new Component_Reload(_core.ReloadMechanics.Reload, _core.ReloadMechanics.ReloadStarted,
                _core.ReloadMechanics.ReloadTimer, _core.ReloadMechanics.ReloadDelay));
            Add(new Component_SetActive(_core.Activate));
            Add(new Component_RiffleInstaller(_core));
        }

        public override void Dispose()
        {
            _core.Dispose();
        }
    }
}