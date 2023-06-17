using Common.Atomic.Actions;

namespace Common.Atomic.Values
{
    public class AtomicVariable<T> : IAtomicVariable<T>
    {
        public AtomicEvent<T> OnChanged { get; set; }

        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                this.OnChanged?.Invoke(value);
            }
        }

        private T value;

#if UNITY_EDITOR
        private void OnValueChanged(T val)
        {
            OnChanged?.Invoke(val);
        }
#endif
    }
}