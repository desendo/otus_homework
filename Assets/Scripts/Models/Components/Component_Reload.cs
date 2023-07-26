using System;
using Common.Atomic.Actions;
using Common.Atomic.Values;

namespace Models.Components

{
    public sealed class Component_Reload
    {
        private readonly Action _reload;
        public AtomicEvent<float> ReloadStart { get; }
        public AtomicVariable<float> ReloadTimerNormalized { get; } = new AtomicVariable<float>();


        public Component_Reload(Action reload,
            AtomicEvent<float> reloadStart, AtomicVariable<float> reloadTimer, AtomicVariable<float> reloadTime)
        {
            _reload = reload;
            ReloadStart = reloadStart;
            reloadTimer.OnChanged.Subscribe(x => ReloadTimerNormalized.Value = x / reloadTime.Value);
            reloadTime.OnChanged.Subscribe(x => ReloadTimerNormalized.Value = reloadTimer.Value / x );
            ReloadTimerNormalized.Value = reloadTimer.Value/reloadTime.Value;
        }

        public void Reload()
        {
            _reload.Invoke();
        }
    }
}