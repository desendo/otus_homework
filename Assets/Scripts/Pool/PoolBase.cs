using System;
using System.Collections.Generic;
using DependencyInjection.Util;
using UnityEngine;

namespace Pool
{

    public class PoolBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private Transform _container;
        [SerializeField] private T _prefab;

        private readonly Queue<T> _pool = new Queue<T>();
        private readonly HashSet<T> _active = new HashSet<T>();


        private void AddToPool(Action<T> callbackBeforeAwake = null)
        {
            var instance = InstantiateUtil.Instantiate(_prefab, _container, callbackBeforeAwake);
            _pool.Enqueue(instance);
        }

        public virtual T Spawn(Action<T> callbackBeforeAwake = null)
        {
            if(_pool.Count == 0)
                AddToPool(callbackBeforeAwake);

            if (_pool.TryDequeue(out var instance))
            {
                instance.transform.SetParent(_worldTransform);
                if (_active.Add(instance))
                {
                    if(instance is ISpawn spawn)
                        spawn.OnSpawn();
                    return instance;
                }

                throw new Exception($"already {typeof(T)} in active");
            }
            throw new Exception($"no {typeof(T)} in pool");
        }

        public virtual void Unspawn(T instance)
        {
            instance.transform.SetParent(_container);

            if (_active.Contains(instance))
            {
                _pool.Enqueue(instance);
                _active.Remove(instance);
                if(instance is ISpawn spawn)
                    spawn.OnUnSpawn();
            }
        }

        public void Clear()
        {
            foreach (var instance in _active)
            {
                instance.transform.SetParent(_container);
                _pool.Enqueue(instance);
                if(instance is ISpawn spawn)
                    spawn.OnUnSpawn();
            }
            _active.Clear();
        }
    }

    public interface ISpawn
    {
        void OnSpawn();
        void OnUnSpawn();
    }
}