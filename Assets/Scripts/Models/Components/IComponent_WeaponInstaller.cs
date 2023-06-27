using System.Collections.Generic;
using Config;
using Models.Declarative.Weapons;

namespace Models.Components
{
    public interface IComponent_WeaponInstaller
    {
        void Setup(IEnumerable<Parameter> parameters);
    }
    public abstract class Component_WeaponInstallerAbstract : IComponent_WeaponInstaller
    {
        private readonly WeaponModelCoreAbstract _core;

        protected Component_WeaponInstallerAbstract(WeaponModelCoreAbstract core)
        {
            _core = core;
        }

        public void Setup(IEnumerable<Parameter> parameters)
        {
            foreach (var p in parameters)
            {
                if(p.Id.Contains("float"))
                    p.Value = p.Value.Replace('.', ',');

                if (p.Id == "Damage_int")
                {
                    if (int.TryParse(p.Value, out var val))
                        _core.Damage.Value = val;
                }
                else if (p.Id == "Name_string")
                {
                    _core.Name.Value = p.Value;
                }
                else
                {
                    HandleParameter(p);
                }
            }
        }

        protected virtual void HandleParameter(Parameter p)
        {
        }
    }

}