namespace Common.Atomic.Actions
{
    public sealed class AtomicAction : IAtomicAction
    {
        private readonly System.Action action;

        public AtomicAction(System.Action action)
        {
            this.action = action;
        }

        public void Invoke()
        {
            action?.Invoke();
        }
    }

    public sealed class AtomicAction<T> : IAction<T>
    {
        private readonly System.Action<T> action;

        public AtomicAction(System.Action<T> action)
        {
            this.action = action;
        }

        public void Invoke(T args)
        {
            this.action?.Invoke(args);
        }
    }
    public sealed class AtomicAction<T1,T2> : IAction<T1,T2>
    {
        private readonly System.Action<T1,T2> action;

        public AtomicAction(System.Action<T1,T2> action)
        {
            this.action = action;
        }

        public void Invoke(T1 arg1, T2 arg2)
        {
            this.action?.Invoke(arg1, arg2);
        }
    }
}