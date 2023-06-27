using Common.Atomic.Actions;
namespace Models.Components
{
    public sealed class Component_IsReloading
    {
        private readonly AtomicEvent<float> _isReloading;


        public Component_IsReloading(AtomicEvent<float> isReloading)
        {
            _isReloading = isReloading;
        }

        public void SetReloadingTime(float time)
        {
            _isReloading.Invoke(time);
        }
    }
}