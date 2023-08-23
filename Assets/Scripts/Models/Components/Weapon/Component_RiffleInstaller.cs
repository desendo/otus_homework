﻿using System.Collections.Generic;
using System.Globalization;
using Config;
using Models.Declarative.Weapons;

namespace Models.Components
{
    public sealed class Component_RiffleInstaller : Component_WeaponInstallerAbstract
    {
        private readonly RiffleModelCore _core;
        public Component_RiffleInstaller(RiffleModelCore core) : base(core)
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

            else if (p.Id == "ReloadTime_float")
            {
                if(float.TryParse(p.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var val))
                    _core.ReloadMechanics.ReloadDelay.Value = val;
            }
            else if (p.Id == "BulletSpeed_float")
            {
                if(float.TryParse(p.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var val))
                    _core.BulletSpeed.Value = val;
            }

        }
    }
}