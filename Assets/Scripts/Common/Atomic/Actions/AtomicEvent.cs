using System;
using System.Collections.Generic;
using ReactiveExtension;

namespace Common.Atomic.Actions
{
    public sealed class AtomicEvent : IAtomicAction
    {
        private readonly List<Action> _actions = new List<Action>();

        public IDisposable Subscribe(Action callback)
        {

            _actions.Add(callback);
            return new DisposeContainer(() => UnSubscribe(callback));
        }

        private void UnSubscribe(Action callback)
        {
            _actions.Remove(callback);
        }


        public void Invoke()
        {
            foreach (var action in _actions)
            {
                action.Invoke();
            }
        }
    }

    public class AtomicEvent<T> : IAction<T>
    {
        private readonly List<IAction<T>> actions = new List<IAction<T>>();
        private readonly Dictionary<System.Action<T>, IAction<T>> delegates = new Dictionary<Action<T>, IAction<T>>();
        private readonly List<IAction<T>> cache = new List<IAction<T>>();

        public IDisposable Subscribe(Action<T> callback)
        {
            var action = new AtomicAction<T>(callback);
            actions.Add(action);
            delegates[callback] = action;
            return new DisposeContainer(() => UnSubscribe(callback));
        }

        public void UnSubscribe(Action<T> callback)
        {
            if (delegates.TryGetValue(callback, out var action))
            {
                delegates.Remove(callback);
                actions.Remove(action);
            }
        }

        public void Invoke(T args)
        {
            this.cache.Clear();
            this.cache.AddRange(this.actions);

            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var action = this.cache[i];
                action.Invoke(args);
            }
        }
    }
    public class AtomicEvent<T1,T2> : IAction<T1,T2>
    {
        private readonly List<IAction<T1,T2>> actions = new List<IAction<T1,T2>>();
        private readonly Dictionary<System.Action<T1,T2>, IAction<T1,T2>> delegates = new Dictionary<Action<T1,T2>, IAction<T1,T2>>();
        private readonly List<IAction<T1,T2>> cache = new List<IAction<T1,T2>>();

        public IDisposable Subscribe(Action<T1,T2> callback)
        {
            var action = new AtomicAction<T1,T2>(callback);
            actions.Add(action);
            delegates[callback] = action;
            return new DisposeContainer(() => UnSubscribe(callback));
        }
        public void UnSubscribe(Action<T1,T2> callback)
        {
            if (delegates.TryGetValue(callback, out var action))
            {
                delegates.Remove(callback);
                actions.Remove(action);
            }
        }

        public void Invoke(T1 arg1, T2 arg2)
        {
            this.cache.Clear();
            this.cache.AddRange(actions);

            for (int i = 0, count = cache.Count; i < count; i++)
            {
                var action = cache[i];
                action.Invoke(arg1, arg2);
            }
        }
    }
}