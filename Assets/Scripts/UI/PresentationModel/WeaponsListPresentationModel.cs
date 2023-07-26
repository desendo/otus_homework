using System.Collections.Generic;
using Common.Atomic.Actions;
using Models.Components;
using Models.Entities;
using Services;

namespace UI.PresentationModel
{
    public sealed class WeaponsListPresentationModel
    {
        public readonly AtomicEvent OnChange = new AtomicEvent();
        public readonly List<WeaponPresentationModel> WeaponPresentationModels = new List<WeaponPresentationModel>();
        public WeaponsListPresentationModel(HeroManager heroManager)
        {
            heroManager.OnWeaponCollected.Subscribe(OnWeaponAdd);
            heroManager.OnWeaponsClear.Subscribe(Clear);
        }

        private void OnWeaponAdd(IWeapon obj)
        {
            var clip = obj.Get<Component_Clip>();
            var reload = obj.Get<Component_Reload>();
            var damage = obj.Get<Component_Damage>();
            var active = obj.Get<Component_IsActive>();
            obj.TryGet<Component_Burst>(out var burst);
            var burstCount = burst?.Count ?? 0;
            var pm = new WeaponPresentationModel(clip.ShotsLeft, clip.ClipSize,
                reload.ReloadTimerNormalized, damage.Damage, active.IsActive, burstCount);

            var info = obj.Get<Component_Info>();
            info.Name.OnChanged.Subscribe(x => pm.Name.Value = x);
            pm.Name.Value = info.Name.Value;
            WeaponPresentationModels.Add(pm);
            OnChange.Invoke();
        }

        private void Clear()
        {
            WeaponPresentationModels.Clear();
        }
    }
}