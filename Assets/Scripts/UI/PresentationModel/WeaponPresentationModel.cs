using Common.Atomic.Values;
using UnityEngine;

namespace UI.PresentationModel
{
    public sealed class WeaponPresentationModel
    {
        public readonly AtomicVariable<string> Name = new AtomicVariable<string>();
        public readonly AtomicVariable<string> Damage = new AtomicVariable<string>();
        public readonly AtomicVariable<float> ReloadProgress = new AtomicVariable<float>();
        public AtomicVariable<bool> IsReloading = new AtomicVariable<bool>();
        public readonly AtomicVariable<bool> IsActive = new AtomicVariable<bool>();
        public readonly AtomicVariable<string> Shots = new AtomicVariable<string>();

        public WeaponPresentationModel(AtomicVariable<int> shotsLeft, AtomicVariable<int> clipSize,
            AtomicVariable<float> reloadTimerNormalized, AtomicVariable<int> damage, AtomicVariable<bool> isActive,
            int shotMult = 0)
        {
            reloadTimerNormalized.OnChanged.Subscribe(x => ReloadProgress.Value = x);
            damage.OnChanged.Subscribe(x =>
            {
                Damage.Value = shotMult == 0 ? $"Damage: {x}" : $"Damage: {x}x{shotMult}";
            });

            Damage.Value = shotMult == 0 ? $"Damage: {damage.Value}" : $"Damage: {damage.Value}x{shotMult}";
            shotsLeft.OnChanged.Subscribe(x => Shots.Value = $"{shotsLeft.Value}/{clipSize.Value}");
            clipSize.OnChanged.Subscribe(x => Shots.Value = $"{shotsLeft.Value}/{clipSize.Value}");
            Shots.Value = $"{shotsLeft.Value}/{clipSize.Value}";

            IsActive.Value = isActive.Value;
            isActive.OnChanged.Subscribe(x => IsActive.Value = x);
        }
    }
}