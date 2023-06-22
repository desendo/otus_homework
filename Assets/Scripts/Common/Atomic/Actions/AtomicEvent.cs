using System;
using System.Collections.Generic;

namespace Common.Atomic.Actions
{
    public sealed class AtomicEvent : IAtomicAction
    {
        private readonly List<IAtomicAction> actions;

        public AtomicEvent()
        {
            this.actions = new List<IAtomicAction>(1);
        }

        public static AtomicEvent operator +(AtomicEvent composite, IAtomicAction action)
        {
            if (composite == null)
            {
                composite = new AtomicEvent();
            }

            composite.actions.Add(action);
            return composite;
        }

        public static AtomicEvent operator -(AtomicEvent composite, IAtomicAction action)
        {
            if (composite == null)
            {
                return null;
            }

            composite.actions.Remove(action);
            return composite;
        }

        public static AtomicEvent operator +(AtomicEvent composite, System.Action action)
        {
            composite += new AtomicAction(action);
            return composite;
        }
        
        public void Invoke()
        {
            foreach (var action in this.actions)
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

        public static AtomicEvent<T> operator +(AtomicEvent<T> composite, IAction<T> action)
        {
            composite.actions.Add(action);
            return composite;
        }

        public static AtomicEvent<T> operator +(AtomicEvent<T> composite, System.Action<T> @delegate)
        {
            var action = new AtomicAction<T>(@delegate);
            composite.actions.Add(action);
            composite.delegates[@delegate] = action;
            return composite;
        }

        public static AtomicEvent<T> operator -(AtomicEvent<T> composite, IAction<T> action)
        {
            composite.actions.Remove(action);
            return composite;
        }

        public static AtomicEvent<T> operator -(AtomicEvent<T> composite, Action<T> @delegate)
        {
            if (composite.delegates.TryGetValue(@delegate, out var action))
            {
                composite.delegates.Remove(@delegate);
                composite.actions.Remove(action);
            }

            return composite;
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
}