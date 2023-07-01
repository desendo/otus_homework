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
                _core.ShootDelay.Value = float.Parse(p.Value);
            }
            else if (p.Id == "ReloadTime_float")
            {
                _core.ReloadModel.ReloadDelay.Value = float.Parse(p.Value);
            }
            else if (p.Id == "BulletSpeed_float")
            {
                _core.BulletSpeed.Value = float.Parse(p.Value);
            }
            else if (p.Id == "BurstAngle_float")
            {
                _core.BurstModel.BurstAngle.Value = float.Parse(p.Value);
            }
            else if (p.Id == "BurstCount_int")
            {
                if (int.TryParse(p.Value, out var val))
                    _core.BurstModel.BurstCount.Value = val;
            }
        }
    }
}