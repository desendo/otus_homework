using System;

namespace Common.Atomic.Values
{
    public sealed class AtomicValue<T> : IAtomicValue<T>
    {
        public T Value => _func.Invoke();

        private readonly Func<T> _func;

        public AtomicValue(Func<T> func)
        {
            _func = func;
        }
    }
}