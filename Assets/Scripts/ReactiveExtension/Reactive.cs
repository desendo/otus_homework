using System;
using System.Collections.Generic;

namespace ReactiveExtension
{
    public interface IDisposable
    {
        void Dispose();
    }

    internal class DisposeContainer : IDisposable
    {
        private readonly Action _disposeAction;

        public DisposeContainer(Action disposeAction)
        {
            _disposeAction = disposeAction;
        }

        public void Dispose()
        {
            _disposeAction.Invoke();
        }
    }

    public class Reactive<T>
    {
        private T _value;

        private readonly List<Action<T>> _actions = new List<Action<T>>();

        public Reactive(T value = default)
        {
            _value = value;
        }

        public T Value
        {
            get => _value;
            set
            {
                if (!Equals(_value, value))
                    InvokeSubscriptions(value);
                _value = value;
            }
        }

        public IDisposable Subscribe(Action<T> callback)
        {
            _actions.Add(callback);
            callback?.Invoke(_value);
            return new DisposeContainer(()=>DisposeCallback(callback));
        }

        private void DisposeCallback(Action<T> callback)
        {
            _actions.Remove(callback);
        }

        private void InvokeSubscriptions(T value)
        {
            foreach (var action in _actions)
            {
                action?.Invoke(value);
            }
        }
    }
    public class Event<T>
    {
        private readonly List<Action<T>> _actions = new List<Action<T>>();
        public void Invoke(T value)
        {
            InvokeSubscriptions(value);
        }

        public IDisposable Subscribe(Action<T> callback)
        {
            _actions.Add(callback);
            return new DisposeContainer(()=>DisposeCallback(callback));
        }

        private void DisposeCallback(Action<T> callback)
        {
            _actions.Remove(callback);
        }

        private void InvokeSubscriptions(T value)
        {
            foreach (var action in _actions)
            {
                action?.Invoke(value);
            }
        }
    }
    public class Event
    {
        private readonly List<Action> _actions = new List<Action>();
        public void Invoke()
        {
            InvokeSubscriptions();
        }

        public IDisposable Subscribe(Action callback)
        {
            _actions.Add(callback);
            return new DisposeContainer(()=>DisposeCallback(callback));
        }

        private void DisposeCallback(Action callback)
        {
            _actions.Remove(callback);
        }

        private void InvokeSubscriptions()
        {
            foreach (var action in _actions)
            {
                action?.Invoke();
            }
        }
    }
}