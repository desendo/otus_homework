using Common.Atomic.Actions;

namespace Common.Atomic.Values
{
    public interface IAtomicVariable<T> : IAtomicValue<T>
    {
        AtomicEvent<T> OnChanged { get; set; }

        new T Value { get; set; }
    }
}