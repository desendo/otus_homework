using Common.Atomic.Actions;

namespace GameManager
{
    public interface IUpdateProvider
    {
        AtomicEvent<float> OnUpdate { get; set; }
        AtomicEvent<float> OnFixedUpdate { get; set; }
        AtomicEvent OnLateUpdate { get; set; }
    }

    public class UpdateProvider: IFixedUpdate, IUpdate, ILateUpdate, IUpdateProvider
    {
        public AtomicEvent<float> OnUpdate { get; set; } = new AtomicEvent<float>();
        public AtomicEvent<float> OnFixedUpdate { get; set; } = new AtomicEvent<float>();
        public AtomicEvent OnLateUpdate { get; set; } = new AtomicEvent();
        public void FixedUpdate(float dt)
        {
            OnFixedUpdate.Invoke(dt);
        }

        public void Update(float dt)
        {
            OnUpdate.Invoke(dt);
        }

        public void LateUpdate()
        {
            OnLateUpdate.Invoke();
        }
    }
}