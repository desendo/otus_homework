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
            if (p.Id == "Delay_float")
            {
                _core.AttackDelayMechanics.AttackDelayValue.Value = float.Parse(p.Value);
            }
            if (p.Id == "MaxRange_float")
            {
                _core.MaxRange.Value = float.Parse(p.Value);
            }
        }
    }
}