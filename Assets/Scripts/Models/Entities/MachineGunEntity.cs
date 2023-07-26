using Common.Entities;
using Config;
using GameManager;
using Models.Components;
using Models.Declarative.Weapons;
using UnityEngine;

namespace Models.Entities
{
    public class MachineGunEntity : WeaponEntityBase
    {
        private MachineGunModel _core;

        public MachineGunEntity(IUpdateProvider updateProvider, Transform shootTransform, WeaponType weaponType) : base(updateProvider, shootTransform, weaponType)
        {

        }

        public void BindModel(MachineGunModel model)
        {
            _core = model;
            _core.Construct(UpdateProvider);
            AddComponents();
        }

        private void AddComponents()
        {
            Add(new Component_Info(_core.Name));
            Add(new Component_Attack(_core.Attack,_core.OnAttackStop, _core.OnAttackContinue));
            Add(new Component_Dispersion(_core.DisperseAngle));
            Add(new Component_IsActive(_core.IsActive));
            Add(new Component_Clip(_core.ClipModel.ClipSize, _core.ClipModel.ShotsLeft));
            Add(new Component_Pivot(ShootTransform));
            Add(new Component_OnAttack(_core.AttackRequested));
            Add(new Component_Speed(_core.BulletSpeed));
            Add(new Component_Damage(_core.Damage));
            Add(new Component_Reload(_core.ReloadMechanics.Reload, _core.ReloadMechanics.ReloadStarted,
                _core.ReloadMechanics.ReloadTimer, _core.ReloadMechanics.ReloadDelay));
            Add(new Component_SetActive(_core.Activate));
            Add(new Component_MachineGunInstaller(_core));
        }

        public override void Dispose()
        {
            _core.Dispose();
        }
    }
}