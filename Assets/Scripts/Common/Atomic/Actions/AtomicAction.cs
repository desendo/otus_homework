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
            this.action?.Invoke();
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
}