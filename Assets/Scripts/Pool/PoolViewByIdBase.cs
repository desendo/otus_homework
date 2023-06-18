using System;
using System.Collections.Generic;
using Homework;
using UnityEngine;

namespace Pool
{
    public class PoolViewByIdBase<T> : MonoBehaviour, IPool<T> where T : MonoBehaviour, IView
    {
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private Transform _container;
        [SerializeField] private PrefabsById[] _prefabsByIds;
        [SerializeField] private int _initialPoolSize;

        private readonly List<T> _pool = new List<T>();
        private readonly HashSet<T> _active = new HashSet<T>();

        [System.Serializable]
        private class PrefabsById
        {
            public string Id;
            public T Prefab;
        }

        private void OnDrawGizmos()
        {
            if(_prefabsByIds == null)
                return;

            foreach (var prefabsById in _prefabsByIds)
            {
                prefabsById.Id = prefabsById.Prefab.name;
            }
        }

        private void Awake()
        {
            for (var i = 0; i < _initialPoolSize; i++)
            {
                foreach (var prefabsById in _prefabsByIds)
                {
                    AddToPool(prefabsById.Id);
                }
            }
        }

        private T AddToPool(string id)
        {
            foreach (var prefabsById in _prefabsByIds)
            {
                if (id == prefabsById.Id)
                {
                    var instance = Instantiate(prefabsById.Prefab, _container);
                    _pool.Add(instance);
                    return instance;
                }
            }
            throw new Exception($"no prefab for view id {id} of type {typeof(T)} in {name}");
        }

        public virtual T Spawn(string id, Transform parent = null)
        {
            T instance = null;
            foreach (var x in _pool)
            {
                if (x.ViewId == id)
                {
                    instance = x;
                    break;
                }
            }

            if(instance == null)
                instance = AddToPool(id);

            if (instance != null)
            {
                _pool.Remove(instance);
                instance.transform.SetParent(parent ? parent : _worldTransform);
                if(_active.Add(instance))
                    return instance;

                throw new Exception($"already {typeof(T)} in active");
            }
            throw new Exception($"no {typeof(T)} in pool. id:{id}");
        }

        public void Unspawn(T instance)
        {
            instance.transform.SetParent(_container);

            if (_active.Contains(instance))
            {
                _active.Remove(instance);
            }
            _pool.Add(instance);
        }

        public void Clear()
        {
            foreach (var instance in _active)
            {
                instance.transform.SetParent(_container);
                _pool.Add(instance);
            }
            _active.Clear();
        }
    }

    public interface IPool<T>
    {
        public void Unspawn(T instance);
        public  T Spawn(string id, Transform parent = null);
    }
}