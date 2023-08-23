using System.Globalization;
using Config;
using Models.Declarative.Weapons;

namespace Models.Components.Weapon
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
                if (float.TryParse(p.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                {
                    _core.AttackDelayMechanics.AttackDelayValue.Value = value;
                }
            }
            if (p.Id == "MaxRange_float")
            {
                if (float.TryParse(p.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                {
                    _core.MaxRange.Value = value;
                }
            }
        }
    }
}