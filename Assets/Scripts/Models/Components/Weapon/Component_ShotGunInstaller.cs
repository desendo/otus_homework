using System.Globalization;
using Config;
using Models.Declarative.Weapons;

namespace Models.Components
{
    public sealed class Component_ShotGunInstaller : Component_WeaponInstallerAbstract
    {
        private readonly ShotgunModelCore _core;

        public Component_ShotGunInstaller(ShotgunModelCore core) : base(core)
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
                if (float.TryParse(p.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                {
                    _core.AttackDelayMechanics.AttackDelayValue.Value = value;
                }
            }
            else if (p.Id == "ReloadTime_float")
            {
                if (float.TryParse(p.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                    _core.ReloadMechanics.ReloadDelay.Value = value;
            }
            else if (p.Id == "BulletSpeed_float")
            {
                if (float.TryParse(p.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                    _core.BulletSpeed.Value = value;
            }
            else if (p.Id == "BurstAngle_float")
            {
                if (float.TryParse(p.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                    _core.Burst.BurstAngle.Value = value;
            }
            else if (p.Id == "BurstCount_int")
            {
                if (int.TryParse(p.Value, out var val))
                    _core.Burst.BurstCount.Value = val;
            }
        }
    }
}