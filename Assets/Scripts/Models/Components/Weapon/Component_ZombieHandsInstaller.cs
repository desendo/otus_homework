using Config;
using Models.Declarative.Weapons;

namespace Models.Components
{
    public class Component_ZombieHandsInstaller : Component_WeaponInstallerAbstract
    {
        private readonly EnemyWeaponModelCore _core;
        public Component_ZombieHandsInstaller(EnemyWeaponModelCore core) : base(core)
        {
            _core = core;
        }

        protected override void HandleParameter(Parameter p)
        {
            if (p.Id == "ReloadTime_float")
            {
                _core.ReloadModel.ReloadDelay.Value = float.Parse(p.Value);
            }
        }
    }
}