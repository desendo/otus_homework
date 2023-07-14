using System.Globalization;
using Config;
using Models.Declarative.Weapons;

namespace Models.Components
{
    public sealed class Component_MachineGunInstaller : Component_WeaponInstallerAbstract
    {
        private readonly MachineGunModel _core;

        public Component_MachineGunInstaller(MachineGunModel core) : base(core)
        {
            _core = core;
        }

        protected override void HandleParameter(Parameter p)
        {
            if (p.Id == "ClipCapacity_int")
            {
                if (int.TryParse(p.Value, out var val))
                    _core.ClipModel.ClipSize.Value = val;
            }
            else if (p.Id == "Delay_float")
            {
                if (float.TryParse(p.Value, out var val))
                    _core.AttackDelayMechanics.AttackDelayValue.Value = val;
            }
            else if (p.Id == "ReloadTime_float")
            {
                _core.ReloadMechanics.ReloadDelay.Value = float.Parse(p.Value);
            }
            else if (p.Id == "DisperseAngle_float")
            {
                if (float.TryParse(p.Value, out var val))
                    _core.DisperseAngle.Value = val;
            }
            else if (p.Id == "BulletSpeed_float")
            {
                if (float.TryParse(p.Value, out var val))
                    _core.BulletSpeed.Value = val;
            }
        }
    }
}