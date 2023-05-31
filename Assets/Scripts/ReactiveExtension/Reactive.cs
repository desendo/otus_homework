using System;
using System.Collections.Generic;
using System.Linq;

namespace ReactiveExtension
{
    public class Reactive<T> : IReactive<T>
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
            var cache = _actions.ToList();
            foreach (var action in cache)
            {
                action?.Invoke(value);
            }
        }
    }
}