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
        protected List<IAction<T>> actions;

        public AtomicEvent()
        {
            this.actions = new List<IAction<T>>(1);
        }

        public AtomicEvent(params IAction<T>[] actions)
        {
            this.actions = new List<IAction<T>>(actions);
        }

        public static AtomicEvent<T> operator +(AtomicEvent<T> composite, IAction<T> action)
        {
            if (composite == null)
            {
                composite = new AtomicEvent<T>();
            }

            composite.actions.Add(action);
            return composite;
        }
        
        public static AtomicEvent<T> operator +(AtomicEvent<T> composite, System.Action<T> action)
        {
            composite += new AtomicAction<T>(action);
            return composite;
        }
        
        public static AtomicEvent<T> operator -(AtomicEvent<T> composite, IAction<T> action)
        {
            if (composite == null)
            {
                return null;
            }

            composite.actions.Remove(action);
            return composite;
        }
        
        public void Invoke(T args)
        {
            foreach (var listener in this.actions)
            {
                listener.Invoke(args);
            }
        }
    }
}