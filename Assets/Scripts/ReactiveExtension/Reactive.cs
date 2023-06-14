using System;
using System.Collections.Generic;
using System.Linq;

namespace ReactiveExtension
{
    public class Reactive<T> : IReactive<T>
    {
        private T _value;

        private readonly List<Action<T>> _actions = new List<Action<T>>();
        private bool _isInvoking;
        private readonly List<Action<T>> _removeAfterInvoke = new List<Action<T>>();

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
                {
                    _value = value;
                    InvokeSubscriptions(value);
                }
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
            if(!_isInvoking)
                _actions.Remove(callback);
            else
                _removeAfterInvoke.Add(callback);
        }

        private void InvokeSubscriptions(T value)
        {
            _isInvoking = true;
            foreach (var action in _actions)
            {
                action?.Invoke(value);
            }
            _isInvoking = false;

            foreach (var action in _removeAfterInvoke)
                _actions.Remove(action);
            _actions.Clear();
        }
    }
}