using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Atomic.Actions;
using Models.Components;
using Models.Entities;
using Services;

namespace UI.PresentationModel
{
    public sealed class WeaponsListPresentationModel
    {
        private readonly List<IDisposable> _subs = new List<IDisposable>();
        public readonly AtomicEvent OnChange = new AtomicEvent();
        public readonly List<WeaponPresentationModel> WeaponPresentationModels = new List<WeaponPresentationModel>();
        private readonly WeaponManager _weaponManager;

        public WeaponsListPresentationModel(WeaponManager weaponManager)
        {
            _weaponManager = weaponManager;
            weaponManager.OnWeaponCollected.Subscribe(OnWeaponAdd);
            weaponManager.OnWeaponRemoved.Subscribe(OnWeaponRemoved);
            weaponManager.OnWeaponsClear.Subscribe(Clear);
        }

        private void OnWeaponAdd(IWeapon obj)
        {
            var clip = obj.Get<Component_Clip>();
            var reload = obj.Get<Component_Reload>();
            var damage = obj.Get<Component_Damage>();
            var active = obj.Get<Component_IsActive>();
            obj.TryGet<Component_Burst>(out var burst);
            var burstCount = burst?.Count ?? 0;
            var pm = new WeaponPresentationModel(obj.Id, clip.ShotsLeft, clip.ClipSize,
                reload.ReloadTimerNormalized, damage.Damage, active.IsActive, burstCount);

            var info = obj.Get<Component_Info>();
            info.Name.OnChanged.Subscribe(x => pm.Name.Value = x).AddTo(_subs);
            pm.Name.Value = info.Name.Value;
            WeaponPresentationModels.Add(pm);
            OnChange.Invoke();
        }
        private void OnWeaponRemoved(IWeapon obj)
        {
            _subs.Dispose();
            Clear();
            foreach (var w in _weaponManager.CollectedWeapons)
            {
                OnWeaponAdd(w);
            }
            OnChange.Invoke();
        }

        private void Clear()
        {
            WeaponPresentationModels.Clear();
        }
    }
}