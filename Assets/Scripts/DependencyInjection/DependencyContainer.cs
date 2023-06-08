using System;
using System.Collections.Generic;
using UnityEngine;

namespace DependencyInjection
{
    public class DependencyContainer
    {
        private readonly Cache _cache;
        private readonly DependencyInjector _injector;

        public DependencyContainer()
        {
            _cache = new Cache();
            _injector = new DependencyInjector(this);
        }

        public T Add<T>() where T : class
        {
            var instance = Activator.CreateInstance(typeof(T));
            _cache.Add(instance);
            return (T) instance;
        }

        public void Add(object target)
        {
            _cache.Add(target);
        }
        public void Bind(object target)
        {
            _cache.Add(target);
            _injector.Inject(target);
        }
        public T Bind<T>() where T: class
        {
            var constructors = typeof(T).GetConstructors();
            object instance;

            if (constructors.Length > 1)
            {
                throw new Exception("trying to bind multiple constructor object. can't choose.");
            }

            if (constructors.Length == 1)
            {
                var paramInfos = constructors[0].GetParameters();
                var args = _injector.GetArguments(paramInfos);
                instance = Activator.CreateInstance(typeof(T), args);
            }
            else
            {
                instance = Activator.CreateInstance(typeof(T));
            }
            _cache.Add(instance);
            _injector.Inject(instance);
            return (T) instance;
        }
        public void Inject(object target)
        {
            _injector.Inject(target);
        }
        public object Get(Type type)
        {
            return _cache.Get(type);
        }
        public T Get<T>()
        {
            return (T) _cache.Get(typeof(T));
        }
        public object GetList(Type type)
        {
            return _cache.GetList(type);
        }
        private class Cache
        {
            private readonly Dictionary<Type, List<object>> _objectsByInterfaces = new Dictionary<Type, List<object>>();
            private readonly Dictionary<Type, object> _objectsByTypes = new Dictionary<Type, object>();
            public void Add(object target)
            {
                var type = target.GetType();
                var interfaces = type.GetInterfaces();
                if (!_objectsByTypes.ContainsKey(type))
                    _objectsByTypes[type] = target;
                else
                    Debug.LogWarning($"{type} is already bound to {target.GetType()} ");

                foreach (var i in interfaces)
                {
                    if (!_objectsByInterfaces.ContainsKey(i))
                    {
                        _objectsByInterfaces.Add(i, new List<object>());
                    }

                    if (_objectsByInterfaces[i].Contains(target))
                    {
                        Debug.LogWarning($"{target.GetType()} already add to {i} list");
                        continue;
                    }
                    else
                    {
                        _objectsByInterfaces[i].Add(target);
                    }
                }
            }

            public object Get(Type type)
            {
                if (_objectsByTypes.TryGetValue(type, out var obj))
                {
                    return obj;
                }
                return null;
            }

            public object GetList(Type type)
            {

                if (_objectsByInterfaces.ContainsKey(type))
                    return _objectsByInterfaces[type];

                return new List<object>();
            }
        }
    }
}