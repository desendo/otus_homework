using Common.Atomic.Actions;

namespace Models.Components
{
    public sealed class Component_IsReloading
    {
        private readonly AtomicEvent<float> _onReloadingStarted;

        public Component_IsReloading(AtomicEvent<float> onReloadingStart)
        {
            _onReloadingStarted = onReloadingStart;
        }

        public void SetReloadingTime(float time)
        {
            _onReloadingStarted.Invoke(time);
        }
    }
}