using Common.Atomic.Actions;

namespace Common.Atomic.Values
{
    public class AtomicVariable<T> : IAtomicVariable<T>
    {
        public AtomicEvent<T> OnChanged { get; set; } = new AtomicEvent<T>();

        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                OnChanged?.Invoke(value);
            }
        }


        private T value;

        public AtomicVariable()
        {
            this.value = default;
        }

        public AtomicVariable(T value)
        {
            this.value = value;
        }

#if UNITY_EDITOR
        private void OnValueChanged(T val)
        {
            this.OnChanged?.Invoke(val);
        }
#endif
    }
}